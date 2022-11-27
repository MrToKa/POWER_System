using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IPartService
{
    Task<IEnumerable<PartServiceModel>> GetAllPartsForEnclosuresAsync(Guid enclosureId);

    Task<IEnumerable<PartServiceModel>> AddParts(IEnumerable<PartServiceModel> model);
}