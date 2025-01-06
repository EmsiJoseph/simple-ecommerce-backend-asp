using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace api.Data.Models;

public partial class Category
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public int? parent_id { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> products { get; set; } = new List<Product>();
}
