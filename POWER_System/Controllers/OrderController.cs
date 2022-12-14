using Microsoft.AspNetCore.Mvc;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IPartService partService;

        public OrderController(IOrderService _orderService,
            IPartService _partService)
        {
            orderService = _orderService;
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, string orderId)
        {
            await orderService.DeletePartsOrderAsync(orderId);

            return RedirectToAction("All", "Order", new { id = id});
        }
    }
}
