using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class NewsCarouselComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public NewsCarouselComponent(IArticleService _articleService)
        {
            articleService = _articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await articleService.LastThreeArticlesAsync(a => a.ArticleType == Common.Enumerations.ArticleType.News);
            return View(model);
        }
    }
}
