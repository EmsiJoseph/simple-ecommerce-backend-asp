using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class OrderItem
{
    public int id { get; set; }

    public int order_id { get; set; }

    public int product_id { get; set; }

    public int qty { get; set; }

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual Order order { get; set; } = null!;

    public virtual Product product { get; set; } = null!;
}
