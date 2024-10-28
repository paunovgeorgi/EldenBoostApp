using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static EldenBoost.Common.Constants.ValidationConstants.ServiceOptionValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class ServiceOption
    {
        [Key]
        [Comment("Unique identifier for the ServiceOption.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of the ServiceOption.")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Comment("Price associated with this ServiceOption.")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Service))]
        [Comment("Reference to the associated Service entity.")]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}