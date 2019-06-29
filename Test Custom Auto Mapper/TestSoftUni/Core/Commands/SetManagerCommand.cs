namespace TestSoftUni.Core.Commands
{
    using AutoMapper;
    using System;
    using System.Linq;
    using TestSoftUni.Infrastructure.Data;
    public class SetManagerCommand : BaseCommand
    {
        public SetManagerCommand(TContext context, Mapper mapper) : base(context, mapper)
        { }

        public override string Execute(string[] input)
        {
            int employeeId = int.Parse(input[0]);
            int managerId = int.Parse(input[1]);

            var employee = context.Employees.FirstOrDefault(x => x.Id == employeeId);
            var manager = context.Employees.FirstOrDefault(x => x.Id == managerId);
            if (employee is null)
            {
                throw new ArgumentException($"Employee with Id={employeeId} Not found!");
            }
            if (manager is null)
            {
                throw new ArgumentException($"Manager with Id={managerId} Not found!");
            }
            employee.Manager = manager;
            // manager.Subordinates.Add(employee);
            context.SaveChanges();
            return $"The employee {employee.FirstName + " " + employee.LastName} has a manager {manager.FirstName + " " + manager.LastName}";
        }
    }
}