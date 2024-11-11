using EldenBoost.Core.Models.Article;

namespace EldenBoost.Core.Contracts
{
    public interface IArticleService
    {
        Task<AllArticlesFilteredAndPagedModel> AllFilteredAndPagedAsync(AllArticlesQueryModel model);
        Task<IEnumerable<ArticleCardViewModel>> AllByAuthorIdAsync(int authorId);
		Task<ArticleReadViewModel?> GetArticleReadModelAsync(int articleId);
		Task CreateAsync(ArticleFormModel model, string userId);
	}
}
