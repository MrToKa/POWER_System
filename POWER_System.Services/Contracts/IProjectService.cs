using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IProjectService
{
    Task<ProjectServiceModel> GetProjectAsync(Guid id);

    Task<IEnumerable<ProjectServiceModel>> GetAllProjectsAsync();

    Task AddProjectAsync(ProjectServiceModel model);
}