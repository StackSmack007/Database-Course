namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Album : BaseId
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        [Required, MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        [NotMapped]
        public decimal Price => Songs.Sum(x => x.Price);

        public int? ProducerId { get; set; } //TOCHECK if producer is required

        [ForeignKey(nameof(ProducerId))]
        public virtual Producer Producer { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
     }
}//done