using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.WebApi.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceApiController : Controller
    {
        private readonly IServiceService serviceService;
        public ServiceApiController(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }

        [HttpGet("getDeactivated")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDeactivated()
        {
            try
            {
                var services = await serviceService.AllServiceListViewModelFilteredAsync(s => s.IsActive == false);

                return Ok(services);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("getActive")]
        [Produces("application/json")]
        public async Task<IActionResult> GetActive()
        {
            try
            {
                var services = await serviceService.AllServiceListViewModelFilteredAsync(s => s.IsActive == true);

                return Ok(services);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
