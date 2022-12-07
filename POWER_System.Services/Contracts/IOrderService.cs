using POWER_System.Models;
using POWER_System.Services.Models;

namespace POWER_System.Services.Contracts;

public interface IOrderService
{
    Task AddOrderAsync(Guid enclosureId);

    Task CreatePartsOrder(List<PartServiceModel> model, Guid enclosureId);

    Task<List<PartServiceModel>> GetOrderAsync(Guid id);
}