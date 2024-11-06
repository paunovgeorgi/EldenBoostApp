using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Core.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepository repository;
        public OrderService(IRepository _repository)
        {
            repository = _repository;
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
	}
}
