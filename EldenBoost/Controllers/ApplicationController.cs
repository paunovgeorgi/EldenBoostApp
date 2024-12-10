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
        private readonly IAuthorService authorService;
        private readonly ILogger<ApplicationController> logger;

        public ApplicationController(IApplicationService _applicationService,
            IPlatformService _platformService,
            IOrderService _orderService,
            IBoosterService _boosterService,
            IUserService _userService,
            IAuthorService _authorService,
            ILogger<ApplicationController> _logger)
        {
            applicationService = _applicationService;
            platformService = _platformService;
            orderService = _orderService;
            boosterService = _boosterService;
            userService = _userService;
            authorService = _authorService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> SubmitBooster()
        {
            string userId = User.Id();

            // Check if the user has existing orders that disqualify them from applying.
            var hasOrdersCheckResult = await CheckIfUserHasOrdersAsync(userId);
            if (hasOrdersCheckResult != null)
            {
                return hasOrdersCheckResult;
            }

            // Check if the user has already applied to be a booster.
            var boosterApplicationCheck = await CheckIfUserHasAppliedAsync(userId, ApplicationType.Booster);
            if (boosterApplicationCheck != null)
            {
                return boosterApplicationCheck;
            }

            // Verify if the user is already a booster.
            var boosterCheckResult = await CheckIfUserIsBoosterAsync(userId);
            if (boosterCheckResult != null)
            {
                return boosterCheckResult;
            }

            // Ensure the user is not already an author.
            var authorCheckResult = await CheckIfUserIsAuthorAsync(userId);
            if (authorCheckResult != null)
            {
                return authorCheckResult;
            }

            try
            {
                // Fetch all available platforms for the application form.
                var platforms = await platformService.GetAllPlatformsAsync();

                // Initialize the application form model with the list of platforms.
                var model = new ApplicationFormModel { SupportedPlatforms = platforms };
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the error and display a generic error message to the user.
                logger.LogError(ex, "Error fetching platforms for SubmitBooster view (User: {UserId})", userId);
                TempData[ErrorMessage] = "An error occurred while loading the application form. Please try again later.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBooster(ApplicationFormModel model)
        {
            // If the form data is invalid, redisplay the form with validation errors.
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string userId = User.Id();

            // Check if the user has existing orders that disqualify them from applying.
            var hasOrdersCheckResult = await CheckIfUserHasOrdersAsync(userId);
            if (hasOrdersCheckResult != null)
            {
                return hasOrdersCheckResult;
            }

            // Check if the user has already applied to be a booster.
            var boosterApplicationCheck = await CheckIfUserHasAppliedAsync(userId, ApplicationType.Booster);
            if (boosterApplicationCheck != null)
            {
                return boosterApplicationCheck;
            }

            // Verify if the user is already a booster.
            var boosterCheckResult = await CheckIfUserIsBoosterAsync(userId);
            if (boosterCheckResult != null)
            {
                return boosterCheckResult;
            }

            // Ensure the user is not already an author.
            var authorCheckResult = await CheckIfUserIsAuthorAsync(userId);
            if (authorCheckResult != null)
            {
                return authorCheckResult;
            }

            try
            {
                // Create a new booster application and save it to the database.
                await applicationService.CreateBoosterApplicationAsync(userId, model);
                TempData[SuccessMessage] = "Successfully submitted your application. Our admins will contact you if approved!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the error and display a generic error message to the user.
                logger.LogError(ex, "Error submitting booster application (User: {UserId})", userId);
                TempData[ErrorMessage] = "An error occurred while submitting your application. Please try again later.";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SubmitAuthor()
        {

            string userId = User.Id();

            // Check if the user has existing orders that disqualify them from applying.
            var hasOrdersCheckResult = await CheckIfUserHasOrdersAsync(userId);
            if (hasOrdersCheckResult != null)
            {
                return hasOrdersCheckResult;
            }

            // Check if the user has already applied to be an author.
            var authorApplicationCheck = await CheckIfUserHasAppliedAsync(userId, ApplicationType.Author);
            if (authorApplicationCheck != null)
            {
                return authorApplicationCheck;
            }

            // Verify if the user is already a booster.
            var boosterCheckResult = await CheckIfUserIsBoosterAsync(userId);
            if (boosterCheckResult != null)
            {
                return boosterCheckResult;
            }

            // Ensure the user is not already an author.
            var authorCheckResult = await CheckIfUserIsAuthorAsync(userId);
            if (authorCheckResult != null)
            {
                return authorCheckResult;
            }

            try
            {
                // Initialize the application form model.
                var model = new ApplicationFormModel();
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the error and display a generic error message to the user.
                logger.LogError(ex, "Error rendering SubmitAuthor view (User: {UserId})", userId);
                TempData[ErrorMessage] = "An error occurred while loading the application form. Please try again later.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAuthor(ApplicationFormModel model)
        {
            // If the form data is invalid, redisplay the form with validation errors.
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string userId = User.Id();

            // Check if the user has existing orders that disqualify them from applying.
            var hasOrdersCheckResult = await CheckIfUserHasOrdersAsync(userId);
            if (hasOrdersCheckResult != null)
            {
                return hasOrdersCheckResult;
            }

            // Check if the user has already applied to be an author.
            var authorApplicationCheck = await CheckIfUserHasAppliedAsync(userId, ApplicationType.Author);
            if (authorApplicationCheck != null)
            {
                return authorApplicationCheck;
            }

            // Verify if the user is already a booster.
            var boosterCheckResult = await CheckIfUserIsBoosterAsync(userId);
            if (boosterCheckResult != null)
            {
                return boosterCheckResult;
            }

            // Ensure the user is not already an author.
            var authorCheckResult = await CheckIfUserIsAuthorAsync(userId);
            if (authorCheckResult != null)
            {
                return authorCheckResult;
            }

            try
            {
                // Create a new author application and save it to the database.
                await applicationService.CreateAuthorApplicationAsync(User.Id(), model);
                TempData[SuccessMessage] = "Successfully submited your application. Our admins will contact you if approved!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the error and display a generic error message to the user.
                logger.LogError(ex, "Error submitting author application (User: {UserId})", userId);
                TempData[ErrorMessage] = "An error occurred while submitting your application. Please try again later.";
                return View(model);
            }       
        }




        //------------------- private actions ----------------

        private async Task<IActionResult?> CheckIfUserHasOrdersAsync(string userId)
        {
            if (await userService.HasOrdersAsync(userId))
            {
                TempData[InformationMessage] = "You are already using our service as a client. If you'd like to apply for this position, you should make a new registration.";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        private async Task<IActionResult?> CheckIfUserIsBoosterAsync(string userId)
        {
            bool isBooster = await boosterService.BoosterExistsByUserIdAsync(userId);
            if (isBooster)
            {
                TempData[InformationMessage] = "You are already part of our Boosters team!";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        private async Task<IActionResult?> CheckIfUserIsAuthorAsync(string userId)
        {
            bool isAuthor = await authorService.ExistsByUserIdAsync(userId);
            if (isAuthor)
            {
                TempData[InformationMessage] = "You are already part of our Authors team!";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        private async Task<IActionResult?> CheckIfUserHasAppliedAsync(string userId, ApplicationType applicationType)
        {
            bool hasApplied = await applicationService.HasAppliedByUserIdAsync(userId, a => a.ApplicationType == applicationType);
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
            return null;
        }

    }
}
