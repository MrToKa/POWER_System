using Microsoft.AspNetCore.Mvc;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Controllers
{
    public class EnclosureController : Controller
    {
        private readonly IEnclosureService enclosureService;

        public EnclosureController(IEnclosureService _enclosureService)
        {
            this.enclosureService = _enclosureService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EnclosureServiceModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EnclosureServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await enclosureService.AddProjectEnclosureAsync(model);

                return RedirectToAction("Details", "Project", new { id = model.ProjectId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }
    }
}
