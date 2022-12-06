using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

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
    public async Task AddOrderAsync(PartOrderServiceModel model)
    {
        var order = new PartOrder()
        {
            DateCreated = DateTime.Now,
            EnclosureId = model.EnclosureId,
            Comment = model.Comment,
        };

        await repo.AddAsync(order);
        await repo.SaveChangesAsync();
    }

    public async Task<List<EnclosurePart>> CreatePartsOrder(Guid enclosureId)
    {
        var currentParts = repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .Where(e => e.EnclosureId == enclosureId && e.Quantity > 0);

        var orderId = repo.All<PartOrder>()
            .FirstOrDefaultAsync(x => x.EnclosureId == enclosureId).Result.Id;

        List<EnclosurePart> modifiedParts = new List<EnclosurePart>();

        foreach (var part in currentParts)
        {
            part.PartOrderId = orderId;
            modifiedParts.Add(part);
        }

        await repo.SaveChangesAsync();

        return modifiedParts;
    }


    //public async Task CreatePartsOrder(IEnumerable<PartServiceModel> model, Guid enclosureId)
    //{
    //    var currentParts = repo.All<EnclosurePart>()
    //        .Include(p => p.Part)
    //        .Where(e => e.EnclosureId == enclosureId);

    //    foreach (var part in model)
    //    {
    //        if (currentParts.Any(x => x.Part.OrderNumber == part.OrderNumber))
    //        {
    //            currentParts.First(x => x.Part.OrderNumber == part.OrderNumber).Delivery = part.Delivery;
    //        }
    //    }

    //    await repo.SaveChangesAsync();
    //}

}