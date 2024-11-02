using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class BottomSectionJoin : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
