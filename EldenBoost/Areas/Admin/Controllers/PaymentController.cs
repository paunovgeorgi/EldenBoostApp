using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

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
    }
}
