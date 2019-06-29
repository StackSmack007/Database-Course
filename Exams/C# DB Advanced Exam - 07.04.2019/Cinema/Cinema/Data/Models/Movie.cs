namespace Cinema.Data.Models
{
    using Cinema.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    //10:30 -11:30 _12:45 1/2 of t2
    public class Movie : BaseId
    {

        [Required, MinLength(3), MaxLength(20)]
        public string Title { get; set; }

        public Genre Genre { get; set; }

        public TimeSpan Duration { get; set; }

        [Range(1, 10)]
        public double Rating { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Director { get; set; }

        public virtual ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();
    }
}