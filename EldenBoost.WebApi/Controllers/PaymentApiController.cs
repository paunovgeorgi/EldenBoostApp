using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Core.Models.Payment;
using EldenBoost.Core.Services;
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
        [ProducesResponseType(typeof(PaymentListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(typeof(PaymentListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

		[HttpGet("get-data")]
		[Produces("application/json")]
		public async Task<IActionResult> GetPaymentData()
		{
			try
			{
				var data = await paymentService.GetPaymentCountDataAsync();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Message = ex
				});
			}

		}
	}
}
