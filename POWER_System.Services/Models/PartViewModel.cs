using POWER_System.Models.Enum;

namespace POWER_System.Services.Models;

public class PartViewModel
{
    public string Manufacturer { get; set; }

    public string Description { get; set; }

    public string OrderNumber { get; set; }

    public string Measure { get; set; }

    public int Quantity { get; set; }
    
    public decimal? Price { get; set; }

    public OrderDelivery Delivery { get; set; }
}