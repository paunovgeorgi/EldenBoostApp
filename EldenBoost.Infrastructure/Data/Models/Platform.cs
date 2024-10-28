using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static EldenBoost.Common.Constants.ValidationConstants.PlatformValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Platform
    {
        [Key]
        [Comment("Unique identifier for the Platform.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of the Platform.")]
        public string Name { get; set; } = string.Empty;

        [Comment("Collection of Boosters associated with this Platform.")]
        public ICollection<Booster> Boosters { get; set; } = new List<Booster>();

        [Comment("Collection of Applications submitted for this Platform.")]
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}