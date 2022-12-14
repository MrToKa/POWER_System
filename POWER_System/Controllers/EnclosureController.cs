using Microsoft.AspNetCore.Mvc;
using POWER_System.Models;
using POWER_System.Services;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Controllers
{
    public class EnclosureController : Controller
    {
        private readonly IEnclosureService enclosureService;
        private readonly IPartService partService;

        public EnclosureController(IEnclosureService _enclosureService,
            IPartService _partService)
        {
            this.enclosureService = _enclosureService;
            this.partService = _partService;
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
            var model = await enclosureService.EnclosureSummarizedDetails(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EnclosureFullDetails(Guid id)
        {
            var model = await enclosureService.EnclosureFullDetails(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddParts(Guid id)
        {
            var model = await enclosureService.EnclosureFullDetails(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParts(Guid id, IFormFile file)
        {
            if (!ModelState.IsValid || file == null || !file.FileName.EndsWith("xml"))
            {
                ModelState.AddModelError("", "Please select correct file to upload");

                return RedirectToAction("AddParts", id);
            }

            try
            {
                var model = await enclosureService.AddPartsToEnclosure(id, file);
                await partService.AssignPartsToEnclosure(model.Parts, id);

                return RedirectToAction("EnclosureFullDetails", new { id = id });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Check data in the upload file");
            }

            return RedirectToAction("AddParts", id);
        }

    }
}
