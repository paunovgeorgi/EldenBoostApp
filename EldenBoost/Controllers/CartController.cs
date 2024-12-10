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
        private readonly IAuthorService authorService;
        private readonly ILogger<CartController> logger;
        public CartController(ICartService _cartService,
            IBoosterService _boosterService,
            IPlatformService _platformService,
            IServiceService _serviceService,
            IOrderService _orderService,
            IAuthorService _authorService,
            ILogger<CartController> _logger)
        {
            cartService = _cartService;
            boosterService = _boosterService;
            platformService = _platformService;
            serviceService = _serviceService;
            orderService = _orderService;
            authorService = _authorService;
            logger = _logger;
        }

        // Add service to cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int serviceId, int platformId, decimal? updatedPrice, bool hasStream, bool isExpress, int? optionId, int sliderValue)
        {
            // Check if the platform exists
            if (await platformService.PlatformExistsByIdAsync(platformId) == false)
            {
                return BadRequest("Platform doesn't exist.");
            }

            // Check if the service exists
            if (await serviceService.ExistsByIdAsync(serviceId) == false)
            {
                return BadRequest("Service doesn't exist.");
            }

            // Check if the user is a booster (cannot purchase services)
            if (await boosterService.BoosterExistsByUserIdAsync(User.Id()))
            {
                TempData[ErrorMessage] = "You are a booster, you can't purchase services.";
                return RedirectToAction("All", "Service");
            }

            // Check if the user is an author (cannot purchase services)
            if (await authorService.ExistsByUserIdAsync(User.Id()))
			{
				TempData[ErrorMessage] = "You're an author, you can't purchase services.";
				return RedirectToAction("All", "Service");
			}

            try
            {
                await cartService.AddToCartAsync(User.Id(), serviceId, platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
                TempData[SuccessMessage] = "Service successfully added to your cart!";
                return RedirectToAction("All", "Service");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a service to the cart.");
                TempData[ErrorMessage] = "An error occurred while adding the service to your cart.";
                return RedirectToAction("All", "Service");
            }
           
        }

        // Show user's cart 
        [HttpGet]
        public async Task<IActionResult> ShowCart()
        {
            try
            {
                string userId = User.Id();
                var cartViewModel = await cartService.GetCartViewModelAsync(userId);
                return PartialView("_CartPartial", cartViewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching the cart.");
                return StatusCode(500, "Internal server error");
            }
        }

        // Remove an item from the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            try
            {
                var result = await cartService.RemoveItemAsync(cartItemId);
                if (result)
                {
                    var updatedCart = await cartService.GetCartViewModelAsync(User.Id());
                    return PartialView("_CartPartial", updatedCart);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while removing an item from the cart.");
                return StatusCode(500, "Internal server error");
            }
        }

        // Get the cart's item quantity
        [HttpGet]
        public async Task<IActionResult> GetCartQuantity()
        {
            try
            {
                var userId = User.Id();
                int quantity = await cartService.GetCartQuantityByUserIdAsync(userId);
                return Json(quantity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching the cart quantity.");
                return StatusCode(500, "Internal server error");
            }
        }

        // Remove all items from the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var userId = User.Id();
                int cartId = await cartService.GetCartIdAsync(userId);
                await cartService.ClearCartAsync(cartId);
                var updatedCart = await cartService.GetCartViewModelAsync(User.Id());
                return PartialView("_CartPartial", updatedCart);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while clearing the cart.");
                return StatusCode(500, "Internal server error");
            }
        }

        // Checkout and complete the purchase
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                var userId = User.Id();
                int cartId = await cartService.GetCartIdAsync(userId);
                await orderService.CreateOrdersFromCartAsync(cartId, userId);
                await cartService.ClearCartAsync(cartId);
                TempData[SuccessMessage] = "Your purchase was completed successfully!";
                return RedirectToAction("MyProfile", "User");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during the checkout process.");
                TempData[ErrorMessage] = "An error occurred during checkout.";
                return RedirectToAction("All", "Service");
            }
        }
    }
}
