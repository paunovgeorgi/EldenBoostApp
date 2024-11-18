using EldenBoost.Core.Models.CartItem;

namespace EldenBoost.Core.Models.Cart
{
    public class CartViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal TotalPrice { get; set; } 
    }
}
