namespace FastFood.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class OrderItem
    {
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order {get;set;}

        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public virtual Item Item { get; set; }

        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
}