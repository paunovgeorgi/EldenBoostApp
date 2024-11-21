using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class ReviewsCarouselComponent : ViewComponent
    {
        private readonly IReviewService reviewService;

        public ReviewsCarouselComponent(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await reviewService.GetLatestReviewsAsync();
            return View(model);
        }
    }
}
