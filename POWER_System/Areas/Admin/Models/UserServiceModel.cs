using System.ComponentModel.DataAnnotations;

namespace POWER_System.Areas.Admin.Models
{
    public class UserServiceModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Office location")]
        public string OfficeLocation { get; set; } = null!;

        [Display(Name = "Department")] 
        public string Department { get; set; } = null!;

        [Display(Name = "Position")] 
        public string Position { get; set; } = null!;

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "User deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }
    }
}
