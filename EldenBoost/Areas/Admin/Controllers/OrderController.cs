using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var order = await orderService.GetOrderDetailsAsync(orderId); 
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return PartialView("_OrderDetailsPartial", order);
        }
    }
}
