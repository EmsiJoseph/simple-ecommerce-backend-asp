using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class ProductImageDto
{
    [Required(ErrorMessage = "Product ID is required")]
    public int product_id { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    [Url(ErrorMessage = "Please provide a valid URL")]
    public string image_url { get; set; } = string.Empty;

    public bool is_main { get; set; } = false;
}
