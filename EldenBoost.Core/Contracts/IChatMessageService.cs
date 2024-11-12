using EldenBoost.Core.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IChatMessageService
    {
        Task<ChatViewModel> GetChatViewModelAsync(int orderId, string receiverId);
    }
}
