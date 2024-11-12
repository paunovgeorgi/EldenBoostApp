using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class ApplicationController : BaseController
    {
        private readonly IApplicationService applicationService;
        private readonly IPlatformService platformService;
        private readonly IOrderService orderService;
        private readonly IBoosterService boosterService;
        private readonly IUserService userService;

        public ApplicationController(IApplicationService _applicationService,
            IPlatformService _platformService,
            IOrderService _orderService,
            IBoosterService _boosterService,
            IUserService _userService)
        {
            applicationService = _applicationService;
            platformService = _platformService;
            orderService = _orderService;
            boosterService = _boosterService;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> SubmitBooster()
        {
            string userId = User.Id();

            if (await userService.HasOrdersAsync(userId))
            {
                TempData[InformationMessage] = "You are already using our service as a client. If you'd like to apply for this position you should make a new registration.";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            bool hasApplied = await applicationService.HasAppliedByUserIdAsync(userId, a => a.ApplicationType == ApplicationType.Booster);
            if (hasApplied)
            {
                TempData[InformationMessage] = "You've already applied for this position!";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            var platforms = await platformService.GetAllPlatformsAsync();

            var model = new ApplicationFormModel();

            model.SupportedPlatforms = platforms;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBooster(ApplicationFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData[SuccessMessage] = "Successfully submited your application. Our admins will contact you if approved!";
            await applicationService.CreateBoosterApplicationAsync(User.Id(), model);

            return RedirectToAction("Index", "Home");
        }

    }
}
