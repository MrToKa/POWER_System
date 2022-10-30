using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POWER_System.Models.Enum;
using POWER_System.Models.Constants;

namespace POWER_System.Models;

public class Cable
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the cable. Usually is the KKS of the cable.
    /// </summary>
    [Required]
    [MaxLength(CableConstants.CableNameMaxLength)]

    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the cable type. Number of conductors or cross section should not be written here.
    /// </summary>
    [Required]
    [MaxLength(CableConstants.CableTypeMaxLength)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// Gets or sets the number of conductor in the cable. All of the conductors should be written, not only the used ones.
    /// </summary>
    [Required]
    public int Conductors { get; set; }

    /// <summary>
    /// Gets or sets the cross section of the individual conductors in the cable. 
    /// </summary>
    [Required]
    public string CrossSection { get; set; } = null!;

    /// <summary>
    /// Gets or sets the starting location of the cable. Usually this is the KKS of the enclosure.
    /// </summary>
    [Required]
    public string FromLocation { get; set; } = null!;

    /// <summary>
    /// Gets or sets description of the starting location. It consist of a descriptive text of the location.
    /// </summary>
    [Required]

    public string FromLocationDescription { get; set; } = null!;

    /// <summary>
    /// Gets or sets the target location of the cable. Usually this is the KKS of the consumer.
    /// </summary>
    [Required]
    public string ToLocation { get; set; } = null!;

    /// <summary>
    /// Gets or sets description of the target location. It consist of a descriptive text of the consumer and its purpose. It can include a system name too.
    /// </summary>
    [Required]
    public string ToLocationDescription { get; set; } = null!;

    /// <summary>
    /// Gets or sets the voltage level of the cable.
    /// </summary>
    [Required]
    public int Voltage { get; set; }

    /// <summary>
    /// Gets or sets the routing path for the cable. It consists of cable trays and conduits. The routing should be done in from -> to location order.
    /// </summary>
    public string? Routing { get; set; }

    /// <summary>
    /// Gets or sets the design length according the project documentation.
    /// </summary>
    [Required]
    public int DesignLength { get; set; }

    /// <summary>
    /// Gets or sets the install length according the site situation.
    /// </summary>
    public int? InstallLength { get; set; }

    /// <summary>
    /// Gets or sets the date when the cable has been pulled.
    /// </summary>
    public DateTime? PullDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the cable has been connected on from location.
    /// </summary>
    public DateTime? ConnectedFrom { get; set; }

    /// <summary>
    /// Gets or sets the date when the cable has been connected to the consumer.
    /// </summary>
    public DateTime? ConnectedTo { get; set; }

    /// <summary>
    /// Gets or sets the date when the cable is tested for continuity.
    /// </summary>
    public DateTime? TestedDate { get; set; }

    /// <summary>
    /// Gets or sets the remarks for changes on anything unusual about the cable.
    /// </summary>
    public string? Remarks { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    [ForeignKey(nameof(Enclosure))]
    public Guid EnclosureId { get; set; }

    public Enclosure Enclosure { get; set; }

    /// <summary>
    /// Gets or sets the last date when something about the cable has been changed.
    /// </summary>
    [Required]
    public DateTime LastChangeDateTime { get; set; }

    /// <summary>
    /// Gets or sets the current status of the cable. It is a preset enumeration and the status can be - Waiting for delivery, Not pulled, Pulled, Connected, Tested.
    /// </summary>
    public CableStatus Status { get; set; }
}
