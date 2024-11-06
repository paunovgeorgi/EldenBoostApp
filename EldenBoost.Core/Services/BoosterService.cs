using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Booster;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class BoosterService : IBoosterService
    {
        private readonly IRepository repository;
        public BoosterService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<BoosterCardViewModel>> AllBoostersToCardModelAsync()
        {
            return await repository.AllReadOnly<Booster>()
               .Include(b => b.User)
               .Include(b => b.Platforms)
               .Select(b => new BoosterCardViewModel()
               {
                   Name = b.User.Nickname,
                   ProfilePicture = b.User.ProfilePicture ?? string.Empty,
                   SupportedPlatforms = string.Join(", ", b.Platforms.Select(p => p.Name).ToArray()),
                   Rating = b.Rating,
                   RatingCount = b.RatingCount
               })
               .ToListAsync();
        }

        public async Task<bool> BoosterExistsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Booster>().AnyAsync(u => u.UserId == userId);
        }
    }
}
