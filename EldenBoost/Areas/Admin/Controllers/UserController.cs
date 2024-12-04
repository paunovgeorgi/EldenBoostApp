using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants; 

namespace EldenBoost.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await userService.AllAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            await userService.DemoteAsync(userId);

            TempData[SuccessMessage] = "User successfully Demoted!";
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Reinstate(string userId)
        {
            await userService.ReinstateAsync(userId);

            TempData[SuccessMessage] = "User successfully Reinstated!";
            return RedirectToAction(nameof(All));
        }
    }
}
