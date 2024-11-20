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

        public async Task<IEnumerable<BoosterCardViewModel>> AllActiveBoostersToCardModelAsync()
        {
            return await repository.AllReadOnly<Booster>()
                .Where(b => !b.IsDemoted)
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

		public async Task<Booster?> GetBoosterByBoosterIdAsync(int boosterId)
		{
			return await repository.AllReadOnly<Booster>()
			   .Where(b => b.Id == boosterId)
			   .Include(b => b.User)
			   .FirstOrDefaultAsync();
		}

        public async Task<Booster?> GetBoosterByUserIdAsync(string userId)
        {
            var booster = await repository.AllReadOnly<Booster>()
              .Include(b => b.Platforms)
              .Include(b => b.Orders)
              .Where(b => b.UserId == userId)
              .FirstOrDefaultAsync();

            return booster;
        }

        public async Task<int> GetBoosterIdAsync(string userId)
        {
            return await repository.AllReadOnly<Booster>()
                .Where(b => b.UserId == userId)
                .Select(b => b.Id)
                .FirstOrDefaultAsync();
        }

		public async Task<bool> IsActiveAsync(string userId)
		{
			return await repository.AllReadOnly<Booster>()
			   .AnyAsync(b => b.UserId == userId && !b.IsDemoted);
		}

		public async Task RateAsync(int boosterId, int rating)
        {
            Booster? booster = await repository.GetByIdAsync<Booster>(boosterId);

            if (booster != null)
            {
                booster.Rating += rating;
                booster.RatingCount++;
                await repository.SaveChangesAsync();
            }
        }
    }
}
