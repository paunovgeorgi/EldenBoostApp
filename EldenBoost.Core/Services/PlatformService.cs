using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Platform;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IRepository repository;

        public PlatformService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<IEnumerable<PlatformFormModel>> GetAllPlatformsAsync()
        {
            return await repository.AllReadOnly<Platform>()
               .Select(p => new PlatformFormModel()
               {
                   Id = p.Id,
                   Name = p.Name,
               })
               .ToListAsync();
        }

    }
}
