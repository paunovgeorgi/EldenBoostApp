using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ReviewValidations;
using static EldenBoost.Common.Constants.ValidationErrors;

namespace EldenBoost.Core.Models.Review
{
    public class ReviewFormViewModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = FieldLength)]
        public string Content { get; set; } = null!;
    }
}
