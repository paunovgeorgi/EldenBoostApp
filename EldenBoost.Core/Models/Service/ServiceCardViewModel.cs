using EldenBoost.Core.Models.Service.Contracts;

namespace EldenBoost.Core.Models.Service
{
    public class ServiceCardViewModel : IServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
        public string ImageURL { get; set; } = null!;
    }
}
