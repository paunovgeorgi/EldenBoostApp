using EldenBoost.Common.Enumerations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EldenBoost.Common.Constants.ValidationConstants.ServiceValidations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Service
    {
        [Key]
        [Comment("Unique identifier for the Service.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Service title.")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("Service description.")]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Service price.")]
        public decimal Price { get; set; }

        [MaxLength(ImageUrlMaxLength)]
        [Comment("Service image url.")]
        public string? ImageURL { get; set; }

        [Required]
        [Comment("Flag for active and inactive services.")]
        public bool IsActive { get; set; } = true;

        [Comment("Service purchase count.")]
        public int PurchaseCount { get; set; }

        public int MaxAmount { get; set; }

        [Comment("Service type")]
        public ServiceType ServiceType { get; set; }

        [Comment("Collection of options for the service.")]
        public ICollection<ServiceOption> Options { get; set; } = new List<ServiceOption>();
    }
}
