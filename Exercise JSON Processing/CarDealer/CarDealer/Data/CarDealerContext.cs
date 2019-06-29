namespace CarDealer.Data
{
    using CarDealer.Data.ModelConfiguration;
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    public class CarDealerContext : DbContext
    {
        public CarDealerContext(DbContextOptions options)
            : base(options)
        { }

        public CarDealerContext()
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=ZEVS-PC\SQLEXPRESS;Database=CarDealer;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new cfg_Car());
            modelBuilder.ApplyConfiguration(new cfg_Customer());
            modelBuilder.ApplyConfiguration(new cfg_Part());
            modelBuilder.ApplyConfiguration(new cfg_PartCar());
            modelBuilder.ApplyConfiguration(new cfg_Sale());
            modelBuilder.ApplyConfiguration(new cfg_Supplier());
        }
    }
}