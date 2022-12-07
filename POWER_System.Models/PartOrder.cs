#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POWER_System.Models.Constants;
using POWER_System.Models.Contracts;
using POWER_System.Models.Enum;

namespace POWER_System.Models;

public class PartOrder : IOrder
{
    [Key]
    public Guid Id { get; set; }

    ///// <summary>
    ///// Gets or sets the project which the parts are ordered for.
    ///// </summary>
    //[ForeignKey(nameof(Project))]
    //public Guid ProjectId { get; set; }

    //[Required]
    //public Project Project { get; set; }

    /// <summary>
    /// Gets or sets the enclosure TAG for which are the parts ordered.
    /// </summary>
    public Guid EnclosureId { get; set; }

    public Enclosure Enclosure { get; set; }

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

    public DateTime DateCreated { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedOn { get; set; }

    public List<EnclosurePartOrder> EnclosureParts { get; set; }
}
