using EldenBoost.Core.Contracts;
using EldenBoost.Extensions;
using EldenBoost.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService authorService;
        private readonly IArticleService articleService;

        public AuthorController(IAuthorService _authorService, IArticleService _articleService)
        {
            authorService = _authorService;
            articleService = _articleService;
        }
        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            Author? author = await authorService.GetAuthorByUserIdAsync(User.Id());
            if (author == null)
            {
                return Unauthorized("You don't have access to this page!");
            }

            var articles = await articleService.AllByAuthorIdAsync(author.Id);

            return View(articles);
        }
    }
}
