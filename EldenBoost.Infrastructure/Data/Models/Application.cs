using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static EldenBoost.Common.Constants.ValidationConstants.ApplicationValidations;
using EldenBoost.Common.Enumerations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Application
    {
        [Key]
        [Comment("Unique identifier for the Application.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryMaxLength)]
        [Comment("Country of the applicant.")]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(ExperienceMaxLength)]
        [Comment("Experience level or description provided by the applicant.")]
        public string Experience { get; set; } = null!;

        [Comment("Availability status of the applicant, typically represented in hours or a defined scale.")]
        public int Availability { get; set; }

        [Comment("Type of application submitted by the user.")]
        public ApplicationType ApplicationType { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Comment("User ID associated with the application.")]
        public string UserId { get; set; } = null!;

        [Comment("User associated with the application.")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("Indicates if the application has been approved.")]
        public bool IsApproved { get; set; }

        [Comment("Indicates if the application has been rejected.")]
        public bool IsRejected { get; set; }

        [Comment("Collection of platforms the applicant is associated with.")]
        public ICollection<Platform> Platforms { get; set; } = new List<Platform>();
    }
}