using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class PaymentController : BaseAdminController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var payments = await paymentService.AllPaymentsFiltered(p => p.IsPaid == false);

            return View(payments);
        }

        [HttpPost]

        public async Task<IActionResult> Pay(int id)
        {
            if (await paymentService.ExistsByIdAsync(id))
            {
                TempData[WarningMessage] = "Payment not found!";
                return RedirectToAction(nameof(All));
            }

            await paymentService.PayAsync(id);
            TempData[SuccessMessage] = "Payment successfully made!";
            return RedirectToAction(nameof(All));
        }
    }
}
