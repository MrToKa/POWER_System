using System.Runtime.InteropServices;
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

    public async Task<IEnumerable<PartServiceModel>> GetSummarizedPartsForEnclosuresAsync(Guid enclosureId)
    {
        var enclosure = await repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .ThenInclude(p => p.Parts)
            .Where(e => e.EnclosureId == enclosureId & e.Quantity > 0).ToListAsync();

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
                Delivery = enclosurePart.Part.Delivery,
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

        return parts.Where(q => q.Quantity > 0);
    }

    public async Task<IEnumerable<PartServiceModel>> GetDetailedPartsForEnclosuresAsync(Guid enclosureId)
    {
        var enclosure = await repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .ThenInclude(p => p.Parts)
            .Where(e => e.EnclosureId == enclosureId).ToListAsync();

        List<PartServiceModel> parts = new List<PartServiceModel>();

        foreach (var enclosurePart in enclosure)
        {
            string orderNumber = enclosurePart.Part.OrderNumber;
            string tag = enclosurePart.Tag;
            double quantity = enclosurePart.Quantity;

            var part = new PartServiceModel()
            {
                Manufacturer = enclosurePart.Part.Manufacturer,
                OrderNumber = orderNumber,
                Description = enclosurePart.Part.Description,
                Delivery = enclosurePart.Part.Delivery,
                Quantity = enclosurePart.Quantity,
                DeviceTag = enclosurePart.Tag,
            };

            if (parts.Any(t => t.DeviceTag == tag && t.OrderNumber == orderNumber))
            {
                parts.First(o => o.OrderNumber == orderNumber).Quantity += quantity;
            }
            else
            {
                parts.Add(part);
            }
        }

        return parts.Where(q => q.Quantity > 0);
    }

    public async Task<List<EnclosurePart>> AssignPartsToEnclosure(IEnumerable<PartServiceModel> model, Guid enclosureId)
    {
        List<EnclosurePart> assignedParts = new List<EnclosurePart>();

        await ResetCounter(enclosureId);

        foreach (var part in model)
        {

            var partON = part.OrderNumber;
            string partTag = part.DeviceTag;
            double quantity = part.Quantity;

            if (assignedParts.Any(t => t.Tag == partTag && t.Part.OrderNumber == partON && t.EnclosureId == enclosureId))
            {
                assignedParts.First(t => t.Tag == partTag).Quantity += quantity;
            }
            else if (await repo.All<EnclosurePart>()
                .AnyAsync(t => t.Tag == partTag && t.Part.OrderNumber == partON && t.EnclosureId == enclosureId))
            {
                var zeroPart = await repo.All<EnclosurePart>()
                    .FirstOrDefaultAsync((t => t.Part.OrderNumber == partON && t.Tag == partTag && t.EnclosureId == enclosureId));

                zeroPart.Quantity += quantity;
                assignedParts.Add(zeroPart);
            }
            else
            {
                var createdPart = await AssignPartToEnclosure(partON, enclosureId, partTag, quantity);
                assignedParts.Add(createdPart);
            }

        }

        await repo.SaveChangesAsync();

        return assignedParts;
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
            var labeling = (EplanLabelling)serializer.Deserialize(reader);

            foreach (var line in labeling.Document.Page.Line)
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
                    Quantity = quantity,
                };

                var databasePart = repo.All<Part>()
                    .Any(n => n.OrderNumber == part.OrderNumber);

                if (databasePart == false)
                {
                    await AddPartToDatabase(part);
                }

                if (parts.Any(t => t.DeviceTag == deviceTag && t.OrderNumber == orderNumber))
                {
                    parts.First(o => o.OrderNumber == orderNumber).Quantity += quantity;
                }
                else
                {
                    parts.Add(part);
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

    private async Task<EnclosurePart> AssignPartToEnclosure(string dBpartON, Guid enclosureId, string devTag, double qty)
    {
        var databasePart = await repo.All<Part>()
            .FirstOrDefaultAsync(p => p.OrderNumber == dBpartON);

        var assignedPart = new EnclosurePart()
        {
            PartId = databasePart.Id,
            Part = databasePart,
            EnclosureId = enclosureId,
            Tag = devTag,
            Quantity = qty
        };

        await repo.AddAsync(assignedPart);
        await repo.SaveChangesAsync();
        return assignedPart;
    }

    private async Task ResetCounter(Guid enclosureId)
    {
        var currentPart = repo.All<EnclosurePart>()
            .Include(p => p.Part)
            .Where(e => e.EnclosureId == enclosureId);

        foreach (var part in currentPart)
        {
            part.Quantity = 0;
        }

        await repo.SaveChangesAsync();
    }
}