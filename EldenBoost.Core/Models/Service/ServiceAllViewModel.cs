using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Models.ServiceOption;

namespace EldenBoost.Core.Models.Service
{
    public class ServiceAllViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageURL { get; set; } = null!;

        public ServiceType ServiceType { get; set; }

        public int MaxAmount { get; set; }

        public ICollection<ServiceOptionViewModel> Options { get; set; } = new List<ServiceOptionViewModel>();
    }
}
