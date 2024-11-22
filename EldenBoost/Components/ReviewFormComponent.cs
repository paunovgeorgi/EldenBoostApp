using EldenBoost.Core.Models.Review;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class ReviewFormComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ReviewFormViewModel? model = null)
        {
            // Provide a new model if none is passed
            model ??= new ReviewFormViewModel();
            return View(model);
        }
    }
}
