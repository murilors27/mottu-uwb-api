using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Models;

namespace Mottu.Uwb.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.IdentificadorUWB)
                .IsUnique();

            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.Motos)
                .WithOne(m => m.Sensor!)
                .HasForeignKey(m => m.SensorId);

            modelBuilder.Entity<Localizacao>()
                .HasOne<Moto>()
                .WithMany()
                .HasForeignKey(l => l.MotoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Localizacao>()
                .HasOne<Sensor>()
                .WithMany()
                .HasForeignKey(l => l.SensorId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
