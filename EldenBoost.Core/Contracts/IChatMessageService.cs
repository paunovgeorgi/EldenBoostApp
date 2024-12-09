using EldenBoost.Core.Models.Chat;

namespace EldenBoost.Core.Contracts
{
    public interface IChatMessageService
    {
        Task<ChatViewModel> GetChatViewModelAsync(int orderId, string receiverId);
        Task SaveMessageAsync(ChatMessageViewModel message);
    }
}
