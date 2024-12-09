using EldenBoost.Infrastructure.Data.Models;

namespace EldenBoost.Core.Models.Chat
{
    public class ChatMessageViewModel
    {
        public int OrderId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public ApplicationUser Sender { get; set; } = null!;
        public ApplicationUser Receiver { get; set; } = null!;
    }
}
