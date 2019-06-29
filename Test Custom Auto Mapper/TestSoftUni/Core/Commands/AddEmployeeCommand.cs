using AutoMapper;
using TestSoftUni.DTO_s;
using TestSoftUni.Infrastructure.Data;
using TestSoftUni.Infrastructure.Models.Models;

namespace TestSoftUni.Core.Commands
{

    public class AddEmployeeCommand : BaseCommand
    {
        public AddEmployeeCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            string firstName = input[0];
            string lastName = input[1];
            decimal salary = decimal.Parse(input[2]);
            int rowsChanged;
            Employee newEmp = new Employee() { FirstName = firstName, LastName = lastName, Salary = salary };

            context.Employees.Add(newEmp);
            rowsChanged = context.SaveChanges();

            if (rowsChanged == 1)
            {
                var empDTO = mapper.Map<EmployeeDTO>(newEmp);
                return $"Successfully added 1 employee!\nFirstName: {empDTO.FirstName}\nLastName: {empDTO.LastName}\nSalary: {empDTO.Salary:F2}";
            }
            return "Misfortune Nothing was added!";
        }
    }
}