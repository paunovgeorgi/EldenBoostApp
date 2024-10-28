using EldenBoost.Infrastructure.Data.Models;
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


            base.OnModelCreating(builder);
        }
    }
}
