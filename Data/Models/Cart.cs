using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class Cart
{
    public int id { get; set; }

    public int customer_id { get; set; }

    public string status { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual ICollection<CartItem> cart_items { get; set; } = new List<CartItem>();

    public virtual User customer { get; set; } = null!;

}
