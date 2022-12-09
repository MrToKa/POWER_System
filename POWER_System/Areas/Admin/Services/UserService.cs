using Microsoft.EntityFrameworkCore;
using POWER_System.Areas.Admin.Models;
using POWER_System.Areas.Admin.Services.Contracts;
using POWER_System.Data.Repositories;
using POWER_System.Models;

namespace POWER_System.Areas.Admin.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AssignProjectsToUser(string id, List<ProjectViewModel> model)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            var userPersonalProjects = new List<UserProject>();

            foreach (var project in model)
            {
                if (project.Assigned)
                {
                    var userProject = new UserProject()
                    {
                        ProjectId = project.Id,
                        UserId = user.Id,
                    };

                    userPersonalProjects.Add(userProject);
                }
            }

            user.PersonalProjects = userPersonalProjects;

            await repo.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            if (user != null)
            {
                user.UserName = "";
                user.FirstName = "";
                user.LastName = "";
                user.Email = "";
                user.Position = "";
                user.OfficeLocation = "";
                user.Department = "";
                user.PhoneNumber = null;
                user.ModifiedOn = DateTime.Now;
                user.DeletedOn = DateTime.Now;
                user.IsDeleted = true;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<List<ProjectViewModel>> GetUserProjects(string id)
        {
            var projects = await repo.All<Project>()
                .Include(p => p.UserProjects.Where(i => i.UserId == id))
                .ToListAsync();

            var userProjects = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                foreach (var item in project.UserProjects)
                {
                    if (item.UserId == id)
                    {
                        userProjects.Add(new ProjectViewModel()
                        {
                            Id = project.Id,
                            Name = project.Name,
                            Contractor = project.Contractor,
                            Description = project.Description,
                            Number = project.Number,
                            Status = project.Status.ToString()
                        });
                    }
                }
            }

            return userProjects;
        }

        public async Task<UserServiceModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserServiceModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                OfficeLocation = user.OfficeLocation,
                Department = user.Department,
                Position = user.Position,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                DeletedOn = user.DeletedOn,
                IsDeleted = user.IsDeleted,
            };
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email,
                    OfficeLocation = u.OfficeLocation,
                    Department = u.Department,
                    Position = u.Position,
                    AssignedProjects = u.PersonalProjects
                })
                .ToListAsync();
        }

        public async Task UpdateUser(UserServiceModel model)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Position = model.Position;
                user.OfficeLocation = model.OfficeLocation;
                user.Department = model.Department;
                user.ModifiedOn = DateTime.Now;

                await repo.SaveChangesAsync();
            }
        }
    }
}
