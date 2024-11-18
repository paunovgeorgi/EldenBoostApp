using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IBoosterService boosterService;
        private readonly IPlatformService platformService;
        private readonly IServiceService serviceService;
        private readonly IOrderService orderService;
        public CartController(ICartService _cartService,
            IBoosterService _boosterService,
            IPlatformService _platformService,
            IServiceService _serviceService,
            IOrderService _orderService)
        {
            cartService = _cartService;
            boosterService = _boosterService;
            platformService = _platformService;
            serviceService = _serviceService;
            orderService = _orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int serviceId, int platformId, decimal? updatedPrice, bool hasStream, bool isExpress, int? optionId, int sliderValue)
        {

            if (await platformService.PlatformExistsByIdAsync(platformId) == false)
            {
                return BadRequest("Platform doesn't not exist mate");
            }

            if (await serviceService.ExistsByIdAsync(serviceId) == false)
            {
                return BadRequest("Service doesn't exist mate");
            }

            if (await boosterService.BoosterExistsByUserIdAsync(User.Id()))
            {
                TempData[ErrorMessage] = "You are a booster mate, can't purchase services.";
                return RedirectToAction("All", "Service");
            }
            TempData[SuccessMessage] = "Service successfully added to your cart!";

            await cartService.AddToCartAsync(User.Id(), serviceId, platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
            return RedirectToAction("All", "Service");
        }


        [HttpGet]
        public async Task<IActionResult> ShowCart()
        {
            string userId = User.Id();
            var cartViewModel = await cartService.GetCartViewModelAsync(userId);

            return PartialView("_CartPartial", cartViewModel);
        }
    }
}
