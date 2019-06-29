namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Ticket : BaseId
    {
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        public int ProjectionId { get; set; }
        [ForeignKey(nameof(ProjectionId))]
        public virtual Projection Projection { get; set; }
    }
}