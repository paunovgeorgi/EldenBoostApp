using EldenBoost.Core.Models.Article;

namespace EldenBoost.Core.Contracts
{
    public interface IArticleService
    {
        Task<AllArticlesFilteredAndPagedModel> AllFilteredAndPagedAsync(AllArticlesQueryModel model);
    }
}
