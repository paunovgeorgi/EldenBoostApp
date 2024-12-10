using EldenBoost.Core.Contracts;
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
        private readonly ILogger<UserController> logger;

        public UserController(IOrderService _orderService,
            IBoosterService _boosterService,
            IAuthorService _authorService,
            IUserService _userService,
            ILogger<UserController> _logger)
        {
            orderService = _orderService;
            boosterService = _boosterService;
            authorService = _authorService;
            userService = _userService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            //// Check if the user is a booster or an author, and redirect accordingly if true
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

            // Set the user's profile information
            ViewBag.Username = await userService.GetUserNicknameAsync(User.Id());
            ViewBag.ProfilePicture = await userService.GetProfilePictureByUseIdAsync(User.Id());
            ViewBag.TotalOrders = await orderService.NumberOfOrdersByClientIdAsync(User.Id());
            ViewBag.TotalSpent = await orderService.TotalPaidByClientIdAsync(User.Id());

            try
            {
                var orders = await orderService.AllByUserIdAsync(User.Id());

                return View(orders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while loading profile for user {UserId}.", User.Id());
                TempData[ErrorMessage] = "Something went wrong while loading your profile.";
                return RedirectToAction("Index", "Home");
            }   
        }


        [HttpPost]
        public async Task<IActionResult> ChangeProfilePic(string imageUrl)
        {
            try
            {
                // Change the user's profile picture
                await userService.ChangeProfilePictureAsync(User.Id(), imageUrl);

                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while changing profile picture for user {UserId}.", User.Id());
                TempData[ErrorMessage] = "Something went wrong while changing your profile picture.";
                return RedirectToAction("MyProfile");
            }

        }
    }
}
