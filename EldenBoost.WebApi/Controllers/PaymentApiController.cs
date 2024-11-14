using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentApiController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentApiController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpGet("getPaid")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPaid()
        {
            try
            {
                var payments = await paymentService.AllPaymentsFiltered(p => p.IsPaid);

                return Ok(payments);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("getPending")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPending()
        {
            try
            {
                var payments = await paymentService.AllPaymentsFiltered(p => !p.IsPaid);

                return Ok(payments);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
