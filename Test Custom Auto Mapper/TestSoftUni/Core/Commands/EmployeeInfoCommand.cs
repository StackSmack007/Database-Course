namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using System.Linq;
    using TestSoftUni.DTO_s;
    using TestSoftUni.Infrastructure.Data;
    public class EmployeeInfoCommand : BaseCommand
    {
        public EmployeeInfoCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            int empId=int.Parse(input[0]);

            var emp = context.Employees.FirstOrDefault(x => x.Id == empId);
            EmployeeDTO empDTO = mapper.Map<EmployeeDTO>(emp);

            //  StringBuilder sb = new StringBuilder();
            //  sb.AppendLine($"");

            return $"ID: { empDTO.Id} - { empDTO.FirstName} { empDTO.LastName} -  ${ empDTO.Salary: f2}";
        }
    }
}