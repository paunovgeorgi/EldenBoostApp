using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

namespace EldenBoost.Areas.Admin.Controllers
{
    public class ArticleController : BaseAdminController
    {
        private readonly IArticleService articleService;
        private readonly ILogger<ArticleController> logger;
        public ArticleController(IArticleService _articleService, ILogger<ArticleController> _logger)
        {
            articleService = _articleService;
            logger = _logger;
        }
        public async Task<IActionResult> All()
        {
            try
            {
                // Retrieve all articles
                var articles = await articleService.GetAllArticlesListViewModelAsync();
                return View(articles);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving articles");
                TempData[ErrorMessage] = "Failed to load articles";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
