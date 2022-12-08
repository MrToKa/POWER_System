using Microsoft.AspNetCore.Mvc;

namespace POWER_System.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
