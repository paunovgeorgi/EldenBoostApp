using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var model = await boosterService.AllBoostersToCardModelAsync();

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
            ViewBag.RequestedAmount = await paymentService.RequsetedAmountAsync(User.Id());
            ViewBag.ProfilePicture = await userService.GetProfilePictureByUseIdAsync(User.Id());

            var orders = await orderService.AllByBoosterIdAsync(booster.Id);

            return View(orders);
        }
    }
}
