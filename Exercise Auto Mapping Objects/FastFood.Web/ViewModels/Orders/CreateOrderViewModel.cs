namespace FastFood.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderViewModel
    {
        [Required]
        public Dictionary<int,string> Items { get; set; }
        [Required]
        public Dictionary<int, string> Employees { get; set; }
    }
}
