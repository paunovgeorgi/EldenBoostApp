using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EldenBoost.Common.Constants.ValidationConstants.BoosterValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Booster
    {
        [Key]
        [Comment("Unique identifier for the Booster.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        [Comment("Country of residence for the Booster.")]
        public string Country { get; set; } = null!;

        [Comment("Current average rating of the Booster.")]
        public double Rating { get; set; }

        [Comment("Total number of ratings received by the Booster.")]
        public int RatingCount { get; set; }

        [Required]
        [Comment("Total amount earned by the Booster (completed orders only).")]
        public decimal TotalEarned { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Comment("User ID associated with the Booster.")]
        public string UserId { get; set; } = null!;

        [Comment("User entity associated with the Booster.")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("List of Platforms the Booster can boost on.")]
        public List<Platform> Platforms { get; set; } = new List<Platform>();

        [Comment("List of Orders assigned to the Booster.")]
        public List<Order> Orders { get; set; } = new List<Order>();

        [Comment("List of Payments made to the Booster.")]
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
