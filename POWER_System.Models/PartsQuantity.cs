using System.ComponentModel.DataAnnotations;

namespace POWER_System.Models;

public class PartsQuantity
{
    [Key]
    public int Id { get; set; }

    public EnclosurePart EnclosurePart { get; set; }

    public string Tag { get; set; }

    public double Quantity { get; set; }
}