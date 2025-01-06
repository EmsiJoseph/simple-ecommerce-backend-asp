using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__order_it__3213E83F3D1FF0A8");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.qty).HasDefaultValue(1);
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.order).WithMany(p => p.order_items)
            .HasForeignKey(d => d.order_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_order_items_order");
        
        builder.Property(e => e.is_deleted).HasDefaultValue(false);
        builder.Property(e => e.deleted_at).HasColumnType("datetime");

        builder.HasOne(d => d.product).WithMany(p => p.order_items)
            .HasForeignKey(d => d.product_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_order_items_product");
    }
}
