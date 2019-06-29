namespace BillsPaymentSystem.Data
{
    using BillsPaymentSystem.Data.EntityConfiguration;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    public class BillsPaymentSystemContext : DbContext
    {
        public BillsPaymentSystemContext() : base()
        { }

        public BillsPaymentSystemContext(DbContextOptions opt) : base(opt)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString, x => x.MigrationsAssembly("BillsPaymentSystem.Data"));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountConfig());
            modelBuilder.ApplyConfiguration(new CreditCardConfig());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}