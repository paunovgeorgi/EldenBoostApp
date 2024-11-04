namespace EldenBoost.Core.Contracts
{
    public interface IBoosterService
    {
        Task<bool> BoosterExistsByUserIdAsync(string userId);
    }
}
