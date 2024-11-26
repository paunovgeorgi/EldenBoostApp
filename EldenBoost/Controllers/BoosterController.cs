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
        public BoosterController(
            IBoosterService _boosterService,
            IOrderService _orderService,
            IUserService _userService,
            IPaymentService _paymentService)
        {
            boosterService = _boosterService;
            orderService = _orderService;
            userService = _userService;
            paymentService = _paymentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await boosterService.AllActiveBoostersToCardModelAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var booster = await boosterService.GetBoosterByUserIdAsync(User.Id());

            if (booster == null)
            {
                return Unauthorized("You don't have access!");
            }

            ViewBag.Username = await userService.GetUserNicknameAsync(User.Id());
            ViewBag.TotalEarned = booster.TotalEarned;
            ViewBag.ReadyForRequst = await paymentService.ReadyForRequstAsync(User.Id());
            ViewBag.RequestedAmount = await paymentService.RequestedAmountAsync(User.Id());
            ViewBag.ProfilePicture = await userService.GetProfilePictureByUseIdAsync(User.Id());

            var orders = await orderService.AllByBoosterIdAsync(booster.Id);

            return View(orders);
        }

        [HttpPost]

        public async Task<IActionResult> Rate(int boosterId, int orderId, int rating)
        {
            if (await boosterService.GetBoosterByBoosterIdAsync(boosterId) == null)
            {
                TempData[ErrorMessage] = "Booster does not exist";
                return RedirectToAction("MyOrders", "Client");
            }

            await boosterService.RateAsync(boosterId, rating);
            await orderService.RateAsync(orderId);
            TempData[SuccessMessage] = "Booster rated successfully!";

            return RedirectToAction("MyProfile", "User");
        }
    }
}
