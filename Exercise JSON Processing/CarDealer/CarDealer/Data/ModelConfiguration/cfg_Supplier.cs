namespace CarDealer.Data.ModelConfiguration
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    public class cfg_Supplier : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Name).HasMaxLength(64).IsUnicode().IsRequired();
        }
    }
}