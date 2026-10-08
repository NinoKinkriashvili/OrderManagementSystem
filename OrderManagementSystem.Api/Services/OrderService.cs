using OrderManagementSystem.Api.DTOs;
using OrderManagementSystem.Api.Mappings;
using OrderManagementSystem.Api.Models;
using OrderManagementSystem.Api.Repositories.Interfaces;
using OrderManagementSystem.Api.Services.Interfaces;

namespace OrderManagementSystem.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<OrderResponseDto>> GetAllOrdersAsync(
        CancellationToken cancellationToken = default)
    {
        List<Order> orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

        return orders
            .Select(OrderMapping.MapToOrderResponseDto)
            .ToList();
    }

    public async Task<List<OrderResponseDto>> GetOrdersByCustomerAsync(
        string customerName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            return new List<OrderResponseDto>();

        List<Order> orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

        return orders
            .Where(order => string.Equals(
                order.Customer,
                customerName,
                StringComparison.OrdinalIgnoreCase))
            .Select(OrderMapping.MapToOrderResponseDto)
            .ToList();
    }

    public async Task<OrderStatisticsDto> GetStatisticsAsync(
        CancellationToken cancellationToken = default)
    {
        List<Order> orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);

        // Status Completed
        List<Order> completedOrders = orders
            .Where(order => order.Status == OrderStatus.Completed)
            .ToList();

        if (completedOrders.Count == 0)
        {
            return new OrderStatisticsDto
            {
                CompletedOrdersCount = 0,
                TotalSales = 0,
                AverageOrderValue = 0,
                MostPopularProducts = new List<string>()
            };
        }

        // Sum, AVG
        decimal totalSales = completedOrders
            .SelectMany(order => order.Items)
            .Sum(item => item.Quantity * item.Price);

        decimal averageOrderValue = totalSales / completedOrders.Count;

        // Popular products
        var productQuantities = completedOrders
            .SelectMany(order => order.Items)
            .GroupBy(item => item.Product)
            .Select(group => new
            {
                Product = group.Key,
                TotalQuantity = group.Sum(item => item.Quantity)
            })
            .ToList();

        int maxQuantity = productQuantities.Max(p => p.TotalQuantity);

        List<string> mostPopularProducts = productQuantities
            .Where(p => p.TotalQuantity == maxQuantity)
            .Select(p => p.Product)
            .ToList();

        return new OrderStatisticsDto
        {
            CompletedOrdersCount = completedOrders.Count,
            TotalSales = totalSales,
            AverageOrderValue = Math.Round(averageOrderValue, 2),
            MostPopularProducts = mostPopularProducts
        };
    }
}