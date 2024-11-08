using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Core.Models.Article.Enums;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository repository;
        public ArticleService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<AllArticlesFilteredAndPagedModel> AllFilteredAndPagedAsync(AllArticlesQueryModel model)
        {
            IQueryable<Article> articleQuery = repository.AllReadOnly<Article>()
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                string wildCard = $"%{model.SearchString.ToLower()}%";
                articleQuery = articleQuery.Where(s => EF.Functions.Like(s.Title, wildCard) || EF.Functions.Like(s.Content, wildCard));
            }

            articleQuery = model.ArticleSorting switch
            {
                ArticleSorting.Newest => articleQuery.OrderByDescending(s => s.Id),
                ArticleSorting.Oldest => articleQuery.OrderBy(s => s.Id),
                ArticleSorting.Type => articleQuery.OrderBy(s => s.ArticleType).ThenByDescending(s => s.Id)
            };

            IEnumerable<ArticleCardViewModel> allArticles = await articleQuery
                .Skip((model.CurrentPage - 1) * model.ArticlesOnPage)
                .Take(model.ArticlesOnPage)
                .Select(s => new ArticleCardViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    ArticleType = s.ArticleType,
                    ImageURL = s.ImageURL ?? string.Empty,
                })
                .ToListAsync();

            int totalArticles = articleQuery.Count();

            return new AllArticlesFilteredAndPagedModel()
            {
                TotalArticleCount = totalArticles,
                Articles = allArticles
            };
        }
    }
}
