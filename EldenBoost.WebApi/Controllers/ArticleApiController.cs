using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
	[Route("api/article")]
	[ApiController]
	public class ArticleApiController : Controller
	{
		private readonly IArticleService articleService;
        public ArticleApiController(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        [HttpGet("get-data")]
		[Produces("application/json")]
		public async Task<IActionResult> GetApplicationsData()
		{
			try
			{
				var data = await articleService.GetArticleCountDataAsync();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Message = ex
				});
			}

		}
	}
}
