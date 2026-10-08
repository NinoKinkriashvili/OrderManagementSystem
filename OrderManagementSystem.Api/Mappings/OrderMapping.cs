using OrderManagementSystem.Api.DTOs;
using OrderManagementSystem.Api.Models;

namespace OrderManagementSystem.Api.Mappings;

public static class OrderMapping
{
    public static OrderResponseDto MapToOrderResponseDto(Order order) => new()
    {
        OrderId = order.OrderId,
        CustomerName = order.Customer,
        Status = order.Status.ToString(),
        Items = order.Items
            .Select(item => new OrderItemDto
            {
                Product = item.Product,
                Quantity = item.Quantity,
                Price = item.Price
            })
            .ToList()
    };
}