namespace EldenBoost.Core.Models.CartItem
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string ServiceTitle { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public decimal Price { get; set; } 
        public string? Information { get; set; } 
        public bool? HasStream { get; set; } 
        public bool? IsExpress { get; set; }
        public string ServiceImage { get; set; } = null!;
    }
}
