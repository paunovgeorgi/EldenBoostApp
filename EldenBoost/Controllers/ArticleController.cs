using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;
using static EldenBoost.Core.Extensions.ModelExtensions;

namespace EldenBoost.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IAuthorService authorService;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(IArticleService _articleService, IAuthorService _authorService, ILogger<ArticleController> _logger)
        {
            articleService = _articleService;
            authorService = _authorService;
            logger = _logger;
        }

        [HttpGet]
        [AllowAnonymous]
        // Retrieves all articles with filtering and pagination based on the query parameters.
        public async Task<IActionResult> All([FromQuery] AllArticlesQueryModel model)
        {
            AllArticlesFilteredAndPagedModel articleModel = await articleService.AllFilteredAndPagedAsync(model);

            model.Articles = articleModel.Articles;
            model.TotalArticles = articleModel.TotalArticleCount;

            return View(model);
        }

        [HttpGet]
		[AllowAnonymous]
        // Fetches the article for reading. Redirects to All if the article doesn't exist.
		public async Task<IActionResult> Read(int id)
		{
			var model = await articleService.GetArticleReadModelAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
		}


		[HttpGet]
        // Checks if the user is an active author before showing the article creation form.
        public async Task<IActionResult> Create()
		{
			if (await authorService.IsActiveAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an active author!");
			}

			var model = new ArticleFormModel();

			return View(model);
		}

		[HttpPost]
        // Validates input and ensures the user is an active author before creating a new article.
        public async Task<IActionResult> Create(ArticleFormModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData[ErrorMessage] = "Model is invalid";
				return View(model);
			}

			if (await authorService.IsActiveAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an active author!");
			}

            try
            {
                await articleService.CreateAsync(model, User.Id());
                TempData[SuccessMessage] = "Article created successfully!";
                return RedirectToAction("MyProfile", "Author");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the article for user {UserId}.", User.Id());
                TempData[ErrorMessage] = "An error occurred while creating the article. Please try again.";
                return View(model);
            }
        }

        [HttpGet]
        // Fetches the article for editing. Ensures the user is its author or an admin.
        public async Task<IActionResult> Edit(int id)
        {
            var model = await articleService.GetArticleEditModelByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction("All", "Article");
            }

            bool hasArticle = await authorService.HasArticleAsync(User.Id(), model.Id);
            bool isActiveAuthor = await authorService.IsActiveAsync(User.Id());
            if ((!hasArticle || !isActiveAuthor) && !User.IsAdmin())
            {
                return RedirectToAction("All", "Article");
            }

            return View(model);
        }

        [HttpPost]
        // Updates the article if the user is its author or an admin, and the input is valid.
        public async Task<IActionResult> Edit(ArticleEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool hasArticle = await authorService.HasArticleAsync(User.Id(), model.Id);
            bool isActiveAuthor = await authorService.IsActiveAsync(User.Id());
            if ((!hasArticle || !isActiveAuthor) && !User.IsAdmin())
            {
                return RedirectToAction("All", "Article");
            }

            try
            {
                await articleService.EditArticleAsync(model);
                TempData[SuccessMessage] = "Article edited successfully!";
                string information = model.GetArticleInformation();
                return RedirectToAction("Read", "Article", new { area = "", model.Id, information });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while editing the article with ID {ArticleId} by user {UserId}.", model.Id, User.Id());
                TempData[ErrorMessage] = "An error occurred while editing the article. Please try again.";
                return View(model);
            }
        }
    }
}
