using EldenBoost.Common.Enumerations;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ArticleValidations;

namespace EldenBoost.Core.Models.Article
{
    public class ArticleEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Display(Name = "Article Type")]
        public ArticleType ArticleType { get; set; }

        [Required]
        [MaxLength(ImageURLMaxLength)]
        public string ImageURL { get; set; } = null!;
    }
}
