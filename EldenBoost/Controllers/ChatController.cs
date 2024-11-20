using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IBoosterService boosterService;
        private readonly IOrderService orderService;
        private readonly IChatMessageService chatMessageService;

        public ChatController(IBoosterService _boosterService, IOrderService _orderService, IChatMessageService _chatMessageService)
        {
            boosterService = _boosterService;
            orderService = _orderService;
            chatMessageService = _chatMessageService;
        }

        public async Task<IActionResult> OrderChat(int id)
        {
            string receiverId = ""; 
            string userId = User.Id();

            bool isBooster = await boosterService.BoosterExistsByUserIdAsync(userId);
            bool isActiveBooster = await boosterService.IsActiveAsync(userId);

            if (isBooster && !isActiveBooster)
            {
                return RedirectToAction("Index", "Home");
            }

            Order? order = await orderService.GetOrderWithBoosterByOrderIdAsync(id);

            if (order == null)
            {
                return NotFound(); 
            }

            if (isBooster)
            {
                receiverId = order.ClientId;
            }
            else
            {
                receiverId = order.Booster!.UserId;
            }

            var model = await chatMessageService.GetChatViewModelAsync(id, receiverId);


            return View(model);
        }
    }
}
