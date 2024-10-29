using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.HasData(GeneratePlatforms());
        }

        private Platform[] GeneratePlatforms()
        {
            ICollection<Platform> platforms = new HashSet<Platform>();

            Platform platform;

            platform = new Platform()
            {
                Id = 1,
                Name = "PC"
            };

            platforms.Add(platform);

            platform = new Platform()
            {
                Id = 2,
                Name = "Playstation"
            };

            platforms.Add(platform);

            platform = new Platform()
            {
                Id = 3,
                Name = "Xbox"
            };

            platforms.Add(platform);

            return platforms.ToArray();
        }
    }
}
