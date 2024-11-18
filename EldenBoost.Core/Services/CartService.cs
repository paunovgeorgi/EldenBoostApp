using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repository;
        public CartService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task AddToCartAsync(string userId, int serviceId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue)
        {
            var cart = await repository.All<Cart>()
            .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Service)
           .FirstOrDefaultAsync(c => c.ClientId == userId);

            if (cart == null)
            {
                cart = new Cart { ClientId = userId };
                await repository.AddAsync(cart);
                await repository.SaveChangesAsync();
            }

            var service = await repository.GetByIdAsync<Service>(serviceId);
            string infoMessage = "";
            ServiceOption option;
            if (optionId != null)
            {
                option = await repository.GetByIdAsync<ServiceOption>(optionId);
                infoMessage = $"Selected option: {option!.Name}";
            }

            if (service!.ServiceType == Common.Enumerations.ServiceType.Slider)
            {
                infoMessage = $"Selected amount: {sliderValue}";
            }
            var price = updatedPrice != null ? (decimal)updatedPrice : service.Price;

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ServiceId = serviceId,
                PlatformId = platformId,
                Price = price,
                HasStream = hasStream,
                IsExpress = isExpress,
                OptionId = optionId,
                SliderValue = sliderValue,
                Information = infoMessage
            };

            cart.CartItems.Add(cartItem);
            await repository.SaveChangesAsync();
        }
    }
}
