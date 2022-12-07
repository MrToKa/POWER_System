using Microsoft.AspNetCore.Http;
using POWER_System.Models;
using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IPartService
{
    Task<IEnumerable<PartServiceModel>> GetSummarizedPartsForEnclosuresAsync(Guid enclosureId);

    Task<IEnumerable<PartServiceModel>> GetDetailedPartsForEnclosuresAsync(Guid enclosureId);

    Task<List<EnclosurePart>> AssignPartsToEnclosure(IEnumerable<PartServiceModel> model, Guid enclosureId);

    Task<List<PartServiceModel>> AddPartsFromFile(IFormFile file, Guid enclosureId);

    //Task EditPartsDeliveryFromOrder(List<PartServiceModel> model);


}