using OrderManagementSystem.Api.Models;
using OrderManagementSystem.Api.Repositories;

namespace OrderManagementSystem.Tests.Repositories;

public class OrderRepositoryTests
{
    [Fact]
    public async Task GetAllOrdersAsync_ReturnsAllOrders()
    {
        // Arrange
        OrderRepository repository = new();

        // Act
        List<Order> orders = await repository.GetAllOrdersAsync(
            CancellationToken.None);

        // Assert
        Assert.NotNull(orders);
        Assert.Equal(4, orders.Count);
    }

    [Fact]
    public async Task GetAllOrdersAsync_DeserializesOrderDataCorrectly()
    {
        // Arrange
        OrderRepository repository = new();

        // Act
        List<Order> orders = await repository.GetAllOrdersAsync(
            CancellationToken.None);

        // Assert
        Order firstOrder = orders[0];

        Assert.Equal(1, firstOrder.OrderId);
        Assert.Equal("Nino", firstOrder.Customer);
        Assert.Equal(OrderStatus.Completed, firstOrder.Status);

        Assert.Equal(2, firstOrder.Items.Count);
        Assert.Equal("Keyboard", firstOrder.Items[0].Product);
        Assert.Equal(2, firstOrder.Items[0].Quantity);
        Assert.Equal(50, firstOrder.Items[0].Price);
    }
}