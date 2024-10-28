using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ArticleValidations;
using EldenBoost.Common.Enumerations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Article
    {
        [Key]
        [Comment("Unique identifier for the Article.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Title of the Article.")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLength)]
        [Comment("Main content of the Article.")]
        public string Content { get; set; } = null!;

        [Comment("Type of Article, represented by the ArticleType enumeration.")]
        public ArticleType ArticleType { get; set; }

        [Comment("Date when the Article was released or published.")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(ImageURLMaxLength)]
        [Comment("URL of the image associated with the Article.")]
        public string ImageURL { get; set; } = null!;

        [ForeignKey(nameof(Author))]
        [Comment("ID of the author who created the Article.")]
        public int AuthorId { get; set; }

        [Comment("Author entity associated with the Article.")]
        public Author Author { get; set; } = null!;
    }
}