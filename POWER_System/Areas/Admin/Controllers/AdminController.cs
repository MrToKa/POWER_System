using Microsoft.AspNetCore.Mvc;

namespace POWER_System.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
