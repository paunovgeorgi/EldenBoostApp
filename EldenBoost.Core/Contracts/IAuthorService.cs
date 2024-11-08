namespace EldenBoost.Core.Contracts
{
    public interface IAuthorService
    {
        Task<bool> ExistsByUserIdAsync(string userId);
		Task<bool> HasArticleAsync(string userId, int articleId);
	}
}
