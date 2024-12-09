using System.ComponentModel.DataAnnotations.Schema;

namespace EldenBoost.Infrastructure.Data.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; } = null!;

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; } = null!;

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;

    }
}
