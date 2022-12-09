using POWER_System.Models;

namespace POWER_System.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? OfficeLocation { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public List<UserProject> AssignedProjects { get; set; } = new List<UserProject>();
    }
}
