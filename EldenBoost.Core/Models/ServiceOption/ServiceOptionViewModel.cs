using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ServiceOptionValidations;
using static EldenBoost.Common.Constants.ValidationConstants.DecimalUniversalRange;
using static EldenBoost.Common.Constants.ValidationErrors;

namespace EldenBoost.Core.Models.ServiceOption
{
    public class ServiceOptionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = FieldLength)]
        public string Name { get; set; } = null!;

        [Range(typeof(decimal), DecimalMin, DecimalMax, ErrorMessage = FieldLength)]
        public decimal Price { get; set; }
    }
}
