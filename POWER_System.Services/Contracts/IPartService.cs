using Microsoft.AspNetCore.Http;
using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IPartService
{
    Task<IEnumerable<PartServiceModel>> GetAllPartsForEnclosuresAsync(Guid enclosureId);

    Task AssignPartsToEnclosure(IEnumerable<PartServiceModel> model, Guid enclosureId);

    Task<List<PartServiceModel>> AddPartsFromFile(IFormFile file, Guid enclosureId);
}