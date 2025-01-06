using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(e => e.id).HasName("PK__cards__3213E83F6207BED5");

        builder.Property(e => e.card_brand)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.card_token)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.created_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.last4)
            .HasMaxLength(4)
            .IsUnicode(false);
        builder.Property(e => e.updated_at)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        builder.HasOne(d => d.customer).WithMany(p => p.cards)
            .HasForeignKey(d => d.customer_id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_cards_customer");
    }
}
