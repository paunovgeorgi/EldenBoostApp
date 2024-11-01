namespace EldenBoost.Core.Models.Service
{
    public class AllServicesFilteredAndPagedModel
    {
        public int TotalServicesCount { get; set; }

        public IEnumerable<ServiceAllViewModel> Services { get; set; } = new List<ServiceAllViewModel>();
    }
}
