using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UserApiController : Controller
	{
		private readonly IUserService userService;
		public UserApiController(IUserService _userService)
		{
			userService = _userService;
		}

		[HttpGet("get-data")]
		[Produces("application/json")]
		public async Task<IActionResult> GetUserData()
		{
			try
			{
				var data = await userService.GetUserCountDataAsync();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Message = ex,
				});
			}
		}
	}
}
