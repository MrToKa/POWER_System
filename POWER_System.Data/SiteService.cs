using System.ComponentModel.DataAnnotations;
using POWER_System.Data.Constants;

namespace POWER_System.Data;

public class SiteService
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the required task.
    /// </summary>
    [Required]
    [MaxLength(SiteServiceConstants.ServiceMaxNameLength)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of work that needs to be done, equipment and parts summary.
    /// </summary>
    [Required]
    [MaxLength(SiteServiceConstants.ServiceDescMaxLength)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the person name who requested the job.
    /// </summary>
    [Required]
    [MaxLength(SiteServiceConstants.ServiceRequesterMaxLength)]
    public string RequestedFrom { get; set; }

    /// <summary>
    /// Gets or sets the time needed for job to be performed.
    /// </summary>
    [Required]
    public TimeSpan RequiredTime { get; set; }

    public IEnumerable<Project> Projects { get; set; }

    public IEnumerable<Part> PartsNeeded { get; set; }

    public IEnumerable<Cable> CablesNeeded { get; set; }

    // TODO: Implement workers list who did the job
    // public IEnumerable<User> Workers { get; set; }
}