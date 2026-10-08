using OrderManagementSystem.Api.DTOs;
using OrderManagementSystem.Api.Models;
using OrderManagementSystem.Api.Repositories.Interfaces;
using OrderManagementSystem.Api.Services;

namespace OrderManagementSystem.Tests.Services;

public class OrderServiceTests
{
    private readonly List<Order> _orders =
    [
        new Order
        {
            OrderId = 1,
            Customer = "Nino",
            Status = OrderStatus.Completed,
            Items =
            [
                new OrderItem
                {
                    Product = "Keyboard",
                    Quantity = 2,
                    Price = 50
                },
                new OrderItem
                {
                    Product = "Mouse",
                    Quantity = 1,
                    Price = 25
                }
            ]
        },
        new Order
        {
            OrderId = 2,
            Customer = "Giorgi",
            Status = OrderStatus.Cancelled,
            Items =
            [
                new OrderItem
                {
                    Product = "Keyboard",
                    Quantity = 1,
                    Price = 50
                }
            ]
        },
        new Order
        {
            OrderId = 3,
            Customer = "Nino",
            Status = OrderStatus.Completed,
            Items =
            [
                new OrderItem
                {
                    Product = "Mouse",
                    Quantity = 2,
                    Price = 25
                },
                new OrderItem
                {
                    Product = "Monitor",
                    Quantity = 1,
                    Price = 200
                }
            ]
        }
    ];

    [Fact]
    public async Task GetAllOrdersAsync_ReturnsAllOrders()
    {
        // Arrange
        IOrderRepository repository = new FakeOrderRepository(_orders);
        OrderService service = new(repository);

        // Act
        List<OrderResponseDto> result =
            await service.GetAllOrdersAsync();

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetOrdersByCustomerAsync_ReturnsOnlyMatchingOrders()
    {
        // Arrange
        IOrderRepository repository = new FakeOrderRepository(_orders);
        OrderService service = new(repository);

        // Act
        List<OrderResponseDto> result =
            await service.GetOrdersByCustomerAsync("Nino");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(
            result,
            order => Assert.Equal("Nino", order.CustomerName));
    }

    [Fact]
    public async Task GetStatisticsAsync_ReturnsCorrectStatistics()
    {
        // Arrange
        IOrderRepository repository = new FakeOrderRepository(_orders);
        OrderService service = new(repository);

        // Act
        OrderStatisticsDto result =
            await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(2, result.CompletedOrdersCount);
        Assert.Equal(375, result.TotalSales);
        Assert.Equal(187.50m, result.AverageOrderValue);

        Assert.Single(result.MostPopularProducts);
        Assert.Equal("Mouse", result.MostPopularProducts[0]);
    }
    
    [Fact]
    public async Task GetOrdersByCustomerAsync_WhenCustomerNotFound_ReturnsEmptyList()
    {
        // Arrange
        IOrderRepository repository = new FakeOrderRepository(_orders);
        OrderService service = new(repository);

        // Act
        List<OrderResponseDto> result =
            await service.GetOrdersByCustomerAsync("Unknown");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetOrdersByCustomerAsync_WhenCustomerNameIsEmpty_ReturnsEmptyList()
    {
        // Arrange
        IOrderRepository repository = new FakeOrderRepository(_orders);
        OrderService service = new(repository);

        // Act
        List<OrderResponseDto> result =
            await service.GetOrdersByCustomerAsync("");

        // Assert
        Assert.Empty(result);
    }
    
}