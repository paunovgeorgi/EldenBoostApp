using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants; 

namespace EldenBoost.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;
        public UserController(IUserService _userService, ILogger<UserController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }

        // Retrieve all users
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var users = await userService.AllAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving users.");
                TempData[ErrorMessage] = "Failed to load users.";
                return RedirectToAction("Index", "Home");
            }
        }

        // Demote user from Booster or Author position
        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            try
            {
                await userService.DemoteAsync(userId);
                TempData[SuccessMessage] = "User successfully demoted!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while demoting user with ID {UserId}.", userId);
                TempData[ErrorMessage] = "Failed to demote the user.";
            }

            return RedirectToAction(nameof(All));
        }

        // Reinstate user to Booster or Author position
        [HttpPost]
        public async Task<IActionResult> Reinstate(string userId)
        {
            try
            {
                await userService.ReinstateAsync(userId);
                TempData[SuccessMessage] = "User successfully reinstated!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while reinstating user with ID {UserId}.", userId);
                TempData[ErrorMessage] = "Failed to reinstate the user.";
            }
            return RedirectToAction(nameof(All));
        }
    }
}
