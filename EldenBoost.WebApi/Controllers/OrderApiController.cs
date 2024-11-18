using EldenBoost.Core.Contracts;
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


        [HttpGet("get-pending")]
        [Produces("application/json")]
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
