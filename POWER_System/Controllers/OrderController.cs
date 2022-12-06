using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IProjectService _projectService,
            IOrderService _orderService,
            IEnclosureService _enclosureService)
        {
            projectService = _projectService;
            orderService = _orderService;
            enclosureService = _enclosureService;
        }

        [HttpGet]
        public async Task<IActionResult> All(Guid id)
        {
            var model = await projectService.GetAllOrdersForProjectsAsync(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Add(EnclosureServiceModel modelId)
        {


            var model = new PartOrderServiceModel()
            {
                EnclosureId = modelId.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PartOrderServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                //await orderService.AddOrderAsync(model);
                model.Parts = await orderService.CreatePartsOrder(model.EnclosureId);

                return RedirectToAction("Details", "Enclosure", new {id = model.EnclosureId});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> AddOrder(Guid id)
        //{
        //    var model = await enclosureService.EnclosureSummarizedDetails(id);

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPartsToOrder(Guid id)
        //{
        //    var model = await enclosureService.EnclosureSummarizedDetails(id);
        //    await orderService.CreatePartsOrder(model.Parts, id);
            
        //    return View(model);
        //}
    }
}
