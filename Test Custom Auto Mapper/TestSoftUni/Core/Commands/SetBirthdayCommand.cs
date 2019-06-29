namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using System;
    using System.Globalization;
    using System.Linq;
    using TestSoftUni.DTO_s;
    using TestSoftUni.Infrastructure.Data;
    using TestSoftUni.Infrastructure.Models.Models;
    public class SetBirthdayCommand : BaseCommand
    {
        public SetBirthdayCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            int Id = int.Parse(input[0]);
            DateTime birthDate = DateTime.ParseExact(input[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            Employee emp = context.Employees.FirstOrDefault(x => x.Id == Id);
            if (emp is null)
            {
                throw new ArgumentException("EmployeeId not found!");
            }
            emp.Birthday = birthDate;
           int rowsChanged= context.SaveChanges();

            if (rowsChanged == 1)
            {
                var empDTO = mapper.Map<EmployeeDTO>(emp);
                return $"Successfully added birthDate To employee!\nFirstName: {empDTO.FirstName}\nLastName: {empDTO.LastName}";
            }
            return "Misfortune Nothing was added!";
        }
    }
}