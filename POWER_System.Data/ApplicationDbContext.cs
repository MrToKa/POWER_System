using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using POWER_System.Models;

namespace POWER_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enclosure> Enclosure { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Cable> Cables { get; set; }

        public DbSet<PartOrder> PartsOrders { get; set; }

        public DbSet<CableOrder> CablesOrders { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<SiteService> SiteServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Enclosure>(entity =>
            {
                entity.HasMany(p => p.Parts)
                    .WithMany(p => p.Enclosure);
            });

            builder.Entity<Enclosure>(entity =>
            {
                entity.HasMany(p => p.Cables)
                    .WithOne(p => p.Enclosure);
            });

            builder.Entity<Part>(entity =>
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)"));

            base.OnModelCreating(builder);
        }
    }
}