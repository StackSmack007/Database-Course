namespace Cinema.Data
{
    using Cinema.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Projection> Projections { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public CinemaContext() { }

        public CinemaContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<Ticket>().HasOne(t => t.Customer).WithMany(c => c.Tickets).HasForeignKey(t => t.Customer);
        }
    }
}