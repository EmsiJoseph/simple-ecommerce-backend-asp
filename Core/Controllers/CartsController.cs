using api.Core.DTOs;
using api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCart(int customerId)
    {
        try
        {
            var cart = await _cartService.GetCartByCustomerIdAsync(customerId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CartDto cartDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var cart = await _cartService.CreateCartAsync(cartDto);
            return CreatedAtAction(nameof(GetCart), new { customerId = cart.customer_id }, cart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("{customerId}/items")]
    public async Task<IActionResult> AddItemToCart(int customerId, [FromBody] CartItemDto itemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var item = await _cartService.AddItemToCartAsync(customerId, itemDto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{customerId}/items/{productId}")]
    public async Task<IActionResult> UpdateCartItemQuantity(int customerId, int productId, [FromBody] int quantity)
    {
        try
        {
            var item = await _cartService.UpdateCartItemQuantityAsync(customerId, productId, quantity);
            if (item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{customerId}/items/{productId}")]
    public async Task<IActionResult> RemoveItemFromCart(int customerId, int productId)
    {
        try
        {
            var result = await _cartService.RemoveItemFromCartAsync(customerId, productId);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCart(int customerId)
    {
        try
        {
            var result = await _cartService.DeleteCartAsync(customerId);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
