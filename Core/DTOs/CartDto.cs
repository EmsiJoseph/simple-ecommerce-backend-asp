using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class CartDto
{
    public int id { get; set; }

    [Required]
    public int customer_id { get; set; }

    public ICollection<CartItemDto>? cart_items { get; set; }
}

public class CartItemDto
{
    public int id { get; set; }

    [Required]
    public int product_id { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int qty { get; set; }
}
