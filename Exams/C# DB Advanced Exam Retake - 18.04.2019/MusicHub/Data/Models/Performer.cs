namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Performer : BaseId
    {
        public Performer()
        {
            PerformerSongs = new HashSet<SongPerformer>();
        }
        [Required,MinLength(3),MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MinLength(3), MaxLength(20)]
        public string LastName { get; set; }
        [Range(18,70)]
        public int Age { get; set; }
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        public decimal NetWorth { get; set; }

        public virtual ICollection<SongPerformer> PerformerSongs  { get; set; }
    }
}//done