namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    internal class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(pm => pm.CreditCard)
                .WithOne(cc => cc.PaymentMethod);

            builder.HasOne(pm => pm.BankAccount)
              .WithOne(ba => ba.PaymentMethod);

            builder
                .HasOne(pm => pm.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(pm=>pm.UserId);
        }
    }
}