using OrderManagementSystem.Api.Models;

namespace OrderManagementSystem.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
}