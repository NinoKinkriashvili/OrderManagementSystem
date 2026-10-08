using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.DTOs;
using OrderManagementSystem.Api.Services.Interfaces;

namespace OrderManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponseDto>>> GetAllOrders(CancellationToken cancellationToken)
    {
        List<OrderResponseDto> orders =
            await _orderService.GetAllOrdersAsync(cancellationToken);

        return Ok(orders);
    }

    [HttpGet("customer/{customerName}")]
    public async Task<ActionResult<List<OrderResponseDto>>> GetOrdersByCustomer(string customerName,
        CancellationToken cancellationToken)
    {
        List<OrderResponseDto> orders =
            await _orderService.GetOrdersByCustomerAsync(
                customerName,
                cancellationToken);

        return Ok(orders);
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<OrderStatisticsDto>> GetStatistics(CancellationToken cancellationToken)
    {
        OrderStatisticsDto statistics =
            await _orderService.GetStatisticsAsync(cancellationToken);

        return Ok(statistics);
    }
}