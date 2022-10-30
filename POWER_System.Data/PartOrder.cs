#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POWER_System.Data.Constants;
using POWER_System.Data.Contracts;
using POWER_System.Data.Enum;

namespace POWER_System.Data;

public class PartOrder : IOrder
{
    public PartOrder()
    {
        Parts = new HashSet<Part>();
    }

    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the project which the parts are ordered for.
    /// </summary>
    [ForeignKey(nameof(Project))]
    public Guid ProjectId { get; set; }

    [Required]
    public Project Project { get; set; }

    /// <summary>
    /// Gets or sets the enclosure TAG for which are the parts ordered.
    /// </summary>
    //[ForeignKey(nameof(Enclosure))]
    //public string EnclosureId { get; set; }

    //public Enclosure Enclosure { get; set; }

    /// <summary>
    /// Gets or sets the order date.
    /// </summary>
    [Required]
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Gets or sets the current status of the order. Preset enumeration - In review, ordered, delivered.
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Gets or sets any comments about the order like delivery time and so on.
    /// </summary>
    [MaxLength(OrderConstants.CommentMaxLength)]
    public string? Comment { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual IEnumerable<Part> Parts { get; set; }
}
