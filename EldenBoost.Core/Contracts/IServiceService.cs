using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IServiceService
    {
        Task<AllServicesFilteredAndPagedModel> AllAsync(AllServicesQueryModel model);
        Task<IEnumerable<ServiceCardViewModel>> GetPopularServicesAsync();
        Task<ServiceCardViewModel?> GetServiceViewModelByIdAsync(int serviceId);
        Task<IEnumerable<ServiceOptionViewModel>> GetServiceOptionsAsync(int serviceId);
        Task CreateServiceAsync(ServiceFormViewModel model);
        Task<ServiceEditViewModel?> GetServiceEditViewModelByIdAsync(int serviceId);
        Task EditAsync(ServiceEditViewModel model);
        Task DeleteByIdAsync(int serviceId);

    }
}
