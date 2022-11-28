using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Services.Contracts;
using POWER_System.Services.DTO.Parts;
using POWER_System.Services.Models;
using System.Xml.Serialization;
using POWER_System.Models.Enum;

namespace POWER_System.Services;

public class PartService : IPartService
{
    private readonly IApplicationDbRepository repo;


    public PartService(IApplicationDbRepository _repo)
    {
        repo = _repo;
    }

    public async Task<IEnumerable<PartServiceModel>> GetAllPartsForEnclosuresAsync(Guid enclosureId)
    {
        var enclosure = await repo.All<Enclosure>()
            .Include(p => p.Parts)
            .FirstOrDefaultAsync(e => e.Id == enclosureId);

        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var enclosurePart in enclosure.Parts)
        {
            var part = new PartServiceModel()
            {
                Manufacturer = enclosurePart.Manufacturer,
                OrderNumber = enclosurePart.OrderNumber,
                Description = enclosurePart.Description,
                Delivery = enclosurePart.Delivery
            };

            parts.Add(part);
        }

        return parts;
    }

    public async Task AssignPartsToEnclosure(IEnumerable<PartServiceModel> model, Guid enclosureId)
    {
        var enclosure = await repo.All<Enclosure>()
            .Include(p => p.Parts)
            .FirstOrDefaultAsync(e => e.Id == enclosureId);

        List<Part> assignedParts = new List<Part>();

        foreach (var part in model)
        {
            var databasePart = await repo.All<Part>()
                .FirstOrDefaultAsync(n => n.OrderNumber == part.OrderNumber);

            if (assignedParts.Contains(databasePart))
            {
                databasePart.Quantity += 1;
            }

            assignedParts.Add(databasePart);
        }

        enclosure.Parts = assignedParts;

        await repo.SaveChangesAsync();

    }

    public async Task<List<PartServiceModel>> AddPartsFromFile(IFormFile file, Guid enclosureId)
    {
        var parts = new List<PartServiceModel>();

        var filePath = Path.GetTempFileName();

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(EplanLabelling));

        FileStream reader = new FileStream(filePath, FileMode.OpenOrCreate);
        {
            var labelling = (EplanLabelling)serializer.Deserialize(reader);

            foreach (var line in labelling.Document.Page.Line)
            {
                var DeviceTag = line.Label.Property[0].PropertyValue;
                var Manufacturer = line.Label.Property[1].PropertyValue;
                var Description = line.Label.Property[2].PropertyValue;
                var OrderNumber = line.Label.Property[3].PropertyValue;

                var part = new PartServiceModel()
                {
                    DeviceTag = DeviceTag,
                    Manufacturer = Manufacturer,
                    Description = Description,
                    OrderNumber = OrderNumber
                };

                parts.Add(part);

                var databasePart = await repo.All<Part>()
                    .FirstOrDefaultAsync(n => n.OrderNumber == part.OrderNumber);

                if (databasePart == null)
                {
                    await AddPartToDatabase(part);
                }

            }
        }

        await AssignPartsToEnclosure(parts, enclosureId);

        return parts;
    }

    private async Task AddPartToDatabase(PartServiceModel part)
    {
        var dataPart = new Part()
        {
            Manufacturer = part.Manufacturer,
            OrderNumber = part.OrderNumber,
            Description = part.Description,
            Delivery = OrderDelivery.ACS,
            Measure = string.Empty,
            Price = 0.0m,
            Quantity = 1,
        };

        await repo.AddAsync(dataPart);
        await repo.SaveChangesAsync();
    }
}