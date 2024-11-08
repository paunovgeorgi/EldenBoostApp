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

        public OrderController(IOrderService _orderService,
            IBoosterService _boosterService)
        {
            orderService = _orderService;
            boosterService = _boosterService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var booster = await boosterService.GetBoosterByUserIdAsync(User.Id());
            if (booster == null)
            {
                return Unauthorized("You don't have access to this page");
            }

            //Get the platform that this booster supports.
            int[] platforms = booster.Platforms.Select(p => p.Id).ToArray();

            var orders = await orderService.AllOrdersAsync();

            //Get only the orders with platforms supported by the booster.
            var ordersToShow = orders.Where(o => platforms.Contains(o.PlatformId))
                .OrderByDescending(o => o.IsExpress).ToList();

            return View(ordersToShow);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int orderId)
        {

            string userId = User.Id();

            //Check if order exists.
            if (await orderService.ExistsByIdAsync(orderId) == false)
            {
                return BadRequest("Order does not exist");
            }

            //Check if the user attempting the action is a booster.
            if (await boosterService.BoosterExistsByUserIdAsync(userId) == false)
            {
                TempData[WarningMessage] = "You're not a booster!";
                return RedirectToAction("Index", "Home");
            }

            //Check if the order is already taken by another booster.
            if (await orderService.IsTakenAsync(orderId))
            {
                TempData[WarningMessage] = "Order is already taken!";
                return RedirectToAction("All", "Order");
            }

            int boosterId = await boosterService.GetBoosterIdAsync(User.Id());
            var result = await orderService.AssignBoosterAsync(orderId, boosterId);
            if (!result.Success)
            {
                TempData[ErrorMessage] = result.Message;
                return RedirectToAction("All", "Order");
            }

            TempData[SuccessMessage] = result.Message;
            return RedirectToAction("MyProfile", "Booster");
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int orderId)
        {
            int boosterId = await boosterService.GetBoosterIdAsync(User.Id());

            //Check if this is the booster assigned to the order.
            if (await orderService.HasBoosterWithIdAsync(orderId, boosterId) == false)
            {
                return BadRequest("Not your order mate");
            }

            await orderService.CompleteAsync(orderId);
            TempData[SuccessMessage] = "Order completed!";

            return RedirectToAction("MyProfile", "Booster");
        }
    }
}
