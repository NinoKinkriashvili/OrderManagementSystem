using System.Text.Json;
using System.Text.Json.Serialization;
using OrderManagementSystem.Api.Models;
using OrderManagementSystem.Api.Repositories.Interfaces;

namespace OrderManagementSystem.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
    public OrderRepository()
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "orders.json");
    }

    public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Orders file was not found.", _filePath);
        }

        string json = await File.ReadAllTextAsync(_filePath, cancellationToken);

        return JsonSerializer.Deserialize<List<Order>>(json, _jsonOptions)
               ?? new List<Order>();
    }
}