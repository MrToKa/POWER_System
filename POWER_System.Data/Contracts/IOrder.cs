using POWER_System.Data.Enum;

namespace POWER_System.Data.Contracts;

public interface IOrder
{
    Project Project { get; set; }

    //Enclosure Enclosure { get; set; }

    DateTime OrderDate { get; set; }

    OrderStatus Status { get; set; }

    string Comment { get; set; }
}
