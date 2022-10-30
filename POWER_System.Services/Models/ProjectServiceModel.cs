using POWER_System.Models.Enum;
using POWER_System.Models;

namespace POWER_System.Services.Models;

public class ProjectServiceModel
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Contractor { get; set; } = null!;

    public string Status { get; set; } = null!;

    //public virtual IEnumerable<EnclosureServiceModel> Enclosures { get; set; }

    //public virtual IEnumerable<StorageServiceModel> Storages { get; set; }

    //public virtual IEnumerable<SiteServiceServiceModel> SiteServices { get; set; }
}