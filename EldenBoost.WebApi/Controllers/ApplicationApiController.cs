using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Core.Models.Order;
using EldenBoost.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationApiController : Controller
    {
        private readonly IApplicationService applicationService;

        public ApplicationApiController(IApplicationService _applicationService)
        {
            applicationService = _applicationService;
        }

        [HttpGet("get-approved")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApproved()
        {
            try
            {
                var applications = await applicationService.AllAsync(a => a.IsApproved && a.ApplicationType == ApplicationType.Booster);
                return Ok(applications);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-pending")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPending()
        {
            try
            {
                var applications = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Booster);
                return Ok(applications);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-approved-author")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApprovedAuthor()
        {
            try
            {
                var applications = await applicationService.AllAsync(a => a.IsApproved && a.ApplicationType == ApplicationType.Author);

                return Ok(applications);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("get-pending-author")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPendingAuthor()
        {
            try
            {
                var applications = await applicationService.AllAsync(a => !a.IsApproved && !a.IsRejected && a.ApplicationType == ApplicationType.Author);

                return Ok(applications);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

		[HttpGet("get-data")]
		[Produces("application/json")]
		public async Task<IActionResult> GetApplicationsData()
		{
			try
			{
				var data = await applicationService.GetApplicationCaountDataAsync();
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
