using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IBoosterService boosterService;
        private readonly ILogger<OrderController> logger;
        public OrderController(IOrderService _orderService,
            IBoosterService _boosterService,
            ILogger<OrderController> _logger)
        {
            orderService = _orderService;
            boosterService = _boosterService;
            logger = _logger;
        }

        // Get all pending orders
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var booster = await boosterService.GetBoosterByUserIdAsync(User.Id());
            bool isActiveBooster = await boosterService.IsActiveAsync(User.Id());

            //Check if the user is an active booster.
            if (booster == null || !isActiveBooster)
            {
                logger.LogWarning("Unauthorized access attempt by user ID: {UserId}", User.Id());
                return Forbid();
            }

            //Get the platforms this booster supports.
            int[] platforms = booster.Platforms.Select(p => p.Id).ToArray();

            try
            {
                var orders = await orderService.AllOrdersAsync();
                var ordersToShow = orders
                    .Where(o => platforms.Contains(o.PlatformId))
                    .OrderByDescending(o => o.IsExpress)
                    .ToList();

                return View(ordersToShow);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching orders.");
                return StatusCode(500);
            }
        }

        // Assing order to booster
        [HttpPost]
        public async Task<IActionResult> Assign(int orderId)
        {
            string userId = User.Id();

            // Check if the order exists
            if (!await orderService.ExistsByIdAsync(orderId))
            {
                logger.LogWarning("Attempt to assign non-existent order ID: {OrderId}", orderId);
                return BadRequest("Order does not exist");
            }

            // Check if the booster is active or has been demoted
            if (!await boosterService.IsActiveAsync(userId))
            {
                logger.LogWarning("Inactive booster (ID: {UserId}) tried to assign order {OrderId}.", userId, orderId);
                TempData[WarningMessage] = "You're not an active booster!";
                return RedirectToAction("Index", "Home");
            }

            //Check whether or not the order's already been taken by another booster
            if (await orderService.IsTakenAsync(orderId))
            {
                logger.LogInformation("Order {OrderId} is already taken.", orderId);
                TempData[WarningMessage] = "Order is already taken!";
                return RedirectToAction("All", "Order");
            }

            try
            {
                int boosterId = await boosterService.GetBoosterIdAsync(userId);
                var result = await orderService.AssignBoosterAsync(orderId, boosterId);

                if (!result.Success)
                {
                    TempData[ErrorMessage] = result.Message;
                    return RedirectToAction("All", "Order");
                }

                TempData[SuccessMessage] = result.Message;
                return RedirectToAction("MyProfile", "Booster");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while assigning booster ID: {UserId} to order ID: {OrderId}", userId, orderId);
                return StatusCode(500);
            }
        }

        // Complete the order 
        [HttpPost]
        public async Task<IActionResult> Complete(int orderId)
        {
            int boosterId = await boosterService.GetBoosterIdAsync(User.Id());

            //Check if this is the booster assigned to the order.
            if (await orderService.HasBoosterWithIdAsync(orderId, boosterId) == false)
            {
                return BadRequest("Not your order!");
            }

            try
            {
                await orderService.CompleteAsync(orderId);
                TempData[SuccessMessage] = "Order completed!";
                return RedirectToAction("MyProfile", "Booster");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while completing order ID: {OrderId} by booster ID: {BoosterId}", orderId, boosterId);
                return StatusCode(500);
            }
        }
    }
}
