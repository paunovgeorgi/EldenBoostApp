﻿using EldenBoost.Core.Contracts;
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

        public async Task CreateOrderAsync(int serviceId, string clientId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue)
		{
			var service = await repository.GetByIdAsync<Service>(serviceId);
			string infoMessage = "";
			ServiceOption option;
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

		public async Task<Order?> GetOrderByIdAsync(int orderId)
		{
			return await repository.GetByIdAsync<Order>(orderId);
		}
	}
}