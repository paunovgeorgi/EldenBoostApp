using EldenBoost.Core.Models.Article;
using EldenBoost.Infrastructure.Data.Models;
using System.Linq.Expressions;

namespace EldenBoost.Core.Contracts
{
    public interface IArticleService
    {
        Task<AllArticlesFilteredAndPagedModel> AllFilteredAndPagedAsync(AllArticlesQueryModel model);
        Task<IEnumerable<ArticleCardViewModel>> AllByAuthorIdAsync(int authorId);
		Task<ArticleReadViewModel?> GetArticleReadModelAsync(int articleId);
		Task CreateAsync(ArticleFormModel model, string userId);
        Task<ArticleEditViewModel?> GetArticleEditModelByIdAsync(int articleId);
        Task EditArticleAsync(ArticleEditViewModel model);
        Task<IEnumerable<ArticleListViewModel>> GetAllArticlesListViewModelAsync();
        Task<IEnumerable<ArticleCardViewModel>> LastThreeArticlesAsync(Expression<Func<Article, bool>> predicate);
        Task<ArticleCountDataModel> GetArticleCountDataAsync();
    }
}
