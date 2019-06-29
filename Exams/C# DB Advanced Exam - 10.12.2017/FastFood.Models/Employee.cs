namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee: BaseEntityIdentifiable
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Range(15,80)]
        public int Age { get; set; }

        public int PositionId { get; set; }
        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}