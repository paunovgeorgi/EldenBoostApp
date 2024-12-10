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
        private readonly ILogger<AuthorController> logger;

        public AuthorController(IAuthorService _authorService, IArticleService _articleService, ILogger<AuthorController> _logger)
        {
            authorService = _authorService;
            articleService = _articleService;
            logger = _logger;
        }
        [HttpGet]
        // Fetches the author's profile and their articles
        public async Task<IActionResult> MyProfile()
        {
            try
            {
                // Get the author details based on the logged-in user's ID
                Author? author = await authorService.GetAuthorByUserIdAsync(User.Id());
                if (author == null)
                {
                    logger.LogWarning($"User with ID {User.Id()} tried to access the profile but is not an author.");
                    return Unauthorized("You don't have access to this page!");
                }

                // Fetch articles written by the author
                var articles = await articleService.AllByAuthorIdAsync(author.Id);
                return View(articles);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching author profile and articles.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
