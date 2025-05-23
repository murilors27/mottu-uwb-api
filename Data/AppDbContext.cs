using Microsoft.EntityFrameworkCore;
using Mottu.Uwb.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Mottu.Uwb.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Sensor> Sensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Moto>()
                .HasIndex(m => m.IdentificadorUWB)
                .IsUnique();

            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.Motos)
                .WithOne(m => m.Sensor!)
                .HasForeignKey(m => m.SensorId);
        }
    }
}
