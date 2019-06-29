namespace FastFood.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public class Order : BaseEntityIdentifiable
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Required]
        public string Customer { get; set; }

        public DateTime DateTime { get; set; }

        public OrderType Type { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                return OrderItems.Select(x => x.Quantity * x.Item.Price).Sum();
            }
        }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}