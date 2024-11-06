using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.Booster
{
    public class BoosterCardViewModel
    {
        public string Name { get; set; } = null!;

        public string? ProfilePicture { get; set; }

        public string SupportedPlatforms { get; set; } = null!;

        public double Rating { get; set; }

        public double RatingCount { get; set; }
    }
}
