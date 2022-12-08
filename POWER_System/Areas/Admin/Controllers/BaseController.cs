using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static POWER_System.Areas.Admin.AdminConstants;

namespace POWER_System.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRoleName)]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
