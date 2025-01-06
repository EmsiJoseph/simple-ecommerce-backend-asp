using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class CartItem
{
    public int id { get; set; }

    public int cart_id { get; set; }

    public int product_id { get; set; }

    public int qty { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual Cart cart { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
