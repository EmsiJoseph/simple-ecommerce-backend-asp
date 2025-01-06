using System;
using System.Collections.Generic;

namespace api.Data.Models;

public partial class Account
{
    public int id { get; set; }

    public required int user_id { get; set; }

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string role { get; set; }

    public DateTime? last_login { get; set; }

    public string account_status { get; set; } = "Active";

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual User? user { get; set; }
}
