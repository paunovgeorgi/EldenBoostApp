namespace EldenBoost.Core.Contracts
{
    public interface IAuthorService
    {
        Task<bool> ExistsByUserIdAsync(string userId);
    }
}
