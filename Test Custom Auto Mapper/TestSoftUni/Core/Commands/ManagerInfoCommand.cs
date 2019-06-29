namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;
    using TestSoftUni.DTO_s;
    using TestSoftUni.Infrastructure.Data;
    public class ManagerInfoCommand : BaseCommand
    {
        public ManagerInfoCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            int managerId = int.Parse(input[0]);

            var manager = context.Employees.Include(e=>e.Subordinates).FirstOrDefault(x => x.Id == managerId);
            
            if (manager is null)
            {
                throw new ArgumentException($"Manager with Id={managerId} Not found!");
            }
            ManagerDTO manDTO = mapper.Map<ManagerDTO>(manager);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{manDTO.FirstName} {manDTO.LastName} | Employees: {manDTO.SubordinatesCount}");
            foreach (var emp in manDTO.Subordinates)
            {
                sb.AppendLine($"    - {emp.FirstName} {emp.LastName} - ${emp.Salary}");
            }

            return sb.ToString().Trim();
        }
    }
}