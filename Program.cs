using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Mottu.Uwb.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// === Controllers + JSON ===
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// === Swagger ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu UWB API",
        Version = "v1",
        Description = "API para rastreamento de motos com sensores UWB"
    });

    // Suporte à autenticação via API Key
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Informe sua API Key no formato: **12345**",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// === Banco de Dados ===
var environment = builder.Environment.EnvironmentName;

if (environment == "Testing")
{
    // Ambiente de Teste → Banco em Memória
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid()));
}
else
{
    // Ambiente real → PostgreSQL
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

    // HealthCheck apenas se houver connection string válida
    if (!string.IsNullOrEmpty(connectionString))
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(connectionString, name: "PostgreSQL", failureStatus: HealthStatus.Unhealthy);
    }
    else
    {
        builder.Services.AddHealthChecks();
    }
}

// === Injeção de Serviços ===
builder.Services.AddScoped<Mottu.Uwb.Api.Services.MotoService>();
builder.Services.AddScoped<Mottu.Uwb.Api.Services.SensorService>();
builder.Services.AddScoped<Mottu.Uwb.Api.Services.LocalizacaoService>();

// === Versionamento ===
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

// === Swagger ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// === Middleware padrão ===
if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseCors("DevCorsPolicy");

// === Middleware de autenticação via API Key ===
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/swagger") ||
        context.Request.Path.StartsWithSegments("/health"))
    {
        await next();
        return;
    }

    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key ausente.");
        return;
    }

    var config = context.RequestServices.GetRequiredService<IConfiguration>();
    var apiKey = config.GetValue<string>("ApiKey");

    if (string.IsNullOrEmpty(apiKey) || apiKey != extractedApiKey)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key inválida.");
        return;
    }

    await next();
});

app.UseAuthorization();
app.MapControllers();

// === HealthCheck ===
app.MapHealthChecks("/health");

app.Run();

// Necessário para testes de integração
public partial class Program { }
