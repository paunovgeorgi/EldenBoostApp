using System.ComponentModel.DataAnnotations;

namespace EldenBoost.Core.Models.Order
{
    public class OrderCardViewModel
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public string Status { get; set; }

        public decimal BoosterPay { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }

        public string ImageURL { get; set; }

        public string AssignedTo { get; set; } = "Pending";

        [Display(Name = "Time Of Purchase")]
        public string TimeOfPurchase { get; set; }

        public bool IsArchived { get; set; }

        public bool IsRated { get; set; }

        public decimal Price { get; set; }

        public bool HasStream { get; set; }

        public bool IsExpress { get; set; }

        public string? Information { get; set; }
    }
}
