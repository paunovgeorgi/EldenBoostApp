using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EldenBoost.Common.Constants.ValidationConstants.AuthorValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Author
    {
        [Key]
        [Comment("Unique identifier for the Author")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        [Comment("Country of residence for the Author.")]
        public string Country { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        [Comment("User ID associated with the author.")]
        public string UserId { get; set; } = null!;


        [Comment("User associated with the author.")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("Collection of articles for the author.")]
        public ICollection<Article> Articles { get; set; } = new List<Article>();

        [Comment("Flag for Active and Demoted authors.")]
        public bool IsDemoted { get; set; }
    }
}
