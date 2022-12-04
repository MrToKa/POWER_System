using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POWER_System.Models.Enum;

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

    public string Tag { get; set; }

    public double Quantity { get; set; }

    public Guid? PartOrderId { get; set; }

    public PartOrder PartOrder { get; set; }

    public OrderDelivery Delivery { get; set; }   
}