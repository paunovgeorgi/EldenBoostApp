using EldenBoost.Core.Models.Order;
using EldenBoost.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
	public interface IOrderService
	{
		Task CreateOrderAsync(int serviceId, string clientId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue);
        Task<IEnumerable<OrderCardViewModel>> AllOrdersAsync();
		Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderCardViewModel>> AllByUserIdAsync(string userId);
        Task<int> NumberOfOrdersByClientIdAsync(string userId);
        Task<decimal> TotalPaidByClientIdAsync(string clientId);
        Task<IEnumerable<OrderCardViewModel>> AllByBoosterIdAsync(int boosterId);
        Task<bool> ExistsByIdAsync(int orderId);
        Task<bool> IsTakenAsync(int orderId);
    }
}
