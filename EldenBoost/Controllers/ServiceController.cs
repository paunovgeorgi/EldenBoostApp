using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EldenBoost.Core.Extensions;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllServicesQueryModel model)
        {
            AllServicesFilteredAndPagedModel serviceModel = await serviceService.AllAsync(model);

            model.Services = serviceModel.Services;
            model.TotalServices = serviceModel.TotalServicesCount;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Popular()
        {
            var model = await serviceService.GetPopularServicesAsync();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string information)
        {
            var model = await serviceService.GetServiceViewModelByIdAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = "Service with provided model does not exist!";
                return RedirectToAction("All", "Service");
            }

            if (information != model.GetInformation())
            {
                return BadRequest("Info doesn't add-up mate");
            }

            var options = await serviceService.GetServiceOptionsAsync(model.Id);
            model.Options = (ICollection<ServiceOptionViewModel>)options;

            return View(model);
        }
    }
}
