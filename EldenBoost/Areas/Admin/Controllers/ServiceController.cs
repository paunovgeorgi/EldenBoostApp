using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Services;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ServiceController : BaseAdminController
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {     
            ServiceFormViewModel model = new ServiceFormViewModel();

            model.ServiceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(ServiceFormViewModel model)
        {
            if (model.ServiceType != ServiceType.Option)
            {
                ModelState.Remove("ServiceOptions");
                foreach (var option in model.ServiceOptions)
                {
                    ModelState.Remove($"ServiceOptions[{model.ServiceOptions.IndexOf(option)}].Name");
                    ModelState.Remove($"ServiceOptions[{model.ServiceOptions.IndexOf(option)}].Price");
                }
            }

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "Model is invalid";
                model.ServiceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
                return View(model);
            }

            await serviceService.CreateServiceAsync(model);

            return RedirectToAction("All", "Service");
        }
    }
}
