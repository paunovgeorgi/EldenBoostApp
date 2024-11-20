namespace EldenBoost.Core.Models.Service
{
    public class ServiceListViewModel
    {
        public int Id { get; set; }

        public string ImageURL { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public int PurchaseCount { get; set; }
    }
}
