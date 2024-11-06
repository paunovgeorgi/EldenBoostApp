using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using EldenBoost.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        public IActionResult All()
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
            // Check if the service is of type Option.
            // If so, remove ServiceOptions form the model so that the ModelState can pass.
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            //Gets service edit view model and checks if it's null;
            var model = await serviceService.GetServiceEditViewModelByIdAsync(id);
            if (model == null)
            {
                return BadRequest("Service does not exist!");
            }

            //Gets service options and fills the model.
            var options = await serviceService.GetServiceOptionsAsync(model.Id);
            model.ServiceOptions = (List<ServiceOptionViewModel>)options;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var options = await serviceService.GetServiceOptionsAsync(model.Id);
                model.ServiceOptions = (List<ServiceOptionViewModel>)options;
                return View(model);
            }

            await serviceService.EditAsync(model);
            TempData[SuccessMessage] = "Service edited successfully!";

            string information = model.Title.Replace(" ", "-").ToLower() + "-boost";

            return RedirectToAction(nameof(Details), new { area = "", model.Id, information });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //Deactivates the service;
            await serviceService.DeleteByIdAsync(id);
            TempData[WarningMessage] = "Service deactivated.";

            return RedirectToAction(nameof(All));
        }
    }
}
