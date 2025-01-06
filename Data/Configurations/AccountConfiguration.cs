using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.account_status)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(a => a.role)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(a => a.user)
            .WithOne(u => u.account)
            .HasForeignKey<Account>(a => a.user_id)
            .IsRequired();
    }
}
