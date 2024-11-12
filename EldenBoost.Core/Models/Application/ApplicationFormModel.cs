using EldenBoost.Core.Models.Platform;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.BoosterValidations;
using static EldenBoost.Common.Constants.ValidationErrors;

namespace EldenBoost.Core.Models.Application
{
    public class ApplicationFormModel
    {
        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = FieldLength)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(ExperienceMaxLength, MinimumLength = ExperienceMinLength, ErrorMessage = FieldLength)]
        [Display(Name = "Summary of your experience")]
        public string Experience { get; set; } = null!;

        [Required]
        [Range(1, 12)]
        [Display(Name = "Available hours per day")]
        public int Availabiliity { get; set; }

        [Display(Name = "Platforms you can boost on")]
        public IEnumerable<PlatformFormModel> SupportedPlatforms { get; set; } = new List<PlatformFormModel>();
    }
}
