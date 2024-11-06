using EldenBoost.Core.Models.Booster;
using EldenBoost.Infrastructure.Data.Models;

namespace EldenBoost.Core.Contracts
{
    public interface IBoosterService
    {
        Task<bool> BoosterExistsByUserIdAsync(string userId);
        Task<IEnumerable<BoosterCardViewModel>> AllBoostersToCardModelAsync();
        Task<Booster?> GetBoosterByUserIdAsync(string userId);
    }
}
