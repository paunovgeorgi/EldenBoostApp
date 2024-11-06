using EldenBoost.Core.Models.Booster;

namespace EldenBoost.Core.Contracts
{
    public interface IBoosterService
    {
        Task<bool> BoosterExistsByUserIdAsync(string userId);
        Task<IEnumerable<BoosterCardViewModel>> AllBoostersToCardModelAsync();
    }
}
