using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POWER_System.Models;
using System.Reflection.Emit;

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

        public DbSet<EnclosurePartOrder> EnclosurePartOrders { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Part>().HasKey(q => q.Id);
            builder.Entity<Enclosure>().HasKey(q => q.Id);

            builder.Entity<EnclosurePart>()
            .HasKey(i => i.Id);

            builder.Entity<EnclosurePartOrder>()
                .HasKey(bc => new { bc.EnclosurePartId, bc.PartOrderId });

            builder.Entity<EnclosurePartOrder>()
                .HasOne(o => o.PartOrder)
                .WithMany(o => o.EnclosureParts)
                .HasForeignKey(o => o.PartOrderId);

            builder.Entity<EnclosurePartOrder>()
                .HasOne(o => o.EnclosurePart)
                .WithMany(o => o.EnclosureParts)
                .HasForeignKey(o => o.EnclosurePartId);

            builder.Entity<UserProject>()
    .HasKey(k => new { k.ProjectId, k.UserId });

            builder.Entity<UserProject>()
                .HasOne(o => o.User)
                .WithMany(o => o.PersonalProjects)
                .HasForeignKey(o => o.UserId);

            builder.Entity<UserProject>()
                .HasOne(o => o.Project)
                .WithMany(o => o.UserProjects)
                .HasForeignKey(o => o.ProjectId);

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