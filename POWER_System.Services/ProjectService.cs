using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Enum;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Services;

public class ProjectService : IProjectService
{
    private readonly IApplicationDbRepository repo;
    private readonly IEnclosureService enclosureService;

    public ProjectService(IApplicationDbRepository _repo,
        IEnclosureService _enclosureService
        )
    {
        repo = _repo;
        enclosureService = _enclosureService;
    }

    public async Task AddProjectAsync(ProjectServiceModel model)
    {
        ProjectStatus currentStatus;
        Enum.TryParse(model.Status, out currentStatus);

        var project = new Project()
        {
            Number = model.Number,
            Name = model.Name,
            Description = model.Description,
            Contractor = model.Contractor,
            Status = currentStatus,
        };

        await repo.AddAsync(project);
        await repo.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProjectServiceModel>> GetAllProjectsAsync()
    {
        var projects = await repo.All<Project>().ToListAsync();

        return projects.Select(p => new ProjectServiceModel
        {
            Contractor = p.Contractor,
            Status = p.Status.ToString(),
            Description = p.Description,
            Id = p.Id,
            Number = p.Number,
            Name = p.Name,
        });
    }

    public async Task<ProjectServiceModel> GetProjectAsync(Guid id)
    {
        var project = await repo.All<Project>()
            .Include(e => e.Enclosures)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        var enclosures = await enclosureService.GetAllEnclosuresForProjectAsync(id);
        var orders = await GetAllOrdersForProjectsAsync(id);

        return new ProjectServiceModel()
        {
            Id = project.Id,
            Number = project.Number,
            Contractor = project.Contractor,
            Description = project.Description,
            Name = project.Name,
            Status = project.Status.ToString(),
            Enclosures = (List<EnclosureServiceModel>)enclosures,
            PartsOrders = orders
        };
    }

    public async Task<IEnumerable<ProjectServiceModel>> SearchProjectAsync(string keyword)
    {
        var projects = await repo.All<Project>()
            .ToListAsync();

        if (!String.IsNullOrEmpty(keyword))
        {
            projects = projects.Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }

        var projectCollection = new List<ProjectServiceModel>();

        foreach (var project in projects)
        {
            var filteredProject = new ProjectServiceModel
            {
                Contractor = project.Contractor,
                Status = project.Status.ToString(),
                Description = project.Description,
                Id = project.Id,
                Number = project.Number,
                Name = project.Name,
            };

            projectCollection.Add(filteredProject);
        }

        return projectCollection;
    }

    public async Task<IEnumerable<PartOrderServiceModel>> GetAllOrdersForProjectsAsync(Guid id)
    {
        return await repo.All<PartOrder>()
            .Where(x => x.Enclosure.ProjectId == id && x.IsDeleted == false)
            .Select(p => new PartOrderServiceModel
            {
                DateCreated = p.DateCreated,
                Enclosure = p.Enclosure,
                Comment = p.Comment,
                EnclosureId = p.EnclosureId,
                Id = id,
                OrderDate = p.OrderDate,
                Status = p.Status,
            })
            .OrderByDescending(d => d.DateCreated)
            .ToListAsync();
    }
}