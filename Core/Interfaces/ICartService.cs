using api.Core.DTOs;
using api.Data.Models;

namespace api.Core.Interfaces;

public interface ICartService
{
    Task<Cart?> GetCartByCustomerIdAsync(int customerId);
    Task<Cart> CreateCartAsync(CartDto cartDto);
    Task<Cart?> UpdateCartAsync(int customerId, CartDto cartDto);
    Task<bool> DeleteCartAsync(int customerId);
    Task<CartItem?> AddItemToCartAsync(int customerId, CartItemDto cartItemDto);
    Task<bool> RemoveItemFromCartAsync(int customerId, int productId);
    Task<CartItem?> UpdateCartItemQuantityAsync(int customerId, int productId, int quantity);
}
