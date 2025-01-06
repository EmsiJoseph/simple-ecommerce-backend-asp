using api.Core.DTOs;
using api.Data.Models;

namespace api.Core.Interfaces;

public interface IOrderService : IBaseService<Order>
{
    Task<Order> CreateAsync(OrderDto orderDto);
    Task<Order?> UpdateAsync(int id, OrderDto orderDto);
}
