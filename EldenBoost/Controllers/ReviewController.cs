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
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService _reviewService,IUserService _userService, ILogger<ReviewController> _logger)
        {
            reviewService = _reviewService;
            userService = _userService;
            logger = _logger;
        }

        // Submit client review
        [HttpPost]
        public async Task<IActionResult> Submit(ReviewFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("ReviewFormComponent", new { model });
            }

            // Check if the user has any purchases (needs at least one to submit a review)
            bool hasOrders = await userService.HasOrdersAsync(User.Id());

            if (!hasOrders)
            {
                TempData[InformationMessage] = "You need to have at least one order to submit a review!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Create the review
                await reviewService.CreateReviewAsync(model.Content, User.Id());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while submitting the review for user {UserId}.", User.Id());
                TempData[ErrorMessage] = "There was an error while submitting your review. Please try again.";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
