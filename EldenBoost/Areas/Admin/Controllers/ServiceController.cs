using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ServiceController : BaseAdminController
    {
        private readonly IServiceService serviceService;
        private ILogger<ServiceController> logger;

        public ServiceController(IServiceService _serviceService, ILogger<ServiceController> _logger)
        {
            serviceService = _serviceService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await serviceService.AllServiceListViewModelFilteredAsync(s => s.IsActive);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the services list.");
                TempData[ErrorMessage] = "Failed to load services.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {     
            ServiceFormViewModel model = new ServiceFormViewModel();

            model.ServiceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();

            return View(model);
        }

        //Add new service
        [HttpPost]
        public async Task<IActionResult> Add(ServiceFormViewModel model)
        {
            // Check if the service is of type Option.
            // If not, remove ServiceOptions from the model so that the ModelState can pass.
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

            try
            {
                await serviceService.CreateServiceAsync(model);
                TempData[SuccessMessage] = "Service added successfully!";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a new service.");
                TempData[ErrorMessage] = "Failed to add the service.";
                model.ServiceTypes = Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
                return View(model);
            }
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

        // Edit Service
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var options = await serviceService.GetServiceOptionsAsync(model.Id);
                model.ServiceOptions = (List<ServiceOptionViewModel>)options;
                return View(model);
            }

            try
            {
                await serviceService.EditAsync(model);
                TempData[SuccessMessage] = "Service edited successfully!";
                string information = model.Title.Replace(" ", "-").ToLower() + "-boost";
                return RedirectToAction(nameof(Details), new { area = "", model.Id, information });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while editing the service with ID {ServiceId}.", model.Id);
                TempData[ErrorMessage] = "Failed to edit the service.";
                return View(model);
            }
        }

        // Deactivate service
        [HttpPost]
        public async Task<IActionResult> Deactivate(int id)
        {
            if (await serviceService.ExistsByIdAsync(id) == false)
            {
                return BadRequest("Service does not exist!");
            }

            try
            {
                //Deactivate the service;
                await serviceService.DeactivateByIdAsync(id);
                TempData[WarningMessage] = "Service deactivated.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deactivating the service with ID {ServiceId}.", id);
                TempData[ErrorMessage] = "Failed to deactivate the service.";
            }


            return RedirectToAction(nameof(All));
        }

        // Activate service
        [HttpPost]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                await serviceService.ActivateByIdAsync(id);
                TempData[WarningMessage] = "Service activated.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while activating the service with ID {ServiceId}.", id);
                TempData[ErrorMessage] = "Failed to activate the service.";
            }


            return RedirectToAction(nameof(All));
        }
    }
}
