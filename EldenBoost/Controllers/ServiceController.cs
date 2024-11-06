using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EldenBoost.Core.Extensions;
using static EldenBoost.Common.Constants.NotificationConstants;
using EldenBoost.Core.Services;
using EldenBoost.Extensions;

namespace EldenBoost.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService serviceService;
        private readonly IOrderService orderService;
        private readonly IBoosterService boosterService;
        private readonly IPlatformService platformService;

        public ServiceController(
            IServiceService _serviceService,
            IOrderService _orderService,
            IBoosterService _boosterService,
            IPlatformService _platformService)
        {
            serviceService = _serviceService;
            orderService = _orderService;
            boosterService = _boosterService;
            platformService = _platformService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllServicesQueryModel model)
        {
			//Gets filtered and paged collection of ServiceCardViewModel for all services.
			AllServicesFilteredAndPagedModel serviceModel = await serviceService.AllAsync(model);

            model.Services = serviceModel.Services;
            model.TotalServices = serviceModel.TotalServicesCount;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Popular()
        {
			//Gets services in ServiceCardViewModel ordered by purchase count.
			var model = await serviceService.GetPopularServicesAsync();

            return View(model);
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

		[HttpPost]
		public async Task<IActionResult> Buy(int serviceId, int platformId, decimal? updatedPrice, bool hasStream, bool isExpress, int? optionId, int sliderValue)
		{
			//Check if service exists.
			if (await serviceService.ExistsByIdAsync(serviceId) == false)
			{
				return BadRequest("Service doesn't exist!");
			}

			//Check if platform exists.
			if (await platformService.PlatformExistsByIdAsync(platformId) == false)
			{
				return BadRequest("Platform doesn't exist!");
			}
            
            //Check if user is booster, if so - redirect.
			if (await boosterService.BoosterExistsByUserIdAsync(User.Id()))
			{
				TempData[ErrorMessage] = "You are a booster mate, can't purchase services.";
				return RedirectToAction("All", "Service");
			}

			TempData[SuccessMessage] = "Your purchase was successful!";

            //Create order.
			await orderService.CreateOrderAsync(serviceId, User.Id(), platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
			return RedirectToAction("MyProfile", "Client");
		}
	}
}
