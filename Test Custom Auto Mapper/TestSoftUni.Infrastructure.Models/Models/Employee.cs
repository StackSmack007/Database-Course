namespace TestSoftUni.Infrastructure.Models.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue)]
        public decimal Salary { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

    }
}