using POWER_System.Models;
using POWER_System.Services.Models;
using System.Threading.Tasks;

namespace POWER_System.Services.Contracts;

public interface IOrderService
{
    Task AddOrderAsync(Guid enclosureId);

    Task<IEnumerable<PartOrderServiceModel>> GetAllOrders();

    Task<IEnumerable<PartOrderServiceModel>> GetAllOrdersForProjectsAsync(Guid id);

    Task CreatePartsOrder(List<PartServiceModel> model, Guid enclosureId);

    Task<List<PartServiceModel>> GetOrderAsync(Guid id, string order);
}