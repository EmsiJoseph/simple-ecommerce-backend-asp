using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__products__3213E83FEE7E3AB1");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.Property(e => e.price).HasColumnType("decimal(18, 0)");

        builder.HasOne(e => e.category)
            .WithMany(c => c.products)
            .HasForeignKey(e => e.category_id);
    }
}
