namespace OrderManagementSystem.Api.DTOs;

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
