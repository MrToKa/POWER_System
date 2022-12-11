using Microsoft.AspNetCore.Mvc;
using POWER_System.Models;
using POWER_System.Services;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IEnclosureService enclosureService;
        private readonly IOrderService orderService;
        private readonly IPartService partService;

        public OrderController(IProjectService _projectService,
            IOrderService _orderService,
            IEnclosureService _enclosureService,
            IPartService _partService)
        {
            projectService = _projectService;
            orderService = _orderService;
            enclosureService = _enclosureService;
            partService = _partService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await orderService.GetAllOrders();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllForProject(Guid projectId)
        {
            var model = await orderService.GetAllOrdersForProjectsAsync(projectId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(EnclosureServiceModel modelId)
        {
            var model = await partService.GetSummarizedPartsForEnclosuresAsync(modelId.Id);

            TempData["EnclosureId"] = modelId.Id;
            TempData.Keep();

            return View(model.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(List<PartServiceModel> model)
        {
            //TempData.Keep();
            Guid enclosureId = (Guid)TempData.Peek("EnclosureId");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {                
                await orderService.AddOrderAsync(enclosureId);
                await orderService.CreatePartsOrder(model, enclosureId);

                return RedirectToAction("Details", "Enclosure", new { id = enclosureId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, string orderId)
        {
            var model = await orderService.GetOrderAsync(id, orderId);

            return View(model.ToList());
        }
    }
}
