using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IAuthorService authorService;

        public ArticleController(IArticleService _articleService, IAuthorService _authorService)
        {
            articleService = _articleService;
            authorService = _authorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllArticlesQueryModel model)
        {
            AllArticlesFilteredAndPagedModel articleModel = await articleService.AllFilteredAndPagedAsync(model);

            model.Articles = articleModel.Articles;
            model.TotalArticles = articleModel.TotalArticleCount;

            return View(model);
        }
    }
}
