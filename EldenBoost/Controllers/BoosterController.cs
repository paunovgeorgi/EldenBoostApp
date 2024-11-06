using EldenBoost.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Controllers
{
    public class BoosterController : BaseController
    {
        private readonly IBoosterService boosterService;
        public BoosterController(IBoosterService _boosterService)
        {
            boosterService = _boosterService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await boosterService.AllBoostersToCardModelAsync();

            return View(model);
        }
    }
}
