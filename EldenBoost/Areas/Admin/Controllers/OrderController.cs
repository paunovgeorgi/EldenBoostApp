using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderService _orderService, ILogger<OrderController> _logger)
        {
            orderService = _orderService;
            logger = _logger;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            try
            {
                // Fetch the order details
                var order = await orderService.GetOrderDetailsAsync(orderId);
                if (order == null)
                {
                    logger.LogWarning("Order with ID {OrderId} not found.", orderId);
                    return NotFound("Order not found");
                }

                return PartialView("_OrderDetailsPartial", order);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching details for order ID {OrderId}.", orderId);
                return BadRequest("Failed to load order details.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> All(int? page)
        {
            try
            {
                // Retrieve all active orders
                var model = await orderService.AllOrdersFilteredAsync(o => !o.IsArchived);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving all orders.");
                TempData["ErrorMessage"] = "Failed to load orders.";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                // Archive the order
                await orderService.ArchiveAsync(id);
                TempData["SuccessMessage"] = "Order archived successfully.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while archiving order ID {OrderId}.", id);
                TempData["ErrorMessage"] = "Failed to archive the order.";
            }

            return RedirectToAction(nameof(All));
        }
    }
}
