namespace CarDealer.Data.ModelConfiguration
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class cfg_Car : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.Make).HasMaxLength(64).IsUnicode().IsRequired();
            builder.Property(c => c.Model).HasMaxLength(64).IsUnicode().IsRequired();
        }
    }
}
