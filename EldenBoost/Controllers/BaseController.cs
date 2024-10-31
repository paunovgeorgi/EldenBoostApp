﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
