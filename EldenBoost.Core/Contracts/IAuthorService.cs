using EldenBoost.Infrastructure.Data.Models;

namespace EldenBoost.Core.Contracts
{
    public interface IAuthorService
    {
        Task<bool> ExistsByUserIdAsync(string userId);
		Task<bool> HasArticleAsync(string userId, int articleId);
        Task<Author?> GetAuthorByUserIdAsync(string userId);
        Task<bool> IsActiveAsync(string userId);

    }
}
