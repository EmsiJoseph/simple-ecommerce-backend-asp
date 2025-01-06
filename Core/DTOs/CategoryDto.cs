using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class CategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string name { get; set; } = string.Empty;

    public string? description { get; set; }

    public int? parent_id { get; set; }
}
