namespace OrderManagementSystem.Api.Models;

public class Order
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; }
}