using System.ComponentModel.DataAnnotations;

namespace api.Core.DTOs;

public class CardDto
{
    public int id { get; set; }

    [Required]
    public int customer_id { get; set; }

    [Required]
    public string card_token { get; set; } = null!;

    public string? card_brand { get; set; }

    public string? last4 { get; set; }

    public int? expiration_month { get; set; }

    public int? expiration_year { get; set; }

    public bool is_default { get; set; }
}
