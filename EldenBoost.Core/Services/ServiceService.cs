using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Models.Service.Enums;
using EldenBoost.Core.Models.ServiceOption;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            IEnumerable<ServiceAllViewModel> allServices = await servicesQuery
                .Skip((model.CurrentPage - 1) * model.ServicesPerPage)
                .Take(model.ServicesPerPage)
                .Select(s => new ServiceAllViewModel()
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

        public async Task<IEnumerable<ServiceAllViewModel>> GetPopularServicesAsync()
        {
            return await repository.AllReadOnly<Service>()
               .Where(s => s.IsActive)
               .OrderByDescending(s => s.PurchaseCount)
               .Take(8)
              .Select(s => new ServiceAllViewModel()
              {
                  Id = s.Id,
                  Title = s.Title,
                  Price = s.Price,
                  ImageURL = s.ImageURL ?? string.Empty
              })
              .ToListAsync();
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

        public async Task<ServiceAllViewModel?> GetServiceViewModelByIdAsync(int serviceId)
        {
            var service = await repository.AllReadOnly<Service>()
               .Where(s => s.Id == serviceId)
               .Select(s => new ServiceAllViewModel()
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
    }
}
