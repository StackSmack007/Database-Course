namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Item : BaseEntityIdentifiable
    {
        public Item()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Range(typeof(decimal),"0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public virtual ICollection<OrderItem> OrderItems  { get; set; }
    }
}