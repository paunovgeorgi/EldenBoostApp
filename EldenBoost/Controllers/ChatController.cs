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
        private readonly ILogger<ChatController> logger;

        public ChatController(
            IBoosterService _boosterService,
            IOrderService _orderService,
            IChatMessageService _chatMessageService,
            ILogger<ChatController> _logger)
        {
            boosterService = _boosterService;
            orderService = _orderService;
            chatMessageService = _chatMessageService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> OrderChat(int id)
        {
            string receiverId = ""; 
            string userId = User.Id();

            bool isBooster = await boosterService.BoosterExistsByUserIdAsync(userId);
            bool isActiveBooster = await boosterService.IsActiveAsync(userId);

            // Check if the user is a booster, and if so if they're active or demoted
            if (isBooster && !isActiveBooster)
            {
                return RedirectToAction("Index", "Home");
            }

            // Get order with booster 
            Order? order = await orderService.GetOrderWithBoosterByOrderIdAsync(id);

            if (order == null)
            {
                return NotFound(); 
            }

            // Determine sender and receiver
            receiverId = isBooster ? order.ClientId : order.Booster!.UserId;

            try
            {
                var model = await chatMessageService.GetChatViewModelAsync(id, receiverId);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching the chat model for order ID: {OrderId}", id);
                return StatusCode(500);
            }
   
        }
    }
}
