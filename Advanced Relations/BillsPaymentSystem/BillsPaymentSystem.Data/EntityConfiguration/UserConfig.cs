namespace BillsPaymentSystem.Data.EntityConfiguration
{
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.LastName)
                   .HasMaxLength(50)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasMaxLength(80)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.Password)
                   .HasMaxLength(25)
                   .IsUnicode(false)
                   .IsRequired();
        }
    }
}