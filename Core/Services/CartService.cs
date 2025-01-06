using api.Core.DTOs;
using api.Core.Interfaces;
using api.Data;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Core.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartByCustomerIdAsync(int customerId)
    {
        return await _context.Carts
            .Include(c => c.cart_items)
            .ThenInclude(ci => ci.product)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);
    }

    public async Task<Cart> CreateCartAsync(CartDto cartDto)
    {
        var cart = new Cart
        {
            customer_id = cartDto.customer_id,
            created_at = DateTime.UtcNow,
            updated_at = DateTime.UtcNow
        };

        if (cartDto.cart_items != null)
        {
            cart.cart_items = cartDto.cart_items.Select(item => new CartItem
            {
                product_id = item.product_id,
                qty = item.qty,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            }).ToList();
        }

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart?> UpdateCartAsync(int customerId, CartDto cartDto)
    {
        var cart = await _context.Carts
            .Include(c => c.cart_items)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);

        if (cart == null) return null;

        if (cartDto.cart_items != null)
        {
            _context.CartItems.RemoveRange(cart.cart_items);
            cart.cart_items = cartDto.cart_items.Select(item => new CartItem
            {
                product_id = item.product_id,
                qty = item.qty,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            }).ToList();
        }

        cart.updated_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<bool> DeleteCartAsync(int customerId)
    {
        var cart = await _context.Carts
            .Include(c => c.cart_items)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);

        if (cart == null) return false;

        _context.CartItems.RemoveRange(cart.cart_items);
        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CartItem?> AddItemToCartAsync(int customerId, CartItemDto cartItemDto)
    {
        var cart = await _context.Carts
            .Include(c => c.cart_items)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);

        if (cart == null)
        {
            cart = await CreateCartAsync(new CartDto { customer_id = customerId });
        }

        var existingItem = cart.cart_items
            .FirstOrDefault(i => i.product_id == cartItemDto.product_id);

        if (existingItem != null)
        {
            existingItem.qty += cartItemDto.qty;
            existingItem.updated_at = DateTime.UtcNow;
        }
        else
        {
            var newItem = new CartItem
            {
                cart_id = cart.id,
                product_id = cartItemDto.product_id,
                qty = cartItemDto.qty,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };
            cart.cart_items.Add(newItem);
            existingItem = newItem;
        }

        cart.updated_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return existingItem;
    }

    public async Task<bool> RemoveItemFromCartAsync(int customerId, int productId)
    {
        var cart = await _context.Carts
            .Include(c => c.cart_items)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);

        if (cart == null) return false;

        var item = cart.cart_items.FirstOrDefault(i => i.product_id == productId);
        if (item == null) return false;

        cart.cart_items.Remove(item);
        cart.updated_at = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CartItem?> UpdateCartItemQuantityAsync(int customerId, int productId, int qty)
    {
        var cart = await _context.Carts
            .Include(c => c.cart_items)
            .FirstOrDefaultAsync(c => c.customer_id == customerId);

        if (cart == null) return null;

        var item = cart.cart_items.FirstOrDefault(i => i.product_id == productId);
        if (item == null) return null;

        item.qty = qty;
        item.updated_at = DateTime.UtcNow;
        cart.updated_at = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return item;
    }
}
