namespace Cinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Projection : BaseId
    {
        public Projection()
        {
            Tickets =new HashSet<Ticket>();
            }

        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; }

        public int HallId { get; set; }
        [ForeignKey(nameof(HallId))]
        public virtual Hall Hall { get; set; }

        public DateTime DateTime { get; set; }

        public virtual ICollection<Ticket> Tickets  {get;set;}
   
    }
}