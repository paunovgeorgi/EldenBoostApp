using EldenBoost.Infrastructure.Data.Configurations;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Data
{
    public class EldenBoostDbContext : IdentityDbContext<ApplicationUser>
    {
        public EldenBoostDbContext(DbContextOptions<EldenBoostDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booster> Boosters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ServiceOption> ServiceOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Booster>()
                .HasMany(b => b.Platforms)
                .WithMany(p => p.Boosters)
                .UsingEntity<Dictionary<string, object>>(
                "BoosterPlatform",
                r => r
                .HasOne<Platform>()
                .WithMany()
                .HasForeignKey("PlatformId")
                .HasConstraintName("FK_BoosterPlatform_Platforms_Id")
                .OnDelete(DeleteBehavior.Cascade),
                r => r
                .HasOne<Booster>()
                .WithMany()
                .HasForeignKey("BoosterId")
                .HasConstraintName("FK_BoosterPlatform_Boosters_Id")
                .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("BoosterId", "PlatformId");
                    j.ToTable("BoostersPlatforms");
                    j.IndexerProperty<int>("BoosterId").HasColumnName("BoosterId");
                    j.IndexerProperty<int>("PlatformId").HasColumnName("PlatformId");
                });

            builder.Entity<Application>()
        .HasMany(a => a.Platforms)
        .WithMany(p => p.Applications)
        .UsingEntity<Dictionary<string, object>>(
            "ApplicationPlatform",
            r => r
                .HasOne<Platform>()
                .WithMany()
                .HasForeignKey("PlatformId")
                .HasConstraintName("FK_ApplicationPlatform_Platforms_Id")
                .OnDelete(DeleteBehavior.Cascade),
            r => r
                .HasOne<Application>()
                .WithMany()
                .HasForeignKey("ApplicationId")
                .HasConstraintName("FK_ApplicationPlatform_Applications_Id")
                .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.HasKey("ApplicationId", "PlatformId");
                j.ToTable("ApplicationsPlatforms");
                j.IndexerProperty<int>("ApplicationId").HasColumnName("ApplicationId");
                j.IndexerProperty<int>("PlatformId").HasColumnName("PlatformId");
            });

            builder.ApplyConfiguration(new PlatformConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new ServiceOptionConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration(new PasswordHasher<ApplicationUser>()));

            base.OnModelCreating(builder);
        }
    }
}
