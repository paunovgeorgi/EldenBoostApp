using EldenBoost.Common.Enumerations;
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

        public async Task ApproveAuthorAsync(int applicationId)
        {
            Application? application = await repository.All<Application>()
               .Where(a => a.Id == applicationId)
               .FirstOrDefaultAsync();

            if (application != null)
            {
                application.IsApproved = true;
                Author author = new Author
                {
                    Country = application.Country,
                    UserId = application.UserId,
                };

                await repository.AddAsync<Author>(author);
                await repository.SaveChangesAsync();
            }
        }

        public async Task ApproveBoosterAsync(int applicationId)
        {
            Application? application = await repository.All<Application>()
               .Include(a => a.Platforms)
               .Where(a => a.Id == applicationId)
               .FirstOrDefaultAsync();

            if (application != null)
            {
                application.IsApproved = true;
                Booster booster = new Booster
                {
                    Country = application.Country,
                    UserId = application.UserId,
                };

                foreach (Platform platform in application.Platforms)
                {
                    Platform? currentPl = await repository.GetByIdAsync<Platform>(platform.Id);
                    if (currentPl != null)
                    {
                        booster.Platforms.Add(currentPl);
                        currentPl.Boosters.Add(booster);
                    }
                }

                await repository.AddAsync<Booster>(booster);
                await repository.SaveChangesAsync();
            }
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

        public async Task<string?> GetApplicantUserIdByApplicationIdAsync(int applicationId)
        {
            string? userId =  await repository.AllReadOnly<Application>()
                .Where(a => a.Id == applicationId)
                .Select(a => a.UserId)
                .FirstOrDefaultAsync();

            return userId;
        }

        public async Task<ApplicationCountDataModel> GetApplicationCountDataAsync()
		{
            int boosterPending = await repository.AllReadOnly<Application>().CountAsync(a => !a.IsApproved && a.ApplicationType == ApplicationType.Booster);
            int boosterApproved = await repository.AllReadOnly<Application>().CountAsync(a => a.IsApproved && a.ApplicationType == ApplicationType.Booster);
            int authorPending = await repository.AllReadOnly<Application>().CountAsync(a => !a.IsApproved && a.ApplicationType == ApplicationType.Author);
            int authorApproved = await repository.AllReadOnly<Application>().CountAsync(a => a.IsApproved && a.ApplicationType == ApplicationType.Author);

            return new ApplicationCountDataModel()
            {
                BoosterPending = boosterPending,
                BoosterApproved = boosterApproved,
                AuthorPending = authorPending,
                AuthorApproved = authorApproved
            };
		}

		public async Task<bool> HasAppliedByUserIdAsync(string userId, Expression<Func<Application, bool>> predicate)
        {
            return await repository.AllReadOnly<Application>()
              .Where(predicate)
              .AnyAsync(a => a.UserId == userId);
        }

        public async Task RejectAsync(int applicationId)
        {
            Application? application = await repository.GetByIdAsync<Application>(applicationId);

            if (application != null && application.IsApproved == false)
            {
                application.IsRejected = true;
                await repository.SaveChangesAsync();
            }
        }
    }
}
