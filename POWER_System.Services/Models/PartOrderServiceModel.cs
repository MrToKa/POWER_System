using POWER_System.Models;
using POWER_System.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace POWER_System.Services.Models;

public class PartOrderServiceModel
{
    [Key]
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Project? Project { get; set; }

    public Guid EnclosureId { get; set; }

    public Enclosure? Enclosure { get; set; }

    //public string EnclosureTag { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public string? Comment { get; set; }
    
    public DateTime DateCreated { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public List<EnclosurePart> Parts { get; set; } = new List<EnclosurePart>();
}