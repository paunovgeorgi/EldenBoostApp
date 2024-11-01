using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
