using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EldenBoost.Common.Constants.NotificationConstants;

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

		[HttpGet]
		[AllowAnonymous]
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
		public async Task<IActionResult> Create()
		{
			if (await authorService.ExistsByUserIdAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an author mate mate");
			}

			var model = new ArticleFormModel();

			return View(model);
		}

		[HttpPost]

		public async Task<IActionResult> Create(ArticleFormModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData[ErrorMessage] = "Model is invalid";
				return View(model);
			}

			if (await authorService.ExistsByUserIdAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an author mate mate");
			}

			await articleService.CreateAsync(model, User.Id());

			return RedirectToAction("MyProfile", "Author");
		}
	}
}
