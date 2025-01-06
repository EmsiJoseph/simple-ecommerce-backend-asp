using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__categori__3213E83F0E518CA1");

        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.name)
            .HasMaxLength(100)
            .IsUnicode(false);
        builder.Property(e => e.is_deleted).HasDefaultValue(false);
        builder.Property(e => e.deleted_at).HasColumnType("datetime");
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
    }
}
