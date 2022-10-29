#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POWER_System.Data.Constants;

namespace POWER_System.Data;

public class Storage
{
    public Storage()
    {
        Parts = new HashSet<Part>();
        Cables = new HashSet<Cable>();
        PartOrders = new HashSet<PartOrder>();
        CableOrders = new HashSet<CableOrder>();
        Equipment = new HashSet<Equipment>();
    }

    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the storage.
    /// </summary>
    [Required]
    [MaxLength(StorageConstants.NameMaxLength)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the location of the storage.
    /// </summary>
    [Required]
    [MaxLength(StorageConstants.LocationMaxLength)]
    public string Location { get; set; }

    [ForeignKey(nameof(Project))]
    public Guid? ProjectId { get; set; }

    public Project Project { get; set; }

    public virtual IEnumerable<Part> Parts { get; set; }

    public virtual IEnumerable<Cable> Cables { get; set; }

    public virtual IEnumerable<PartOrder> PartOrders { get; set; }

    public virtual IEnumerable<CableOrder> CableOrders { get; set; }

    public virtual IEnumerable<Equipment> Equipment { get; set; }
}