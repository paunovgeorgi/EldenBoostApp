﻿using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Payment;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Services;
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
        [ProducesResponseType(typeof(ServiceListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(typeof(ServiceListViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpGet("get-data")]
        [Produces("application/json")]
        public async Task<IActionResult> GetServiceData()
        {
            try
            {
                var data = await serviceService.GetServiceCountDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while processing your request."
                });
            }

        }
    }
}
