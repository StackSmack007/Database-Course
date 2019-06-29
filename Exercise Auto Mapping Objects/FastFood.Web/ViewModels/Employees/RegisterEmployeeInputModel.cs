using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Employees
{
    public class RegisterEmployeeInputModel
    {
        [Required]
        [MinLength(3), MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [Range(16, 65)]
        public int Age { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PositionId { get; set; }
        //[Required]
        //[MinLength(3), MaxLength(16)]
        //public string PositionName { get; set; }
        [Required]
        [MinLength(3), MaxLength(64)]
        public string Address { get; set; }
    }
}
