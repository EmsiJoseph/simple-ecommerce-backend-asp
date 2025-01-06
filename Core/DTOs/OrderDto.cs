using System.ComponentModel.DataAnnotations;
using api.Data;

namespace api.Core.DTOs;

public class OrderDto
{
    public int id { get; set; }

    [Required]
    public int customer_id { get; set; }

    public int? card_id { get; set; }

    [Required]
    public string status { get; set; }

    public string? shipping_address_line1 { get; set; }
    public string? shipping_address_line2 { get; set; }
    public string? shipping_city { get; set; }
    public string? shipping_state { get; set; }
    public string? shipping_zip_code { get; set; }
    public string? shipping_country { get; set; }

    public ICollection<OrderItemDto>? order_items { get; set; }
}

public class OrderItemDto
{
    public int id { get; set; }

    [Required]
    public int product_id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int qty { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal price { get; set; }

}

public class OrderCreateDto
{
    public string Status { get; set; }
}

public class OrderUpdateDto
{
    public string? Status { get; set; }
}

public class OrderResponseDto
{
    public string Status { get; set; }
}
