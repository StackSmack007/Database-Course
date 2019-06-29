namespace Cinema.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Hall : BaseId
    {
        public Hall()
        {
            Projections = new HashSet<Projection>();
            Seats = new HashSet<Seat>();
        }

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }
        public bool Is3D { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}