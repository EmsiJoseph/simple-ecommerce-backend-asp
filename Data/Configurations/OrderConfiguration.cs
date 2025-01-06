using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__orders__3213E83F15B048FF");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.shipping_address_line1)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.shipping_address_line2)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.shipping_city)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.shipping_country)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.shipping_state)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.shipping_zip_code)
            .HasMaxLength(20)
            .IsUnicode(false);
        builder.Property(e => e.status)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(e => e.is_deleted).HasDefaultValue(false);
        builder.Property(e => e.deleted_at).HasColumnType("datetime");
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.card).WithMany(p => p.orders)
            .HasForeignKey(d => d.card_id)
            .HasConstraintName("FK_orders_card");

        builder.HasOne(d => d.customer).WithMany(p => p.orders)
            .HasForeignKey(d => d.customer_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_orders_customer");
    }
}
