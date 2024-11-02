using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class FaqComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
