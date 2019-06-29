namespace CarDealer.Data.ModelConfiguration
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class cfg_PartCar : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(pc => new { pc.CarId, pc.PartId });
            builder.HasOne(pc => pc.Part).WithMany(p => p.PartCars).HasForeignKey(pc => pc.PartId);
            builder.HasOne(pc => pc.Car).WithMany(p => p.PartCars).HasForeignKey(pc => pc.CarId);
        }

    }
}