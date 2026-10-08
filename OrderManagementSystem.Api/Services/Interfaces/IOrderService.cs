using OrderManagementSystem.Api.DTOs;

namespace OrderManagementSystem.Api.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponseDto>> GetAllOrdersAsync(CancellationToken cancellationToken);

    Task<List<OrderResponseDto>> GetOrdersByCustomerAsync(string customerName, CancellationToken cancellationToken);

    Task<OrderStatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken);
}