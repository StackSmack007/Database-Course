namespace CarDealer.Data.ModelConfiguration
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    public class cfg_Sale : IEntityTypeConfiguration<Sale>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(s => s.Car).WithMany(c => c.Sales).HasForeignKey(s => s.CarId);
            builder.HasOne(s => s.Customer).WithMany(c => c.Sales).HasForeignKey(s => s.CustomerId);


        }
    }
}
