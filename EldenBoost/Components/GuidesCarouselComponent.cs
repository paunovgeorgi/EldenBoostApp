using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Components
{
    public class GuidesCarouselComponent : ViewComponent
    {
        private readonly IArticleService articleService;

        public GuidesCarouselComponent(IArticleService _articleService)
        {
            articleService = _articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await articleService.LastThreeArticlesAsync(a => a.ArticleType == Common.Enumerations.ArticleType.Guide);
            return View(model);
        }
    }
}
