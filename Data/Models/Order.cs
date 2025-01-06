using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class Order
{
    public int id { get; set; }

    public int customer_id { get; set; }

    public int? card_id { get; set; }

    public string status { get; set; }

    public string? shipping_address_line1 { get; set; }

    public string? shipping_address_line2 { get; set; }

    public string? shipping_city { get; set; }

    public string? shipping_state { get; set; }

    public string? shipping_zip_code { get; set; }

    public string? shipping_country { get; set; }

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual Card? card { get; set; }

    public virtual User customer { get; set; } = null!;

    public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

}
