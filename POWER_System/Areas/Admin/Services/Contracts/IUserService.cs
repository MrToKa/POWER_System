using POWER_System.Areas.Admin.Models;
using POWER_System.Models;

namespace POWER_System.Areas.Admin.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();

        Task<UserServiceModel> GetUserForEdit(string id);

        Task UpdateUser(UserServiceModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task DeleteUser(string id);

        Task AssignProjectsToUser(string id, List<ProjectViewModel> model);

        Task<List<ProjectViewModel>> GetUserProjects(string id);
    }
}
