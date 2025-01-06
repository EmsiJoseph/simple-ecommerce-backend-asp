using api.Core.DTOs;
using api.Data.Models;
using api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

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
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return Ok(await _orderService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var order = await _orderService.CreateAsync(orderDto);
        return CreatedAtAction(nameof(GetOrder), new { id = order.id }, order);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(int id, OrderDto orderDto)
    {
        var order = await _orderService.UpdateAsync(id, orderDto);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _orderService.SoftDeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
