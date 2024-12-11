using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class PaymentController : BaseAdminController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;

        public PaymentController(IPaymentService _paymentService, ILogger<PaymentController> _logger)
        {
            paymentService = _paymentService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                // Retrieve all unpaid payments
                var payments = await paymentService.AllPaymentsFiltered(p => p.IsPaid == false);
                return View(payments);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving the payments list.");
                TempData[ErrorMessage] = "Failed to load payments.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Pay(int id)
        {
            try
            {
                // Check if the payment exists
                if (!await paymentService.ExistsByIdAsync(id))
                {
                    TempData[WarningMessage] = "Payment not found!";
                    return RedirectToAction(nameof(All));
                }

                // Process the payment
                await paymentService.PayAsync(id);
                TempData[SuccessMessage] = "Payment successfully made!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing payment ID {PaymentId}.", id);
                TempData[ErrorMessage] = "Failed to process the payment.";
            }

            return RedirectToAction(nameof(All));
        }
    }
}
