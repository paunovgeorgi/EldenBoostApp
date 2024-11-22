using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class ThreeStepsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
