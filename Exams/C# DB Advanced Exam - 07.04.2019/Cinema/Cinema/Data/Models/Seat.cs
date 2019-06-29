namespace Cinema.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Seat : BaseId
    {
        //done
        public int HallId { get; set; }
        [ForeignKey(nameof(HallId))]
        public virtual Hall Hall { get; set; }
    }
}