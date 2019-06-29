using System;

namespace TestSoftUni.DTO_s
{
    public class EmployeeDTO
    {
        public EmployeeDTO(string firstName, string lastName, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
        }
        public EmployeeDTO() { }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime? Birthday { get; set; }
    }
}