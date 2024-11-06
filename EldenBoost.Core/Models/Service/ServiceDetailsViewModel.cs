using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Models.ServiceOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.Service
{
	public class ServiceDetailsViewModel : ServiceCardViewModel
	{
		public ServiceType ServiceType { get; set; }

		public int MaxAmount { get; set; }

		public ICollection<ServiceOptionViewModel> Options { get; set; } = new List<ServiceOptionViewModel>();
	}
}
