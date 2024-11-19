﻿using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.ServiceOption;
using EldenBoost.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Contracts
{
    public interface IServiceService
    {
        Task<AllServicesFilteredAndPagedModel> AllAsync(AllServicesQueryModel model);
        Task<IEnumerable<ServiceCardViewModel>> GetPopularServicesAsync();
        Task<ServiceCardViewModel?> GetServiceViewModelByIdAsync(int serviceId);
		Task<ServiceDetailsViewModel?> GetServiceDetailsViewModelByIdAsync(int serviceId);
		Task<IEnumerable<ServiceOptionViewModel>> GetServiceOptionsAsync(int serviceId);
        Task CreateServiceAsync(ServiceFormViewModel model);
        Task<ServiceEditViewModel?> GetServiceEditViewModelByIdAsync(int serviceId);
        Task EditAsync(ServiceEditViewModel model);
        Task DeleteByIdAsync(int serviceId);
		Task<bool> ExistsByIdAsync(int serviceId);
        Task<IEnumerable<ServiceListViewModel>> AllServiceListViewModelFilteredAsync(Expression<Func<Service, bool>> predicate);

    }
}
