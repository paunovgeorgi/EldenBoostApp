using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ApplicationController : BaseAdminController
    {
        private readonly IApplicationService applicationService;

        public ApplicationController(IApplicationService _applicationService)
        {
            applicationService = _applicationService;
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
            await applicationService.ApproveBoosterAsync(id);

            return RedirectToAction(nameof(AllBooster));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAuthor(int id)
        {
            await applicationService.ApproveAuthorAsync(id);

            return RedirectToAction(nameof(AllAuthor));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string returnUrl)
        {
            await applicationService.RejectAsync(id);

            return RedirectToAction(returnUrl);
        }
    }
}
