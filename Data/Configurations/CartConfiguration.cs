using api.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__cart__3213E83F3DF31592");

        builder.ToTable("cart");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.status)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.customer).WithMany(p => p.carts)
            .HasForeignKey(d => d.customer_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_cart_customer");
    }
}
