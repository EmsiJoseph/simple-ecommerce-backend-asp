using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class OrderService : BaseService<Order>, IOrderService
{
    public OrderService(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.order_items)
            .Where(o => !o.is_deleted)
            .ToListAsync();
    }

    public override async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.order_items)
            .FirstOrDefaultAsync(o => o.id == id && !o.is_deleted);
    }

    public async Task<Order> CreateAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            customer_id = orderDto.customer_id,
            card_id = orderDto.card_id,
            status = orderDto.status,
            shipping_address_line1 = orderDto.shipping_address_line1,
            shipping_address_line2 = orderDto.shipping_address_line2,
            shipping_city = orderDto.shipping_city,
            shipping_state = orderDto.shipping_state,
            shipping_zip_code = orderDto.shipping_zip_code,
            shipping_country = orderDto.shipping_country,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        if (orderDto.order_items != null)
        {
            order.order_items = orderDto.order_items.Select(item => new OrderItem
            {
                product_id = item.product_id,
                qty = item.qty,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            }).ToList();
        }

        await base.CreateAsync(order);
        return order;
    }

    public override async Task<Order?> UpdateAsync(int id, Order entity)
    {
        var order = await _context.Orders
            .Include(o => o.order_items)
            .FirstOrDefaultAsync(o => o.id == id && !o.is_deleted);

        if (order == null) return null;

        order.customer_id = entity.customer_id;
        order.card_id = entity.card_id;
        order.status = entity.status;
        order.shipping_address_line1 = entity.shipping_address_line1;
        order.shipping_address_line2 = entity.shipping_address_line2;
        order.shipping_city = entity.shipping_city;
        order.shipping_state = entity.shipping_state;
        order.shipping_zip_code = entity.shipping_zip_code;
        order.shipping_country = entity.shipping_country;
        order.updated_at = DateTime.UtcNow;

        if (entity.order_items != null)
        {
            _context.OrderItems.RemoveRange(order.order_items);
            order.order_items = entity.order_items;
        }

        await _context.SaveChangesAsync();
        return order;
    }

    public override async Task<bool> SoftDeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return false;

        order.is_deleted = true;
        order.deleted_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<bool> HardDeleteAsync(int id)
    {
        return await DeleteAsync(id);
    }

    public override async Task<IEnumerable<Order>> GetDeletedAsync()
    {
        return await _context.Orders
            .Include(o => o.order_items)
            .Where(o => o.is_deleted)
            .ToListAsync();
    }

    public override async Task<bool> RestoreAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return false;

        order.is_deleted = false;
        order.deleted_at = null;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Order?> UpdateAsync(int id, OrderDto orderDto)
    {
        var order = await _context.Orders
            .Include(o => o.order_items)
            .FirstOrDefaultAsync(o => o.id == id && !o.is_deleted);

        if (order == null) return null;

        order.customer_id = orderDto.customer_id;
        order.card_id = orderDto.card_id;
        order.status = orderDto.status;
        order.shipping_address_line1 = orderDto.shipping_address_line1;
        order.shipping_address_line2 = orderDto.shipping_address_line2;
        order.shipping_city = orderDto.shipping_city;
        order.shipping_state = orderDto.shipping_state;
        order.shipping_zip_code = orderDto.shipping_zip_code;
        order.shipping_country = orderDto.shipping_country;
        order.updated_at = DateTime.UtcNow;

        if (orderDto.order_items != null)
        {
            _context.OrderItems.RemoveRange(order.order_items);
            order.order_items = orderDto.order_items.Select(item => new OrderItem
            {
                product_id = item.product_id,
                qty = item.qty,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            }).ToList();
        }

        await _context.SaveChangesAsync();
        return order;
    }
}
