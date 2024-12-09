namespace EldenBoost.Core.Models.Chat
{
    public class ChatViewModel
    {
        public int OrderId { get; set; }
        public string ReceiverId { get; set; } = null!;
        public List<ChatMessageViewModel> Messages { get; set; } = new List<ChatMessageViewModel>();
    }
}
