using FastFood.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Orders
{
    public class CreateOrderInputModel
    {
        [Required]
        [MinLength(4),MaxLength(32)]
        public string Customer { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int ItemId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int EmployeeId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public OrderType OrderType { get; set; }
    }
}
