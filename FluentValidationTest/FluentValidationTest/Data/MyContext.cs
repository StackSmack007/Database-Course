namespace FluentValidationTest.Data
{
using FluentValidationTest.Model;
using Microsoft.EntityFrameworkCore;
    public class MyContext : DbContext
    {
        private const string connectionString = @"Server=ZEVS-PC\SQLEXPRESS;Database=Test7-6-2019;Integrated Security=true";
        public MyContext() : base()
        { }

        public MyContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Town> Towns { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//TODOSomething
            base.OnModelCreating(modelBuilder);
        }
    }
}
