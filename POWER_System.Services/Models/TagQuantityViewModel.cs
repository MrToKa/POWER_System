using POWER_System.Models;

namespace POWER_System.Services.Models;

public class TagQuantityViewModel
{
    public EnclosurePart EnclosurePart { get; set; }

    public string Tag { get; set; }

    public double Quantity { get; set; }
}