using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Chat;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IRepository repository;
        public ChatMessageService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<ChatViewModel> GetChatViewModelAsync(int orderId, string receiverId)
        {
            var messages = await repository.AllReadOnly<ChatMessage>()
                  .Where(m => m.OrderId == orderId)
                  .Include(m => m.Sender)
                  .Include(m => m.Receiver)
                  .OrderBy(m => m.Timestamp)
                  .ToListAsync();

            var model = new ChatViewModel
            {
                OrderId = orderId,
                ReceiverId = receiverId,
                Messages = messages
            };

            return model;
        }
    }
}
