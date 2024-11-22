using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class ServicesCarouselComponent : ViewComponent
    {
        private readonly IServiceService serviceService;

        public ServicesCarouselComponent(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await serviceService.LastThreeServicesAsync();
            return View(model);
        }
    }
}
