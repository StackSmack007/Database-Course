namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    internal class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            
            builder.Property(e => e.SwiftCode)
              .HasMaxLength(20)
              .IsUnicode(false)
              .IsRequired();
        }
    }
}