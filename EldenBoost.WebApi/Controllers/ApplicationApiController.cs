using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
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
    }
}
