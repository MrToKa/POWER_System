namespace POWER_System.Services.Models;

public class ProjectServiceModel
{
    public ProjectServiceModel()
    {
        Enclosures = new List<EnclosureServiceModel>();
    }

    public Guid Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Contractor { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual List<EnclosureServiceModel>? Enclosures { get; set; }

    public virtual IEnumerable<PartOrderServiceModel>? PartsOrders { get; set; }

    //public virtual IEnumerable<StorageServiceModel> Storages { get; set; }

    //public virtual IEnumerable<SiteServiceServiceModel> SiteServices { get; set; }
}