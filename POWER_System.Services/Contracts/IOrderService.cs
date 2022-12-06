using POWER_System.Models;
using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IOrderService
{
    Task AddOrderAsync(List<PartServiceModel> model);

    Task<List<EnclosurePart>> CreatePartsOrder(Guid enclosureId);

}