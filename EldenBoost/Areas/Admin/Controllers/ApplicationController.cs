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

        public ApplicationController(IApplicationService _applicationService, IBoosterService _boosterService, IAuthorService _authorService)
        {
            applicationService = _applicationService;
            boosterService = _boosterService;
            authorService = _authorService;
        }

        [HttpGet]
        public async Task<IActionResult> AllBooster()
        {
            var models = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Booster);
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> AllAuthor()
        {
            var models = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Author);
            return View(models);
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

            // Approve the application
            await applicationService.ApproveBoosterAsync(id);

            TempData[SuccessMessage] = "Booster application has been approved!";
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

            // Approve the application
            await applicationService.ApproveAuthorAsync(id);

            TempData[SuccessMessage] = "Author application has been approved!";
            return RedirectToAction(nameof(AllAuthor));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string returnUrl)
        {
            await applicationService.RejectAsync(id);

            TempData[SuccessMessage] = "Application has been rejected!";
            return RedirectToAction(returnUrl);
        }
    }
}
