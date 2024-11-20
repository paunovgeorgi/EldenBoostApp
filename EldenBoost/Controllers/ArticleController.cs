using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Extensions;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Text.RegularExpressions;
using static EldenBoost.Common.Constants.NotificationConstants;
using static EldenBoost.Core.Extensions.ModelExtensions;

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
			if (await authorService.IsActiveAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an active author mate");
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

			if (await authorService.IsActiveAsync(User.Id()) == false)
			{
				return Unauthorized("You're not an active author mate mate");
			}

			await articleService.CreateAsync(model, User.Id());

			return RedirectToAction("MyProfile", "Author");
		}

        [HttpGet]
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

            await articleService.EditArticleAsync(model);
            TempData[SuccessMessage] = "Article edited successfully!";

            string information = model.GetArticleInformation();

            return RedirectToAction("Read", "Article", new { area = "", model.Id, information });
        }
    }
}
