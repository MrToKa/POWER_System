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

    public OrderService(IApplicationDbRepository _repo)
    {
        repo = _repo;
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

    public async Task<IEnumerable<PartOrderServiceModel>> GetAllOrders()
    {
        return await repo.All<PartOrder>()
            .Where(o => o.IsDeleted == false)
            .Select(p => new PartOrderServiceModel
            {
                Id = p.Id,
                DateCreated = p.DateCreated,
                Enclosure = p.Enclosure,
                Comment = p.Comment,
                EnclosureId = p.EnclosureId,
                OrderDate = p.OrderDate,
                Status = p.Status,
                ProjectId = p.Enclosure.ProjectId,
                Project = p.Enclosure.Project
            })
            .OrderByDescending(d => d.DateCreated)
            .ToListAsync();
    }

    public async Task<IEnumerable<PartOrderServiceModel>> GetAllOrdersForProjectsAsync(Guid id)
    {
        return await repo.All<PartOrder>()
            .Where(x => x.Enclosure.ProjectId == id && x.IsDeleted == false)
            .Select(p => new PartOrderServiceModel
            {
                Id = p.Id,
                DateCreated = p.DateCreated,
                Enclosure = p.Enclosure,
                Comment = p.Comment,
                EnclosureId = p.EnclosureId,
                OrderDate = p.OrderDate,
                Status = p.Status,
                ProjectId = p.Enclosure.ProjectId
            })
            .OrderByDescending(d => d.DateCreated)
            .ToListAsync();
    }

    public async Task CreatePartsOrder(List<PartServiceModel> model, Guid enclosureId)
    {
        var currentParts = repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .Include(p => p.EnclosureParts)
            .Where(e => e.EnclosureId == enclosureId && e.Quantity > 0);

        Guid orderId = (await repo.All<PartOrder>()
            .OrderByDescending(d => d.DateCreated)
            .FirstOrDefaultAsync(x => x.EnclosureId == enclosureId)).Id;

        foreach (var modelPart in model)
        {
            foreach (var enclosurePart in currentParts.Where(x => x.Part.OrderNumber == modelPart.OrderNumber))
            {
                enclosurePart.Delivery = modelPart.Delivery;

                enclosurePart.EnclosureParts.Add(new EnclosurePartOrder()
                {
                    EnclosurePartId = enclosurePart.Id,
                    PartOrderId = orderId,
                    Delivery = modelPart.Delivery,
                    Quantity = enclosurePart.Quantity,
                });
            }
        }

        await repo.SaveChangesAsync();
    }

    public async Task<List<PartServiceModel>> GetOrderAsync(Guid enclosureId, string orderId)
    {
        var enclosureParts = await repo.All<EnclosurePart>()
            .Include(p => p.EnclosureParts)
            .Include(p => p.Part)
            .ThenInclude(p => p.Parts)
            .Where(e => e.EnclosureId == enclosureId)
            .ToListAsync();

        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var enclosurePart in enclosureParts)
        {
            if (enclosurePart.EnclosureParts.Any(x => x.PartOrderId.ToString() == orderId))
            {
                var delivery = enclosurePart.EnclosureParts.First(x => x.PartOrderId.ToString() == orderId).Delivery;
                double quantity = enclosurePart.EnclosureParts.First(x => x.PartOrderId.ToString() == orderId).Quantity;

                var part = new PartServiceModel()
                {
                    Manufacturer = enclosurePart.Part.Manufacturer,
                    OrderNumber = enclosurePart.Part.OrderNumber,
                    Description = enclosurePart.Part.Description,
                    Delivery = delivery,
                    Quantity = quantity
                };

                if (parts.Any(t => t.OrderNumber == enclosurePart.Part.OrderNumber))
                {
                    parts.First(o => o.OrderNumber == enclosurePart.Part.OrderNumber).Quantity += quantity;
                }
                else
                {
                    parts.Add(part);
                }
            }
        }

        return parts.Where(q => q.Quantity > 0).ToList();
    }

    public async Task DeletePartsOrderAsync(string orderId)
    {
        var order = await repo.All<PartOrder>()
            .FirstOrDefaultAsync(x => x.Id.ToString() == orderId);

        if (order != null)
        {
            order.DeletedOn = DateTime.Now;
            order.IsDeleted = true;

            await repo.SaveChangesAsync();
        }
    }
}