using EldenBoost.Core.Contracts;
using EldenBoost.Core.Extensions;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService serviceService;
        private readonly IOrderService orderService;
        private readonly IBoosterService boosterService;
        private readonly IPlatformService platformService;
        private readonly ILogger<ServiceController> logger;

        public ServiceController(
            IServiceService _serviceService,
            IOrderService _orderService,
            IBoosterService _boosterService,
            IPlatformService _platformService,
            ILogger<ServiceController> _logger)
        {
            serviceService = _serviceService;
            orderService = _orderService;
            boosterService = _boosterService;
            platformService = _platformService;
            logger = _logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllServicesQueryModel model)
        {
            try
            {
                //Fetches filtered and paged collection of ServiceCardViewModel for all services
                AllServicesFilteredAndPagedModel serviceModel = await serviceService.AllAsync(model);

                model.Services = serviceModel.Services;
                model.TotalServices = serviceModel.TotalServicesCount;

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching all services.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Popular()
        {
            try
            {
                // Gets the most popular services based on purchase count.
                var model = await serviceService.GetPopularServicesAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching popular services.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string information)
        {           
            var model = await serviceService.GetServiceDetailsViewModelByIdAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = "Service with provided model does not exist!";
                return RedirectToAction("All", "Service");
            }

            //Checks if service url passes the security check.
            if (information != model.GetInformation())
            {
                return BadRequest("Info is incorrect!");
            }

            var options = await serviceService.GetServiceOptionsAsync(model.Id);
            model.Options = (ICollection<ServiceOptionViewModel>)options;

            return View(model);
        }
    }
}
