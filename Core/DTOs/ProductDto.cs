using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class ProductDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 255 characters")]
    public string name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    public string description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal price { get; set; }

    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be 0 or greater")]
    public int stock { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public int category_id { get; set; }
}
