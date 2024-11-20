using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ArticleController : BaseAdminController
    {
        private readonly IArticleService articleService;
        public ArticleController(IArticleService _articleService)
        {
            articleService = _articleService;
        }
        public async Task<IActionResult> All()
        {
            var articles = await articleService.GetAllArticlesListViewModelAsync();
            return View(articles);
        }
    }
}
