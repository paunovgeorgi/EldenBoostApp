using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository repository;
        public ApplicationService(IRepository _repository)
        {
            repository = _repository; 
        }

        public async Task CreateAuthorApplicationAsync(string userId, ApplicationFormModel model)
        {
            Application application = new Application()
            {
                Country = model.Country,
                UserId = userId,
                Experience = model.Experience,
                Availability = model.Availabiliity,
                ApplicationType = ApplicationType.Author
            };

            await repository.AddAsync(application);
            await repository.SaveChangesAsync();
        }

        public async Task CreateBoosterApplicationAsync(string userId, ApplicationFormModel model)
        {
            Application application = new Application()
            {
                Country = model.Country,
                UserId = userId,
                Experience = model.Experience,
                Availability = model.Availabiliity,
                ApplicationType = ApplicationType.Booster
            };

            foreach (var platform in model.SupportedPlatforms.Where(p => p.IsChecked))
            {
                var platformEntity = await repository.GetByIdAsync<Platform>(platform.Id);
                if (platformEntity != null)
                {
                    application.Platforms.Add(platformEntity);
                }
            }

            await repository.AddAsync(application);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> HasAppliedByUserIdAsync(string userId, Expression<Func<Application, bool>> predicate)
        {
            return await repository.AllReadOnly<Application>()
              .Where(predicate)
              .AnyAsync(a => a.UserId == userId);
        }
    }
}
