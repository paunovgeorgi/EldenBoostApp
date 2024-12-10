using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;
        public PaymentController(IPaymentService _paymentService, ILogger<PaymentController> _logger)
        {
            paymentService = _paymentService;
            logger = _logger;
        }

        [HttpPost]
        public async Task<IActionResult> RequestPay()
        {
            string userId = User.Id();

            // Check if the booster has a pending payment request
            bool hasPending = await paymentService.IsPendingAsync(userId);
            if (hasPending)
            {
                TempData[InformationMessage] = "You have a pending payment. Once it's paid you'll be able to request another one.";

                // If the request comes from a page, redirect back
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("MyProfile", "Booster");
            }

            // Check if the booster has any orders eligible for a payment request (orders must be completed and not not paid)
            bool hasOrdersForRequest = await paymentService.HasOrdersToRequestAsync(userId);
            if (!hasOrdersForRequest)
            {
                TempData[InformationMessage] = "You have no unpaid completed orders to request payment for.";
                return RedirectToAction("MyProfile", "Booster");
            }

            try
            {
                TempData[SuccessMessage] = "Your payment request has been submitted.";
                await paymentService.CreatePaymentAsync(userId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing the payment request for user {UserId}.", userId);
                TempData[ErrorMessage] = "An error occurred while processing your request. Please try again later.";
            }
           
            return RedirectToAction("MyProfile", "Booster");
        }
    }
}
