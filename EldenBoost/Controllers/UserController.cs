using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class UserController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IBoosterService boosterService;
        private readonly IAuthorService authorService;
        private readonly IUserService userService;

        public UserController(IOrderService _orderService,
            IBoosterService _boosterService,
            IAuthorService _authorService,
            IUserService _userService)
        {
            orderService = _orderService;
            boosterService = _boosterService;
            authorService = _authorService;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            if (await boosterService.BoosterExistsByUserIdAsync(User.Id()))
            {
                TempData[ErrorMessage] = "You're not a client!";
                return RedirectToAction("MyProfile", "Booster");
            }
            else if(await authorService.ExistsByUserIdAsync(User.Id()))
            {
                TempData[ErrorMessage] = "You're not a client!";
                return RedirectToAction("MyProfile", "Author");
            }

            ViewBag.Username = await userService.GetUserNicknameAsync(User.Id());
            ViewBag.ProfilePicture = await userService.GetProfilePictureByUseIdAsync(User.Id());
            ViewBag.TotalOrders = await orderService.NumberOfOrdersByClientIdAsync(User.Id());
            ViewBag.TotalSpent = await orderService.TotalPaidByClientIdAsync(User.Id());

            var orders = await orderService.AllByUserIdAsync(User.Id());

            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeProfilePic(string imageUrl)
        {
            await userService.ChangeProfilePictureAsync(User.Id(), imageUrl);

            return RedirectToAction("MyProfile");

        }
    }
}
