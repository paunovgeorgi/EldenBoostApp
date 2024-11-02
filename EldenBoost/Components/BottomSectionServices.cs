using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class BottomSectionServices : ViewComponent
    {
        private readonly IServiceService serviceService;

        public BottomSectionServices(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await serviceService.GetPopularServicesAsync();
            return View(model);
        }
    }
}
