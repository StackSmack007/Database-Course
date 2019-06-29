namespace VaporStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game:ClassWithIdBase
    {
        public Game()
        {
            Purchases = new HashSet<Purchase>();
            GameTags = new HashSet<GameTag>();
        }

        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal),"0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }
        [ForeignKey(nameof(DeveloperId))]
        public virtual Developer Developer { get; set; }

        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Purchase> Purchases {get;set;}
        public virtual ICollection<GameTag> GameTags {get;set; }


    }
}
