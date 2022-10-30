using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using POWER_System.Models.Enum;
using POWER_System.Models.Constants;

namespace POWER_System.Models;
public class Enclosure
{
    public Enclosure()
    {
        Parts = new List<Part>();
        Cables = new HashSet<Cable>();
        CablesOrders = new List<CableOrder>();
    }

    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the process-related code that identifies installations and equipment according to their assigned task in the plant process.
    /// </summary>
    public string? Plant { get; set; }

    /// <summary>
    /// Gets or sets the location code that identifies the rooms and floors, or other installation sites, for installations and equipment in building structures.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the KKS code that identifies equipment according to its assigned task in the  plant process.
    /// </summary>
    [Required]
    public string Tag { get; set; }

    /// <summary>
    /// Gets or sets the current phase of the enclosure. It is a preset enumeration and the phase can be - Design, Manufacturing, In warehouse , Delivered, Mounted.
    /// </summary>
    [Required]
    public EnclosureStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the current revision of the drawing.
    /// </summary>
    [Required]
    public string Revision { get; set; }

    /// <summary>
    /// Gets or sets comments about the enclosure like specific requirements about the design, manufacturing or installation.
    /// </summary>
    [MaxLength(EnclosureConstants.CommentMaxLength)]
    public string? Comment { get; set; }

    [ForeignKey(nameof(Project))]
    [Required]
    public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; }

    // TODO: Implement documentation class for the enclosure.

    public virtual IEnumerable<Part> Parts { get; set; }

    public virtual IEnumerable<Cable> Cables { get; set; }

    public virtual IEnumerable<CableOrder> CablesOrders { get; set; }
}
