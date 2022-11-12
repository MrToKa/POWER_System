using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts
{
    public interface IEnclosureService
    {
        Task AddProjectEnclosureAsync(EnclosureServiceModel model);

        Task<IEnumerable<EnclosureServiceModel>> GetAllEnclosuresForProjectAsync(Guid id);
    }
}
