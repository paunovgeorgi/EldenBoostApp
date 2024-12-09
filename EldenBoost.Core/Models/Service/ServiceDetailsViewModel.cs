using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Models.ServiceOption;

namespace EldenBoost.Core.Models.Service
{
    public class ServiceDetailsViewModel : ServiceCardViewModel
	{
		public ServiceType ServiceType { get; set; }

		public int MaxAmount { get; set; }

		public ICollection<ServiceOptionViewModel> Options { get; set; } = new List<ServiceOptionViewModel>();
	}
}
