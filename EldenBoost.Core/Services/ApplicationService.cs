﻿using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EldenBoost.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository repository;
        public ApplicationService(IRepository _repository)
        {
            repository = _repository; 
        }

        public async Task<IEnumerable<ApplicationListViewModel>> AllAsync(Expression<Func<Application, bool>> predicate)
        {
            return await repository.AllReadOnly<Application>()
               .Include(a => a.Platforms)
               .Where(predicate)
               .Select(a => new ApplicationListViewModel()
               {
                   Id = a.Id,
                   Nickname = a.User.Nickname,
                   Email = a.User.Email!,
                   Country = a.Country,
                   Availability = a.Availability,
                   Experience = a.Experience,
                   IsApproved = a.IsApproved,
                   IsRejected = a.IsRejected,
                   Platforms = string.Join(", ", a.Platforms.Select(p => p.Name))
               })
               .OrderByDescending(a => a.Id)
               .ToListAsync();
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
