using POWER_System.Models.Enum;

namespace POWER_System.Services.Models;

public class PartServiceModel
{
    public int Id { get; set; }

    public string DeviceTag { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string OrderNumber { get; set; } = null!;

    public string? Comment { get; set; }

    public double Quantity { get; set; }

    public virtual IEnumerable<TagQuantityServiceModel>? TagsQuantity { get; set; }

    public OrderDelivery Delivery { get; set; }
}
