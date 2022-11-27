using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POWER_System.Services.Contracts;
using POWER_System.Services.Models;

namespace POWER_System.Controllers;

[Authorize]
public class ProjectController : BaseController
{
    private readonly IProjectService projectService;

    public ProjectController(IProjectService _projectService)
    {
        this.projectService = _projectService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await projectService.GetAllProjectsAsync();
        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var model = new ProjectServiceModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProjectServiceModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            await projectService.AddProjectAsync(model);

            return RedirectToAction(nameof(All));
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Something went wrong");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var model = await projectService.GetProjectAsync(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> SearchResult(string keyword)
    {
        var model = await projectService.SearchProjectAsync(keyword);

        if (model.Any())
        {
            return View("All", model);
        }

        return RedirectToAction(nameof(All));
    }
}
