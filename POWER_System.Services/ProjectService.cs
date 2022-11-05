﻿using Microsoft.EntityFrameworkCore;
using POWER_System.Data.Repositories;
using POWER_System.Models;
using POWER_System.Models.Enum;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Services;

public class ProjectService : IProjectService
{
    private readonly IApplicationDbRepository repo;

    public ProjectService(IApplicationDbRepository _repo)
    {
        this.repo = _repo;
    }

    public Task AddFromFileAsync(ProjectServiceModel model, string file)
    {
        throw new NotImplementedException();
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

    public Task<ProjectServiceModel> GetProjectAsync(string projectName)
    {
        throw new NotImplementedException();
    }
}