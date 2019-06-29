using FastFood.Data.ConfigureClasses;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
        public virtual DbSet<Position> Positions {get;set;}
        public virtual DbSet<OrderItem> OrderItems {get;set;}
        public virtual DbSet<Order> Orders {get;set;}
        public virtual DbSet<Item> Items {get;set;}
        public virtual DbSet<Employee> Employees {get;set;}
        public virtual DbSet<Category> Categories {get;set; }

        public FastFoodDbContext()
        { }

        public FastFoodDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ItemCFG());
            builder.ApplyConfiguration(new OrderItemsCFG());
            builder.ApplyConfiguration(new PositionCFG());
        }
    }
}