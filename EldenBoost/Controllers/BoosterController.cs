using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class BoosterController : BaseController
    {
        private readonly IBoosterService boosterService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IPaymentService paymentService;
        private readonly ILogger<BoosterController> logger;
        public BoosterController(
            IBoosterService _boosterService,
            IOrderService _orderService,
            IUserService _userService,
            IPaymentService _paymentService,
            ILogger<BoosterController> _logger)
        {
            boosterService = _boosterService;
            orderService = _orderService;
            userService = _userService;
            paymentService = _paymentService;
            logger = _logger;
        }

        [HttpGet]
        [AllowAnonymous]
        // GET: All Active Boosters
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await boosterService.AllActiveBoostersToCardModelAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all active boosters.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        // Fetches the logged-in booster profile and related data
        public async Task<IActionResult> MyProfile()
        {
            try
            {
                var booster = await boosterService.GetBoosterByUserIdAsync(User.Id());
                if (booster == null)
                {
                    logger.LogWarning($"User with ID {User.Id()} tried to access booster profile but is not a booster.");
                    return Unauthorized("You don't have access!");
                }

                // Set profile-related data
                ViewBag.Username = await userService.GetUserNicknameAsync(User.Id());
                ViewBag.TotalEarned = booster.TotalEarned;
                ViewBag.ReadyForRequst = await paymentService.ReadyForRequstAsync(User.Id());
                ViewBag.RequestedAmount = await paymentService.RequestedAmountAsync(User.Id());
                ViewBag.ProfilePicture = await userService.GetProfilePictureByUseIdAsync(User.Id());

                var orders = await orderService.AllByBoosterIdAsync(booster.Id);
                return View(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching booster profile and orders.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        // Allows the user to rate the booster who's completed their order.
        public async Task<IActionResult> Rate(int boosterId, int orderId, int rating)
        {
            try
            {
                var booster = await boosterService.GetBoosterByBoosterIdAsync(boosterId);
                if (booster == null)
                {
                    logger.LogWarning($"Rating attempt for non-existing booster with ID {boosterId}.");
                    TempData[ErrorMessage] = "Booster does not exist";
                    return RedirectToAction("MyOrders", "Client");
                }

                await boosterService.RateAsync(boosterId, rating);
                await orderService.RateAsync(orderId);
                TempData[SuccessMessage] = "Booster rated successfully!";

                return RedirectToAction("MyProfile", "User");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while rating the booster.");
                TempData[ErrorMessage] = "An error occurred while processing your rating.";
                return RedirectToAction("MyProfile", "User");
            }     
        }
    }
}
