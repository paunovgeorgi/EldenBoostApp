using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Order;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.Service.Enums;
using EldenBoost.Core.Models.ServiceOption;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EldenBoost.Core.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IRepository repository;
        public ServiceService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<AllServicesFilteredAndPagedModel> AllAsync(AllServicesQueryModel model)
        {
            IQueryable<Service> servicesQuery = repository.AllReadOnly<Service>()
               .Where(s => s.IsActive)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                string wildCard = $"%{model.SearchString.ToLower()}%";
                servicesQuery = servicesQuery.Where(s => EF.Functions.Like(s.Title, wildCard) || EF.Functions.Like(s.Description, wildCard));
            }

            servicesQuery = model.ServiceSorting switch
            {
                ServiceSorting.Newest => servicesQuery.OrderByDescending(s => s.Id),
                ServiceSorting.Oldest => servicesQuery.OrderBy(s => s.Id),
                ServiceSorting.PriceAscending => servicesQuery.OrderBy(s => s.Price),
                ServiceSorting.PriceDescending => servicesQuery.OrderByDescending(s => s.Price),
                _ => throw new NotImplementedException()
            };

            IEnumerable<ServiceCardViewModel> allServices = await servicesQuery
                .Skip((model.CurrentPage - 1) * model.ServicesPerPage)
                .Take(model.ServicesPerPage)
                .Select(s => new ServiceCardViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    ImageURL = s.ImageURL ?? string.Empty,
                    Price = s.Price
                })
                .ToListAsync();

            int totalServices = servicesQuery.Count();

            return new AllServicesFilteredAndPagedModel()
            {
                TotalServicesCount = totalServices,
                Services = allServices
            };
        }

        public async Task<IEnumerable<ServiceListViewModel>> AllServiceListViewModelFilteredAsync(Expression<Func<Service, bool>> predicate)
        {
            var servicesQuery = repository.AllReadOnly<Service>()
               .Where(predicate)
               .Select(s => new ServiceListViewModel()
               {
                   Id = s.Id,
                   Title = s.Title,
                   IsActive = s.IsActive,
                   PurchaseCount = s.PurchaseCount,
                   Price = s.Price
               })
               .OrderByDescending(s => s.Id);

            return await servicesQuery.ToListAsync();
        }

        public async Task CreateServiceAsync(ServiceFormViewModel model)
        {
            Service service = new Service()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ImageURL = model.ImageURL ?? string.Empty,
                ServiceType = model.ServiceType,
                MaxAmount = model.MaxAmount ?? 0
            };

            if (model.ServiceType == ServiceType.Option)
            {
                foreach (var option in model.ServiceOptions)
                {
                    service.Options.Add(new ServiceOption
                    {
                        Name = option.Name,
                        Price = option.Price
                    });
                }
            }

            await repository.AddAsync(service);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int serviceId)
        {
            Service? service = await repository.GetByIdAsync<Service>(serviceId);

            if (service != null)
            {
                service.IsActive = false;
                await repository.SaveChangesAsync();
            }
        }

        public async Task EditAsync(ServiceEditViewModel model)
        {
            Service? service = await repository.All<Service>()
               .Include(s => s.Options)
               .Where(s => s.Id == model.Id)
               .FirstOrDefaultAsync();

            if (service == null)
            {
                throw new ArgumentException("Service not found");
            }

            service.Title = model.Title;
            service.Description = model.Description;
            service.Price = model.Price;
            service.ImageURL = model.ImageURL;
            service.MaxAmount = model.MaxAmount ?? 0;

            if (service.ServiceType == ServiceType.Option)
            {
                service.Options.Clear();

                foreach (var option in model.ServiceOptions)
                {
                    service.Options.Add(new ServiceOption
                    {
                        Name = option.Name,
                        Price = option.Price
                    });
                }
            }

            await repository.SaveChangesAsync();
        }

		public async Task<bool> ExistsByIdAsync(int serviceId)
		{
			return await repository.AllReadOnly<Service>()
				.AnyAsync(s => s.Id == serviceId);
		}

		public async Task<IEnumerable<ServiceCardViewModel>> GetPopularServicesAsync()
        {
            return await repository.AllReadOnly<Service>()
               .Where(s => s.IsActive)
               .OrderByDescending(s => s.PurchaseCount)
               .Take(8)
              .Select(s => new ServiceCardViewModel()
              {
                  Id = s.Id,
                  Title = s.Title,
                  Price = s.Price,
                  ImageURL = s.ImageURL ?? string.Empty
              })
              .ToListAsync();
        }

		public async Task<ServiceDetailsViewModel?> GetServiceDetailsViewModelByIdAsync(int serviceId)
		{
			var service = await repository.AllReadOnly<Service>()
			  .Where(s => s.Id == serviceId)
			  .Select(s => new ServiceDetailsViewModel()
			  {
				  Id = s.Id,
				  Title = s.Title,
				  Description = s.Description,
				  ImageURL = s.ImageURL ?? string.Empty,
				  Price = s.Price,
				  ServiceType = s.ServiceType,
				  MaxAmount = s.MaxAmount
			  })
			  .FirstOrDefaultAsync();

			return service;
		}

		public async Task<ServiceEditViewModel?> GetServiceEditViewModelByIdAsync(int serviceId)
        {
            var service = await repository.AllReadOnly<Service>()
              .Where(s => s.Id == serviceId)
              .Select(s => new ServiceEditViewModel()
              {
                  Id = s.Id,
                  Title = s.Title,
                  Description = s.Description,
                  ImageURL = s.ImageURL,
                  Price = s.Price,
                  ServiceType = s.ServiceType,
                  MaxAmount = s.MaxAmount
              })
              .FirstOrDefaultAsync();

            return service;
        }

        public async Task<IEnumerable<ServiceOptionViewModel>> GetServiceOptionsAsync(int serviceId)
        {
            return await repository.AllReadOnly<ServiceOption>()
               .Where(so => so.ServiceId == serviceId)
               .Select(so => new ServiceOptionViewModel()
               {
                   Id = so.Id,
                   Name = so.Name,
                   Price = so.Price
               })
               .ToListAsync();
        }

        public async Task<ServiceCardViewModel?> GetServiceViewModelByIdAsync(int serviceId)
        {
            var service = await repository.AllReadOnly<Service>()
               .Where(s => s.Id == serviceId)
               .Select(s => new ServiceCardViewModel()
               {
                   Id = s.Id,
                   Title = s.Title,
                   Description = s.Description,
                   ImageURL = s.ImageURL ?? string.Empty,
                   Price = s.Price
               })
               .FirstOrDefaultAsync();
      
            return service;
        }
    }
}
