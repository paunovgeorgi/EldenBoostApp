using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        [Comment("Unique identifier for the Order.")]
        public int Id { get; set; }

        [ForeignKey(nameof(Service))]
        [Comment("ID of the associated Service.")]
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        [ForeignKey(nameof(Booster))]
        [Comment("ID of the assigned Booster, if any.")]
        public int? BoosterId { get; set; }

        [Comment("Assigned Booster for this Order, if any.")]
        public Booster? Booster { get; set; }

        [ForeignKey(nameof(Client))]
        [Comment("ID of the client placing the order.")]
        public string ClientId { get; set; } = null!;

        [Comment("Client who placed the Order.")]
        public ApplicationUser Client { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Amount to be paid to the Booster.")]
        public decimal BoosterPay { get; set; }

        [Required]
        [Comment("Current status of the Order.")]
        public string Status { get; set; } = string.Empty;

        [Comment("Timestamp of when the Order was placed.")]
        public DateTime TimeOfPurchase { get; set; }

        [Comment("Indicates if the Order has been archived.")]
        public bool IsArchived { get; set; }

        [Comment("Indicates if the Order has been rated by the client.")]
        public bool IsRated { get; set; }

        [Comment("Indicates if the Order has been paid to the booster.")]
        public bool IsPaid { get; set; }

        [Comment("Indicates if the Order has been added to a payment batch.")]
        public bool IsAddedToPayment { get; set; }

        [Required]
        [ForeignKey(nameof(Platform))]
        [Comment("ID of the platform associated with the Order.")]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;

        [ForeignKey(nameof(Payment))]
        [Comment("ID of the payment associated with the Order, if any.")]
        public int? PaymentId { get; set; }

        [Comment("Payment associated with the Order, if any.")]
        public Payment? Payment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Comment("Total price of the Order.")]
        public decimal Price { get; set; }

        [Comment("Indicates if the Order includes streaming.")]
        public bool HasStream { get; set; }

        [Comment("Indicates if the Order is an express request.")]
        public bool IsExpress { get; set; }

        [Comment("Additional information provided for the Order.")]
        public string? Information { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}