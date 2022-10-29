namespace POWER_System.Data.Contracts;

public interface IOrder
{
    Project Project { get; set; }

    //Enclosure Enclosure { get; set; }

    DateTime OrderDate { get; set; }

    int Status { get; set; }

    string Comment { get; set; }
}
