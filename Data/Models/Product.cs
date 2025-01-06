using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Data.Models;

public partial class Product
{
    public int id { get; set; }

    public int? category_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public decimal price { get; set; }

    public int stock { get; set; }

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    [JsonIgnore]
    public virtual ICollection<CartItem> cart_items { get; set; } = new List<CartItem>();

    public virtual Category? category { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();

    [JsonIgnore]
    public virtual ICollection<ProductImage> product_images { get; set; } = new List<ProductImage>();
}
