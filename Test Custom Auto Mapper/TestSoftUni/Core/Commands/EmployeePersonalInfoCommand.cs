namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using TestSoftUni.Infrastructure.Data;
    public class EmployeePersonalInfoCommand : BaseCommand
    {
        public EmployeePersonalInfoCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            int empId = int.Parse(input[0]);

            var emp = context.Employees.FirstOrDefault(x => x.Id == empId);


  StringBuilder sb = new StringBuilder();
  sb.AppendLine($"ID: {emp.Id} - {emp.FirstName} {emp.LastName} - ${emp.Salary:f2}");
  sb.AppendLine($"Birthday: {emp.Birthday.Value.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture)}");
  sb.AppendLine($"Address: {emp.Address}");

            return sb.ToString().Trim();
        }
    }
}