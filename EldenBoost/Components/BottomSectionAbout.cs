using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class BottomSectionAbout : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
