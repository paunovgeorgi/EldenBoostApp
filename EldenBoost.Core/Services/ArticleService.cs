using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Core.Models.Article.Enums;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EldenBoost.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository repository;
        public ArticleService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<ArticleCardViewModel>> AllByAuthorIdAsync(int authorId)
        {
            var articles = await repository.AllReadOnly<Article>()
                .Where(a => a.AuthorId == authorId)
                .Select(a => new ArticleCardViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    ArticleType = a.ArticleType,
                    ImageURL = a.ImageURL
                })
                .ToListAsync();

             return articles;
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

		public async Task CreateAsync(ArticleFormModel model, string userId)
		{
			int authorId = await repository.AllReadOnly<Author>()
				.Where(a => a.UserId == userId)
				.Select(a => a.Id)
				.FirstOrDefaultAsync();

            var sanitizer = new HtmlSanitizer();
            var decodedContent = WebUtility.HtmlDecode(model.Content);
            string sanitizedContent = sanitizer.Sanitize(decodedContent);

            Article article = new Article
            {
                Title = model.Title,
                Content = sanitizedContent,
                AuthorId = authorId,
                ImageURL = model.ImageURL,
                ArticleType = model.ArticleType,
                ReleaseDate = DateTime.Now
            };

            await repository.AddAsync(article);
			await repository.SaveChangesAsync();
		}

        public async Task EditArticleAsync(ArticleEditViewModel model)
        {
            Article? article = await repository.GetByIdAsync<Article>(model.Id);
            if (article != null)
            {
                var sanitizer = new HtmlSanitizer();
                var decodedContent = WebUtility.HtmlDecode(model.Content);
                string sanitizedContent = sanitizer.Sanitize(decodedContent);

                article.Title = model.Title;
                article.Content = sanitizedContent;
                article.ImageURL = model.ImageURL;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ArticleListViewModel>> GetAllArticlesListViewModelAsync()
        {
            return await repository.AllReadOnly<Article>()
                .Select(a => new ArticleListViewModel()
                {
                    Id = a.Id,
                    Type = a.ArticleType.ToString(),
                    ImgUrl = a.ImageURL,
                    Title = a.Title,
                    Author = a.Author.User.Nickname,
                    PublishDate = a.ReleaseDate.ToShortDateString()
                })
                .ToListAsync();
        }

        public async Task<ArticleEditViewModel?> GetArticleEditModelByIdAsync(int articleId)
        {
               return await repository.AllReadOnly<Article>()
               .Where(a => a.Id == articleId)
               .Select(a => new ArticleEditViewModel()
               {
                   Id = a.Id,
                   Title = a.Title,
                   Content = a.Content,
                   ArticleType = a.ArticleType,
                   ImageURL = a.ImageURL
               })
               .FirstOrDefaultAsync();
        }

        public async Task<ArticleReadViewModel?> GetArticleReadModelAsync(int articleId)
		{
			   return await repository.AllReadOnly<Article>()
			  .Where(a => a.Id == articleId)
			  .Select(a => new ArticleReadViewModel()
			  {
				  Id = a.Id,
				  Title = a.Title,
				  Content = a.Content,
				  ImgUrl = a.ImageURL,
				  PublishDate = a.ReleaseDate.ToString("d"),
				  Author = a.Author.User.Nickname
			  })
			  .FirstOrDefaultAsync();
		}
	}
}
