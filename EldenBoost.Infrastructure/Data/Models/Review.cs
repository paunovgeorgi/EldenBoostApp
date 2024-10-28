using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EldenBoost.Common.Constants.ValidationConstants.ReviewValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        [Comment("Unique identifier for the Review.")]
        public int Id { get; set; }

        [MaxLength(ContentMaxLength)]
        [Comment("Content of the review, limited to 248 characters.")]
        public string Content { get; set; } = null!;

        [ForeignKey(nameof(User))]
        [Comment("Foreign key referencing the User who created the review.")]
        public string UserId { get; set; } = null!;

        [Comment("User who created the review, represented by the ApplicationUser entity.")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("Date and time when the review was created, defaulting to UTC now.")]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
