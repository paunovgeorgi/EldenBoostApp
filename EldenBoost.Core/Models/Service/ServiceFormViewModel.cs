using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Models.ServiceOption;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.DecimalUniversalRange;
using static EldenBoost.Common.Constants.ValidationConstants.ServiceValidations;
using static EldenBoost.Common.Constants.ValidationErrors;

namespace EldenBoost.Core.Models.Service
{
    public class ServiceFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = FieldLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = FieldLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), DecimalMin, DecimalMax, ConvertValueInInvariantCulture = true, ErrorMessage = "Price must be a positive number and less than {2} usd")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        public int? MaxAmount { get; set; }

        public ServiceType ServiceType { get; set; }

        public List<ServiceType> ServiceTypes { get; set; } = new List<ServiceType>();

        public List<ServiceOptionViewModel> ServiceOptions { get; set; } = new List<ServiceOptionViewModel>();
    }
}
