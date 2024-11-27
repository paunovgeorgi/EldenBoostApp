using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderApiController : Controller
    {
        private readonly IOrderService orderService;
        public OrderApiController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        [HttpGet("getArchived")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderCardViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArchived()
        {
            try
            {
                var orders = await orderService.AllOrdersFilteredAsync(o => o.IsArchived);

                return Ok(orders);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("getActive")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderCardViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActive()
        {
            try
            {
                var orders = await orderService.AllOrdersFilteredAsync(o => !o.IsArchived);

                return Ok(orders);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

		[HttpGet("get-data")]
		[Produces("application/json")]
		public async Task<IActionResult> GetOrdersData()
        {
			try
			{
				var data = await orderService.GetOrderCountDataAsync();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Message = "An error occurred while processing your request.",
				});
			}

		}


		[HttpGet("get-pending")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderCardViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPending()
        {
           
            try
            {
                var result = await orderService.AllOrdersAsync();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
