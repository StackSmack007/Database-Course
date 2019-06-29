namespace CarDealer.Data.ModelConfiguration
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class cfg_Part : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Name).HasMaxLength(64).IsUnicode().IsRequired();
            builder.HasOne(p => p.Supplier).WithMany(s => s.Parts).HasForeignKey(p => p.SupplierId);
        }
    }
}