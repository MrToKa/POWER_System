using Microsoft.AspNetCore.Mvc;

namespace POWER_System.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
