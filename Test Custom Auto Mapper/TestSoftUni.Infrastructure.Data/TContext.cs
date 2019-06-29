namespace TestSoftUni.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using TestSoftUni.Infrastructure.Data.Configuration;
    using TestSoftUni.Infrastructure.Models.Models;

    public class TContext:DbContext
    {
       public DbSet<Employee> Employees { get; set; }
        public TContext() { }
       
        public TContext(DbContextOptions options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}