using Microsoft.AspNetCore.Mvc;
using POWER_System.Services;
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await enclosureService.EnclosureDetails(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddParts(Guid id)
        {
            var model = await enclosureService.EnclosureDetails(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParts(Guid id, IFormFile file)
        {
            var model = await enclosureService.AddPartsToEnclosure(id, file);

            return View(model);
        }
    }
}
