using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<EnclosurePart> EnclosurePart { get; set; }

        public DbSet<PartsQuantity> PartTagsQuantities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Part>().HasKey(q => q.Id);
            builder.Entity<Enclosure>().HasKey(q => q.Id);
            //builder.Entity<EnclosurePart>()
            //    .HasKey(q => new {q.PartId, q.EnclosureId});
            
            builder.Entity<EnclosurePart>()
                .HasKey(i => i.Id);

            //builder.Entity<EnclosurePart>(entity =>
            //    entity.HasMany(p => p.PartsQuantity)
            //        .WithOne(e => e.EnclosurePart)
            //        .OnDelete(DeleteBehavior.Restrict));


            builder.Entity<EnclosurePart>(entity =>
            {
                entity.HasOne(p => p.Enclosure)
                    .WithMany(p => p.Parts)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Part>(entity =>
            {
                entity.HasMany(x => x.Parts)
                    .WithOne(e => e.Part)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Enclosure>(entity =>
            {
                entity.HasMany(p => p.Parts)
                    .WithOne(p => p.Enclosure)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Enclosure>(entity =>
            {
                entity.HasMany(p => p.Cables)
                    .WithOne(p => p.Enclosure);
            });

            builder.Entity<Part>(entity =>
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)"));

            builder.Entity<Project>(entity =>
            {
                entity.HasMany(e => e.Enclosures)
                .WithOne(p => p.Project);
            });

            base.OnModelCreating(builder);
        }
    }
}