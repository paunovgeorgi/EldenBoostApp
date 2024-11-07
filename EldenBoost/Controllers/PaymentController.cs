using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestPay()
        {
            string userId = User.Id();

            bool hasPending = await paymentService.IsPendingAsync(userId);
            if (hasPending)
            {
                TempData[InformationMessage] = "You have a pending payment. Once it's paid you'll be able to request another one.";
                var refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
                return RedirectToAction("MyProfile", "Booster");
            }

            bool hasOrdersForRequest = await paymentService.HasOrdersToRequestAsync(userId);
            if (!hasOrdersForRequest)
            {
                TempData[InformationMessage] = "You have no unpaid completed orders to request payment for.";
                return RedirectToAction("MyProfile", "Booster");
            }

            TempData[SuccessMessage] = "Your payment request has been submitted.";

            await paymentService.CreatePaymentAsync(userId);

            return RedirectToAction("MyProfile", "Booster");
        }
    }
}
