using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Chat;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace EldenBoost.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService userService;
        private readonly IChatMessageService chatMessageService;

        public ChatHub(IUserService _userService, IChatMessageService _chatMessageService)
        {
            userService = _userService;
            chatMessageService = _chatMessageService;
        }

        public async Task SendMessage(string user, string message, int orderId, string receiverId)
        {
            string senderNickname = await userService.GetUserNicknameAsync(user);
            string senderProfilePicture = await userService.GetProfilePictureByUseIdAsync(user);


            var chatMessage = new ChatMessageViewModel
            {
                SenderId = user,
                ReceiverId = receiverId,
                Message = message,
                OrderId = orderId,
                Timestamp = DateTime.Now
            };

            await chatMessageService.SaveMessageAsync(chatMessage);

            string timestamp = chatMessage.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

            string currentUserId = Context.UserIdentifier!;

            // Broadcast the message to the specified receiver
            await Clients.Users(user, receiverId).SendAsync("ReceiveMessage", senderNickname, senderProfilePicture, message, timestamp, user == currentUserId);
        }
    }
}
