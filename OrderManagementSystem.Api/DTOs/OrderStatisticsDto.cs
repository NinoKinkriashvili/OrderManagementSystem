namespace OrderManagementSystem.Api.DTOs;

public class OrderStatisticsDto
{
    public int CompletedOrdersCount { get; set; }
    public decimal TotalSales { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List<string> MostPopularProducts { get; set; } = new List<string>();
}