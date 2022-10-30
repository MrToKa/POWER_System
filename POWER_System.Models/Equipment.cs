using System.ComponentModel.DataAnnotations;
using POWER_System.Models.Constants;

namespace POWER_System.Models;

public class Equipment
{
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the equipment.
    /// </summary>
    [Required]
    [MaxLength(EquipmentConstants.NameMaxLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the describes the equipment.
    /// </summary>
    [MaxLength(EquipmentConstants.DescriptionMaxLength)]
    public string? Description { get; set; }

    //public string UserId { get; set; }

    //public User User { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}