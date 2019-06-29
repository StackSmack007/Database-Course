namespace TestSoftUni.DTO_s
{
    using System.Collections.Generic;
    using TestSoftUni.Infrastructure.Models.Models;
    public class ManagerDTO
    {
       //first name, last name, list of EmployeeDtos that he/she is in charge of and their count
       //Add the following commands to your console application:
       public int Id { get; set; }
   
        public string FirstName { get; set; }
   
        public string LastName { get; set; }
      
        public ICollection<EmployeeDTO> Subordinates { get; set; } = new List<EmployeeDTO>();
        public int SubordinatesCount => Subordinates.Count;
    }
}