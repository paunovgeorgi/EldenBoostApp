using EldenBoost.Core.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, int serviceId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue);
        Task<int> GetCartQuantityByUserIdAsync(string userId);
        Task<CartViewModel> GetCartViewModelAsync(string userId);
        Task<bool> RemoveItemAsync(int cartItemId);
        Task<int> GetCartIdAsync(string userId);
        Task ClearCartAsync(int cartId);
    }
}
