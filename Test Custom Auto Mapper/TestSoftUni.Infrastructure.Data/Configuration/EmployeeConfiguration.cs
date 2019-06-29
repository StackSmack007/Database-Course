namespace TestSoftUni.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TestSoftUni.Infrastructure.Models.Models;
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        // : first name, last name, salary, birthday and address.Only first name, last name and salary are required.
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).IsUnicode(false).HasMaxLength(32).IsRequired();
            builder.Property(e => e.LastName).IsUnicode(false).HasMaxLength(32).IsRequired();
            builder.Property(e => e.Salary).IsRequired();
            builder.Property(e => e.Address).IsUnicode().HasMaxLength(64).IsRequired(false);

            builder.HasOne(e => e.Manager).WithMany(m => m.Subordinates).HasForeignKey(e => e.ManagerId).IsRequired(false);

        }
    }
}
