using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class EmployeesAllViewModel
    {
        [Required]
        [MinLength(3), MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [Range(16,65)]
        public int Age { get; set; }
        [Required]
        [MinLength(3), MaxLength(64)]
        public string Address { get; set; }
        [Required]
        [MinLength(3), MaxLength(16)]
        public string Position { get; set; }
    }
}
