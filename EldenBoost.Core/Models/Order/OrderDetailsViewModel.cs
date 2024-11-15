namespace EldenBoost.Core.Models.Order
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public string ServiceName { get; set; } = null!;

        public decimal BoosterPay { get; set; }

        public string ImageURL { get; set; } = null!;

        public bool HasStream { get; set; }

        public bool IsExpress { get; set; }

        public string Platform { get; set; } = null!;
    }
}
