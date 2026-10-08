using OrderManagementSystem.Api.Models;
using OrderManagementSystem.Api.Repositories.Interfaces;

namespace OrderManagementSystem.Tests.Services;

public class FakeOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders;

    public FakeOrderRepository(List<Order> orders)
    {
        _orders = orders;
    }

    public Task<List<Order>> GetAllOrdersAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_orders);
    }
}