using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POWER_System.Areas.Admin.Models;
using POWER_System.Areas.Admin.Services.Contracts;
using POWER_System.Models;
using System.Web.Mvc;

namespace POWER_System.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _userService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsers();

            return View(users);
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Roles(string id)
        {
            var user = await userService.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.RoleItems = roleManager.Roles
                            .ToList()
                            .Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                            {
                                Text = r.Name,
                                Value = r.Name,
                                Selected = userManager.IsInRoleAsync(user, r.Name).Result
                            }).ToList();

            return View(model);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await userService.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await userService.GetUserForEdit(id);

            return View(model);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Edit(UserServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await userService.UpdateUser(model);

            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        public async Task<IActionResult> AssignProjets(string id)
        {
            var model = userService.GetUserProjects(id);

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<IActionResult> AssignProjets(string id, List<ProjectViewModel> model)
        {
            await userService.AssignProjectsToUser(id, model);

            return RedirectToAction();
        }
    }
}

