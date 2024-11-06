using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
	public interface IOrderService
	{
		Task CreateOrderAsync(int serviceId, string clientId, int platformId, decimal? updatedPrice, bool? hasStream, bool? isExpress, int? optionId, int sliderValue);
	}
}
