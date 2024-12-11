using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ApplicationController : BaseAdminController
    {
        private readonly IApplicationService applicationService;
        private readonly IBoosterService boosterService;
        private readonly IAuthorService authorService;
        private readonly ILogger<ApplicationController> logger;

        public ApplicationController(IApplicationService _applicationService,
            IBoosterService _boosterService,
            IAuthorService _authorService,
            ILogger<ApplicationController> _logger)
        {
            applicationService = _applicationService;
            boosterService = _boosterService;
            authorService = _authorService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> AllBooster()
        {
            try
            {
                // Retrieve all pending booster applications.
                var models = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Booster);
                return View(models);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving booster applications.");
                TempData[ErrorMessage] = "Failed to load booster applications.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllAuthor()
        {
            try
            {
                // Retrieve all pending author applications.
                var models = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Author);
                return View(models);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving author applications.");
                TempData[ErrorMessage] = "Failed to load author applications.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBooster(int id)
        {
            // Check if the applicant has already been approved for the Author positio
            string? userId = await applicationService.GetApplicantUserIdByApplicationIdAsync(id);
            bool isAuthor = await authorService.ExistsByUserIdAsync(userId!);
            if (isAuthor)
            {
                TempData[ErrorMessage] = "Applicant has already been approved to be an Author, booster application must be rejected!";
                return RedirectToAction(nameof(AllBooster));
            }

            try
            {
                // Approve the application
                await applicationService.ApproveBoosterAsync(id);
                TempData[SuccessMessage] = "Booster application has been approved!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while approving booster application with ID {ApplicationId}.", id);
                TempData[ErrorMessage] = "Failed to approve the booster application.";
            }
              
            return RedirectToAction(nameof(AllBooster));
           
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAuthor(int id)
        {
            // Check if the applicant has already been approved for the booster position
            string? userId =  await applicationService.GetApplicantUserIdByApplicationIdAsync(id);
            var isBooster = await boosterService.BoosterExistsByUserIdAsync(userId!);
            if (isBooster)
            {
                TempData[ErrorMessage] = "Applicant has already been approved to be a Booster, author application must be rejected!";
                return RedirectToAction(nameof(AllAuthor));
            }

            try
            {
                // Approve the application
                await applicationService.ApproveAuthorAsync(id);
                TempData[SuccessMessage] = "Author application has been approved!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while approving author application with ID {ApplicationId}.", id);
                TempData[ErrorMessage] = "Failed to approve the author application.";
            }

            return RedirectToAction(nameof(AllAuthor));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string returnUrl)
        {
            try
            {
                // Reject application.
                await applicationService.RejectAsync(id);
                TempData[SuccessMessage] = "Application has been rejected!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while rejecting application with ID {ApplicationId}.", id);
                TempData[ErrorMessage] = "Failed to reject the application.";
            }

            return RedirectToAction(returnUrl);
        }
    }
}
