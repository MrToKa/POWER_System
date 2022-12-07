using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;
using static POWER_System.Models.Enum.OrderStatus;

namespace POWER_System.Services;

public class OrderService : IOrderService
{
    private readonly IApplicationDbRepository repo;
    private readonly IPartService partService;

    public OrderService(IApplicationDbRepository _repo,
        IPartService _partService)
    {
        repo = _repo;
        partService = _partService;
    }
    public async Task AddOrderAsync(Guid enclosureId)
    {
        var order = new PartOrder()
        {
            DateCreated = DateTime.Now,
            EnclosureId = enclosureId,
            Status = InReview,
        };

        await repo.AddAsync(order);
        await repo.SaveChangesAsync();
    }

    public async Task CreatePartsOrder(List<PartServiceModel> model, Guid enclosureId)
    {
        var currentParts = repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .Where(e => e.EnclosureId == enclosureId && e.Quantity > 0);

        Guid orderId = (await repo.All<PartOrder>()
            .OrderByDescending(d => d.DateCreated)
            .FirstOrDefaultAsync(x => x.EnclosureId == enclosureId)).Id;

        foreach (var modelPart in model)
        {
            foreach (var enclosurePart in currentParts.Where(x => x.Part.OrderNumber == modelPart.OrderNumber))
            {
                enclosurePart.Delivery = modelPart.Delivery;
                enclosurePart.PartOrderId = orderId;
            }

        }
        await repo.SaveChangesAsync();       
    }

    public async Task<List<PartServiceModel>> GetOrderAsync(Guid orderId)
    {
        var enclosure = await repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .ThenInclude(p => p.Parts)
            .Where(e => e.PartOrderId == orderId && e.Quantity > 0).ToListAsync();

        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var enclosurePart in enclosure)
        {
            string OrderNumber = enclosurePart.Part.OrderNumber;
            double Quantity = enclosurePart.Quantity;

            var part = new PartServiceModel()
            {
                Manufacturer = enclosurePart.Part.Manufacturer,
                OrderNumber = OrderNumber,
                Description = enclosurePart.Part.Description,
                Delivery = enclosurePart.Delivery,
                Quantity = enclosurePart.Quantity,
            };


            if (parts.Any(o => o.OrderNumber == OrderNumber))
            {
                parts.First(o => o.OrderNumber == OrderNumber).Quantity += Quantity;
            }
            else
            {
                parts.Add(part);
            }
        }

        return parts.Where(q => q.Quantity > 0).ToList();
    }

}