using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class Payment
    {
        [Key]
        [Comment("Unique identifier for the Payment.")]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Comment("Total amount of the Payment.")]
        public decimal Amount { get; set; }

        [Comment("Indicates if the Payment has been completed.")]
        public bool IsPaid { get; set; }

        [Comment("Date when the Payment was issued.")]
        public DateTime IssueDate { get; set; }

        [Comment("Date when the Payment was completed.")]
        public DateTime CompletionDate { get; set; }

        [ForeignKey(nameof(Booster))]
        [Comment("Foreign key linking the Payment to the associated Booster.")]
        public int BoosterId { get; set; }

        [Comment("Booster associated with the Payment.")]
        public Booster Booster { get; set; } = null!;

        [Comment("List of Orders linked to this Payment.")]
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}