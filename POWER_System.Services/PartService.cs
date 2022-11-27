using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Services;

public class PartService : IPartService
{
    private readonly IApplicationDbRepository repo;
    private readonly IEnclosureService enclosureService;

    public PartService(IApplicationDbRepository _repo,
        IEnclosureService _enclosureService)
    {
        repo = _repo;
        enclosureService = _enclosureService;
    }

    //public async Task<IEnumerable<PartServiceModel>> GetAllPartsForEnclosuresAsync(Guid enclosureId)
    //{
    //    var enclosure = await repo.All<Enclosure>().FirstOrDefaultAsync(e => e.Id == enclosureId);

    //    List<PartServiceModel> parts = new List<PartServiceModel>();

    //    foreach (var enclosurePart in enclosure.Parts)
    //    {
    //        var part = new PartServiceModel()
    //        {
    //            Manufacturer = enclosurePart.Manufacturer,
    //            OrderNumber = enclosurePart.OrderNumber,
    //            Description = enclosurePart.Description,
    //            Delivery = enclosurePart.Delivery
    //        };

    //        parts.Add(part);
    //    }

    //    return parts;
    //}

    public async Task<IEnumerable<PartServiceModel>> GetAllPartsForEnclosuresAsync(Guid enclosureId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PartServiceModel>> AddParts(IEnumerable<PartServiceModel> model)
    {
        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var serviceModel in model)
        {
            var part = new PartServiceModel()
            {
                DeviceTag = serviceModel.DeviceTag,
                Manufacturer = serviceModel.Manufacturer,
                OrderNumber = serviceModel.OrderNumber,
                Description = serviceModel.Description,
                Delivery = serviceModel.Delivery,
                Comment = serviceModel.Comment
            };

            parts.Add(part);
        }

        return parts;
    }
}