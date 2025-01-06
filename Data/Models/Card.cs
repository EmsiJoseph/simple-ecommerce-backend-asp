using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class Card
{
    public int id { get; set; }

    public int customer_id { get; set; }

    public string card_token { get; set; } = null!;

    public string? card_brand { get; set; }

    public string? last4 { get; set; }

    public int? expiration_month { get; set; }

    public int? expiration_year { get; set; }

    public bool is_default { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual User customer { get; set; } = null!;

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();
}
