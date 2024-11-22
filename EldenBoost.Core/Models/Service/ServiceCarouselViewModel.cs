using EldenBoost.Core.Models.Service.Contracts;

namespace EldenBoost.Core.Models.Service
{
    public class ServiceCarouselViewModel : IServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageURL { get; set; } = null!;
    }
}
