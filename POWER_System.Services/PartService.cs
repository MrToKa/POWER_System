using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Enum;
using POWER_System.Services.Contracts;
using POWER_System.Services.DTO.Parts;
using POWER_System.Services.Models;
using System.Xml.Serialization;

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
            .ThenInclude(p => p.Part)
            .Include(x => x.Parts)
            .ThenInclude(q => q.PartsQuantity)
            .FirstOrDefaultAsync(e => e.Id == enclosureId);

        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var enclosurePart in enclosure.Parts)
        {
            var part = new PartServiceModel()
            {
                Manufacturer = enclosurePart.Part.Manufacturer,
                OrderNumber = enclosurePart.Part.OrderNumber,
                Description = enclosurePart.Part.Description,
                Delivery = enclosurePart.Part.Delivery,
                //TagsQuantity = 
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

        List<EnclosurePart> assignedParts = new List<EnclosurePart>();

        foreach (var part in model)
        {
            var databasePart = await repo.All<Part>().AsNoTracking()
                .Include(p => p.Parts)
                .FirstOrDefaultAsync(n => n.OrderNumber == part.OrderNumber);

            var partON = databasePart.OrderNumber;
            string partTag = part.DeviceTag;
            double quantity = part.Quantity;

            var currentPart = await repo.All<EnclosurePart>()
                .Include(p => p.Part)
                .Where(e => e.EnclosureId == enclosureId)
                .FirstOrDefaultAsync(n => n.Part.OrderNumber == partON);

            if (currentPart == null)
            {
                var createdPart = await AssignPartToEnclosure(partON, enclosureId, partTag, quantity);

                assignedParts.Add(createdPart);
            }
            else
            {
                currentPart.PartsQuantity.Add(new PartTagQuantity(){Tag = partTag, Quantity = quantity});
            }
        }

        enclosure.Parts = assignedParts;
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
                string deviceTag = line.Label.Property[0].PropertyValue;
                string manufacturer = line.Label.Property[1].PropertyValue;
                string description = line.Label.Property[2].PropertyValue;
                string orderNumber = line.Label.Property[3].PropertyValue;
                double quantity = double.Parse(line.Label.Property[4].PropertyValue);

                var part = new PartServiceModel()
                {
                    DeviceTag = deviceTag,
                    Manufacturer = manufacturer,
                    Description = description,
                    OrderNumber = orderNumber,
                    Quantity = quantity
                };

                parts.Add(part);

                var databasePart = await repo.All<Part>().AsNoTracking()
                    .FirstOrDefaultAsync(n => n.OrderNumber == part.OrderNumber);

                if (databasePart == null)
                {
                    await AddPartToDatabase(part);
                }

            }
        }

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
        };

        await repo.AddAsync(dataPart);
        await repo.SaveChangesAsync();
    }

    private async Task<EnclosurePart> AssignPartToEnclosure(string partON, Guid enclosureId, string devTag, double qty)
    {
        var part = await repo.All<Part>()
            .Include(p => p.Parts)
            .FirstOrDefaultAsync(n => n.OrderNumber == partON);

        var assignedPart = new EnclosurePart()
        {
            Part = part,
            PartId = part.Id,
            EnclosureId = enclosureId,
            PartsQuantity = new List<PartTagQuantity>()
            {
                new PartTagQuantity()
                    {Tag = devTag, Quantity = qty}
            }
        };

        await repo.AddAsync(assignedPart);
        await repo.SaveChangesAsync();
        return assignedPart;
    }
}