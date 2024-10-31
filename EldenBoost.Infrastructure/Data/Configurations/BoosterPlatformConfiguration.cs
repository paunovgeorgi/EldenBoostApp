using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class BoosterPlatformConfiguration : IEntityTypeConfiguration<BoosterPlatform>
    {
        public void Configure(EntityTypeBuilder<BoosterPlatform> builder)
        {
            builder.HasData(
                new BoosterPlatform { BoosterId = 1, PlatformId = 1 }, // Cloud - PC
                new BoosterPlatform { BoosterId = 1, PlatformId = 2 }, // Cloud - Playstation
                new BoosterPlatform { BoosterId = 2, PlatformId = 1 }, // Mr. White - PC
                new BoosterPlatform { BoosterId = 2, PlatformId = 2 }, // Mr. White - Playstation
                new BoosterPlatform { BoosterId = 2, PlatformId = 3 }, // Mr. White - Xbox
                new BoosterPlatform { BoosterId = 3, PlatformId = 1 }, // D.VA - PC
                new BoosterPlatform { BoosterId = 3, PlatformId = 3 }, // D.VA - Xbox
                new BoosterPlatform { BoosterId = 4, PlatformId = 2 }  // Mr. Wick - Playstation
            );
        }
    }
}
