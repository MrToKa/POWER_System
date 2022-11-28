using Microsoft.AspNetCore.Http;
using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts
{
    public interface IEnclosureService
    {
        Task AddProjectEnclosureAsync(EnclosureServiceModel model);

        Task<IEnumerable<EnclosureServiceModel>> GetAllEnclosuresForProjectAsync(Guid projectId);

        Task<EnclosureServiceModel> EnclosureDetails(Guid enclosureId);

        Task<EnclosureServiceModel> AddPartsToEnclosure(Guid enclosureId, IFormFile file);
    }
}
