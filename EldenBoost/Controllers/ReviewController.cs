using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Review;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly IUserService userService;

        public ReviewController(IReviewService _reviewService,IUserService _userService)
        {
            reviewService = _reviewService;
            userService = _userService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("ReviewFormComponent", new { model });
            }

            bool hasOrders = await userService.HasOrdersAsync(User.Id());

            if (!hasOrders)
            {
                TempData[InformationMessage] = "You need to have at least one order to submit a review!";
                return RedirectToAction("Index", "Home");
            }

            await reviewService.CreateReviewAsync(model.Content, User.Id());

            return RedirectToAction("Index", "Home");
        }
    }
}
