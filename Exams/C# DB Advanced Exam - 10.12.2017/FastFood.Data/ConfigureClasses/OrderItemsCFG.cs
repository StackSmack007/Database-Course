namespace FastFood.Data.ConfigureClasses
{
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    public class OrderItemsCFG : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => new { x.ItemId, x.OrderId });
        }
    }
}
