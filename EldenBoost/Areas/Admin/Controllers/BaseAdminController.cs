using static EldenBoost.Common.Constants.GeneralApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldenBoost.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdminRole)]
    public class BaseAdminController : Controller
    {

    }
}
