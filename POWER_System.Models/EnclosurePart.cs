using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POWER_System.Models;

public class EnclosurePart
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Part))]
    public int PartId { get; set; }

    [ForeignKey(nameof(Enclosure))]
    public Guid EnclosureId { get; set; }
    
    public Part Part { get; set; }

    public Enclosure Enclosure { get; set; }

    public double Quantity { get; set; }

    public List<PartTagQuantity> PartsQuantity { get; set; } = new List<PartTagQuantity>();
}