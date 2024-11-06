using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;

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
    }
}
