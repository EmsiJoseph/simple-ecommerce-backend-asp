using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__cart_ite__3213E83F64EB7828");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.qty).HasDefaultValue(1);
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.cart).WithMany(p => p.cart_items)
            .HasForeignKey(d => d.cart_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_cart_items_cart");

        builder.HasOne(d => d.product).WithMany(p => p.cart_items)
            .HasForeignKey(d => d.product_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_cart_items_product");
    }
}
