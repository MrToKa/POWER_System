using POWER_System.Models.Enum;

namespace POWER_System.Models.Contracts;

public interface IOrder
{
    Project Project { get; set; }

    //Enclosure Enclosure { get; set; }

    DateTime OrderDate { get; set; }

    OrderStatus Status { get; set; }

    string Comment { get; set; }
}
