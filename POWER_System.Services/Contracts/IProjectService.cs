using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IProjectService
{
    Task<ProjectServiceModel> GetProjectAsync(string projectName);

    Task<IEnumerable<ProjectServiceModel>> GetAllProjectsAsync();

    Task AddProjectAsync(ProjectServiceModel model);
}