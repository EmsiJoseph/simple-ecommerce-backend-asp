namespace api.Data.Models;

public partial class User
{
    public int id { get; set; }

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string gender { get; set; }

    public string? phone { get; set; }

    public string? address_line1 { get; set; }

    public string? address_line2 { get; set; }

    public string? city { get; set; }

    public string? state { get; set; }

    public string? zip_code { get; set; }

    public string? country { get; set; }

    public DateOnly? date_of_birth { get; set; }

    public bool is_deleted { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual Account? account { get; set; }

    public virtual ICollection<Card> cards { get; set; } = new List<Card>();

    public virtual ICollection<Cart> carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> orders { get; set; } = new List<Order>();

}
