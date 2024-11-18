using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Cart;
using EldenBoost.Core.Models.CartItem;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

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

            if (service!.ServiceType == ServiceType.Slider)
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

        public async Task ClearCartAsync(int cartId)
        {
            var cartItems = repository.All<CartItem>().Where(ci => ci.CartId == cartId);

            await repository.DeleteRangeAsync(cartItems);
        }

        public async Task<int> GetCartIdAsync(string userId)
        {
            return await repository.AllReadOnly<Cart>()
               .Where(c => c.ClientId == userId)
               .Select(c => c.Id)
               .FirstOrDefaultAsync();
        }

        public async Task<int> GetCartQuantityByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Cart>()
              .Where(c => c.ClientId == userId)
              .Select(c => c.CartItems.Count)
              .FirstOrDefaultAsync();
        }

        public async Task<CartViewModel> GetCartViewModelAsync(string userId)
        {
            var cart = await repository.AllReadOnly<Cart>()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Service)
                .FirstOrDefaultAsync(c => c.ClientId == userId);

            if (cart == null)
            {
                return new CartViewModel();
            }

            var cartItems = await repository.AllReadOnly<CartItem>()
                .Where(ci => ci.CartId == cart.Id)
                .Select(ci => new CartItemViewModel()
                {
                    Id = ci.Id,
                    ServiceTitle = ci.Service.Title,
                    Platform = ((PlatformType)ci.PlatformId).ToString(),
                    Price = ci.Price,
                    Information = ci.Information,
                    HasStream = ci.HasStream,
                    IsExpress = ci.IsExpress,
                    ServiceImage = ci.Service.ImageURL ?? string.Empty
                })
                .ToListAsync();

            var totalPrice = cartItems.Sum(i => i.Price);

            return new CartViewModel
            {
                CartItems = cartItems,
                TotalPrice = totalPrice
            };
        }

        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            var cartItem = await repository.GetByIdAsync<CartItem>(cartItemId);

            if (cartItem == null)
            {
                return false; 
            }

            await repository.DeleteAsync(cartItem);
            await repository.SaveChangesAsync();

            return true;
        }
    }
}
