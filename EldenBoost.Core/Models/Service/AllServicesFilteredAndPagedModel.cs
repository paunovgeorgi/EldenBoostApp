namespace EldenBoost.Core.Models.Service
{
    public class AllServicesFilteredAndPagedModel
    {
        public int TotalServicesCount { get; set; }

        public IEnumerable<ServiceCardViewModel> Services { get; set; } = new List<ServiceCardViewModel>();
    }
}
