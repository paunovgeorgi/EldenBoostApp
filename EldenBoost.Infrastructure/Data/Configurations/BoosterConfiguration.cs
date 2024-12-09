using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class BoosterConfiguration : IEntityTypeConfiguration<Booster>
    {
        public void Configure(EntityTypeBuilder<Booster> builder)
        {           
            builder.HasData(GenerateBoosters()); 
        }

        private Booster[] GenerateBoosters()
        {
            return new[]
            {
            new Booster
            {
                Id = 1,
                Country = "Japan",
                UserId = "a13592d9-c4d0-4184-a3f9-dc7c66640808", // Cloud's UserId
            },
            new Booster
            {
                Id = 2,
                Country = "USA",
                UserId = "b4e77aa9-36ad-4010-987e-9fd6d7b0d6ac", // Mr. White's UserId
            },
            new Booster
            {
                Id = 3,
                Country = "South Korea",
                UserId = "54cf5237-4a7c-4050-9570-7cb5cb753aa5", // D.VA's UserId
            },
            new Booster
            {
                Id = 4,
                Country = "United Kingdom",
                UserId = "ca3439dd-d67e-4733-8b72-a497af8b4c64", // Mr. Wick's UserId
            }
        };
        }
    }

}
