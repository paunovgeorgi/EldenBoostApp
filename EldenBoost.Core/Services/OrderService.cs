using EldenBoost.Common.ReturnMessages;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Order;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EldenBoost.Core.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository repository;
        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<OrderCardViewModel>> AllByBoosterIdAsync(int boosterId)
        {
            return await repository.AllReadOnly<Order>()
               .Where(o => o.BoosterId == boosterId)
              .Select(o => new OrderCardViewModel()
              {
                  Id = o.Id,
                  PlatformId = o.PlatformId,
                  PlatformName = o.Platform.Name,
                  Status = o.Status,
                  ServiceName = o.Service.Title,
                  ImageURL = o.Service.ImageURL ?? string.Empty,
                  BoosterPay = o.Price / 2,
                  HasStream = o.HasStream,
                  IsExpress = o.IsExpress,
                  Information = o.Information
              })
              .ToListAsync();
        }

        public async Task<IEnumerable<OrderCardViewModel>> AllByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Order>()
               .Where(o => o.ClientId == userId)
              .Select(o => new OrderCardViewModel()
              {
                  Id = o.Id,
                  PlatformId = o.PlatformId,
                  PlatformName = o.Platform.Name,
                  Status = o.Status,
                  ServiceName = o.Service.Title,
                  ImageURL = o.Service.ImageURL ?? string.Empty,
                  BoosterPay = o.Service.Price,
                  IsRated = o.IsRated,
                  Price = o.Price,
                  HasStream = o.HasStream,
                  IsExpress = o.IsExpress,
                  Information = o.Information
              })
              .ToListAsync();
        }

        public async Task<IEnumerable<OrderCardViewModel>> AllOrdersAsync()
        {
            var orders = await repository.AllReadOnly<Order>()
                .Where(o => o.IsArchived == false)
                .Select(o => new OrderCardViewModel()
                {
                    Id = o.Id,
                    PlatformId = o.PlatformId,
                    PlatformName = o.Platform.Name,
                    Status = o.Status,
                    ServiceName = o.Service.Title,
                    ImageURL = o.Service.ImageURL ?? string.Empty,
                    BoosterPay = o.Price / 2,
                    TimeOfPurchase = o.TimeOfPurchase.ToLongDateString(),
                    HasStream = o.HasStream,
                    IsExpress = o.IsExpress,
                    Information = o.Information
                })
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            foreach (var order in orders)
            {
                Order? currentOrder = await repository.GetByIdAsync<Order>(order.Id);

                if (currentOrder!.BoosterId != null)
                {
                    Booster? booster = await repository.AllReadOnly<Booster>()
                        .Include(b => b.User)
                        .Where(b => b.Id == currentOrder.BoosterId)
                        .FirstOrDefaultAsync();

                    if (booster != null)
                    {
                        order.AssignedTo = booster.User.Nickname;
                    }
                }
            }

            return orders;
        }

        public async Task<IEnumerable<OrderCardViewModel>> AllOrdersFilteredAsync(Expression<Func<Order, bool>> predicate)
        {
            var ordersQuery = repository.AllReadOnly<Order>()
               .Where(predicate) 
               .Select(o => new OrderCardViewModel()
               {
                   Id = o.Id,
                   PlatformId = o.PlatformId,
                   PlatformName = o.Platform.Name,
                   Status = o.Status,
                   ServiceName = o.Service.Title,
                   ImageURL = o.Service.ImageURL ?? string.Empty,
                   BoosterPay = o.Price / 2,
                   TimeOfPurchase = o.TimeOfPurchase.ToLongDateString(),
                   IsArchived = o.IsArchived
               })
               .OrderByDescending(x => x.Id);

            var orders = await ordersQuery.ToListAsync(); 

            foreach (var order in orders)
            {
                Order? currentOrder = await repository.GetByIdAsync<Order>(order.Id);

                if (currentOrder?.BoosterId != null)
                {
                    Booster? booster = await repository.AllReadOnly<Booster>()
                        .Include(b => b.User)
                        .Where(b => b.Id == currentOrder.BoosterId)
                        .FirstOrDefaultAsync();

                    if (booster != null)
                    {
                        order.AssignedTo = booster.User.Nickname;
                    }
                }
            }

            return orders;
        }

        public async Task ArchiveAsync(int orderId)
        {
            Order? order = await repository.GetByIdAsync<Order>(orderId);

            if (order != null)
            {
                order.IsArchived = true;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<AssignOrderResult> AssignBoosterAsync(int orderId, int boosterId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);
            var booster = await repository.AllReadOnly<Booster>()
                .Where(b => b.Id == boosterId)
                .Include(b => b.Orders)
                .FirstOrDefaultAsync();

            if (order == null || booster == null)
            {
                return new AssignOrderResult { Success = false, Message = "Order or Booster not found!" };
            }

            if (booster.Orders.Any(o => o.Status != "Completed" && o.ClientId != order.ClientId))
            {
                return new AssignOrderResult { Success = false, Message = "You have an ongoing order from another client!" };
            }

            order.BoosterId = boosterId;
            order.Status = "Working";
            await repository.SaveChangesAsync();
            return new AssignOrderResult { Success = true, Message = "Order successfully assigned to your profile!" };
        }

        public async Task CompleteAsync(int orderId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);

            if (order != null)
            {
                order.Status = "Completed";
                await repository.SaveChangesAsync();
            }
        }

        public async Task CreateOrderAsync(int serviceId, string clientId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue)
		{
			var service = await repository.GetByIdAsync<Service>(serviceId);
			string infoMessage = "";
			ServiceOption? option;
			if (optionId != null)
			{
				option = await repository.GetByIdAsync<ServiceOption>(optionId);
				infoMessage = $"Selected option: {option!.Name}";
			}

			if (service != null)
			{
				if (service.ServiceType == Common.Enumerations.ServiceType.Slider)
				{
					infoMessage = $"Selected amount: {sliderValue}";
				}
				var price = updatedPrice != null ? (decimal)updatedPrice : service.Price;
				Order order = new Order()
				{
					Status = "Pending",
					ServiceId = serviceId,
					ClientId = clientId,
					PlatformId = platformId,
					TimeOfPurchase = DateTime.Now,
					BoosterPay = price / 2,
					Price = price,
					HasStream = hasStream ?? false,
					IsExpress = isExpress ?? false,
					Information = infoMessage
				};

				service.PurchaseCount++;

				await repository.AddAsync(order);
				await repository.SaveChangesAsync();
			}
		}

        public async Task CreateOrdersFromCartAsync(int cartId, string clientId)
        {
            var cartItems = await repository.All<CartItem>().Where(ci => ci.CartId == cartId)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                Order order = new Order
                {
                    Status = "Pending",
                    ServiceId = item.ServiceId,
                    ClientId = clientId,
                    PlatformId = item.PlatformId,
                    TimeOfPurchase = DateTime.Now,
                    BoosterPay = item.Price / 2,
                    Price = item.Price,
                    HasStream = item.HasStream ?? false,
                    IsExpress = item.IsExpress ?? false,
                    Information = item.Information
                };
                var service = await repository.GetByIdAsync<Service>(item.ServiceId);
                service!.PurchaseCount++;
                await repository.AddAsync(order);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int orderId)
        {
            return await repository.AllReadOnly<Order>()
                .AnyAsync(o => o.Id == orderId);
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
		{
			return await repository.GetByIdAsync<Order>(orderId);
		}

		public async Task<OrderCountDataModel> GetOrderCountDataAsync()
		{
            int pending = await repository.AllReadOnly<Order>().CountAsync(o => o.Status == "Pending");
            int working = await repository.AllReadOnly<Order>().CountAsync(o => o.Status == "Working");
            int completed = await repository.AllReadOnly<Order>().CountAsync(o => o.Status == "Completed");
            int total = await repository.AllReadOnly<Order>().CountAsync();

            return new OrderCountDataModel()
            {
                Pending = pending,
                Working = working,
                Completed = completed,
                Total = total
            };
		}

		public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId)
        {
            return await repository.AllReadOnly<Order>()
               .Where(o => o.Id == orderId)
               .Select(o => new OrderDetailsViewModel()
               {
                   Id = o.Id,
                   ServiceName = o.Service.Title,
                   BoosterPay = o.BoosterPay,
                   ImageURL = o.Service.ImageURL ?? string.Empty,
                   HasStream = o.HasStream,
                   IsExpress = o.IsExpress,
                   Platform = o.Platform.Name
               })
               .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetOrderWithBoosterByOrderIdAsync(int orderId)
        {
            Order? order = await repository.AllReadOnly<Order>()
               .Where(o => o.Id == orderId)
               .Include(o => o.Booster)
               .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Order?> GetOrderWithClientByIdAsync(int orderId)
        {
            return await repository.AllReadOnly<Order>()
                .Include(o => o.Client)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasBoosterWithIdAsync(int orderId, int boosterId)
        {

            Order? order = await repository.GetByIdAsync<Order>(orderId);

            if (order == null)
            {
                return false;
            }
            return order!.BoosterId == boosterId;
        }

        public async Task<bool> IsTakenAsync(int orderId)
        {
            Order? order = await repository.GetByIdAsync<Order>(orderId);

            if (order == null)
            {
                return false;
            }

            return order.BoosterId != null;
        }

        public async Task<int> NumberOfOrdersByClientIdAsync(string userId)
        {
            return await repository.AllReadOnly<Order>()
                .CountAsync(o => o.ClientId == userId);
        }

        public async Task RateAsync(int orderId)
        {
            Order? order = await repository.GetByIdAsync<Order>(orderId);

            if (order != null)
            {
                order.IsRated = true;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<decimal> TotalPaidByClientIdAsync(string clientId)
        {
            return await repository.AllReadOnly<Order>()
                .Include(o => o.Service)
                .Where(o => o.ClientId == clientId)
                .SumAsync(o => o.Price);
        }
    }
}
