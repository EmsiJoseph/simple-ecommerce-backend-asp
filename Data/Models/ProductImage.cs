using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class ProductImage
{
    public int id { get; set; }

    public int product_id { get; set; }

    public string image_url { get; set; } = null!;

    public bool is_main { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual Product product { get; set; } = null!;
}
