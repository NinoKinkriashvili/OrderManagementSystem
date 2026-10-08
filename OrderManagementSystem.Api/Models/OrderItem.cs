namespace OrderManagementSystem.Api.Models;

public class OrderItem
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}