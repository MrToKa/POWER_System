using Microsoft.AspNetCore.Identity;
using POWER_System.Data.Constants;
using System.ComponentModel.DataAnnotations;
using POWER_System.Data.Enum;

namespace POWER_System.Data;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Roles = new HashSet<IdentityUserRole<string>>();
        this.Claims = new HashSet<IdentityUserClaim<string>>();
        this.Logins = new HashSet<IdentityUserLogin<string>>();
    }

    /// <summary>
    /// Gets or sets the office location of the user. It is a list of current company offices. If a freelancer needs an application account, he is given the role of "Freelancer".
    /// </summary>
    [Required]
    public Offices OfficeLocation { get; set; }

    /// <summary>
    /// Gets or sets the department of the user.
    /// </summary>
    [Required]
    [MaxLength(RegisterUserConstants.DepartmentMaxLength)]
    public string Department { get; set; }

    /// <summary>
    /// Gets or sets the current position of the user.
    /// </summary>
    [Required]
    [MaxLength(RegisterUserConstants.PositionMaxLength)]
    public string Position { get; set; }

    // Audit info
    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    // Deletable entity
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

    public virtual ICollection<Project> PersonalProjects { get; set; }
}