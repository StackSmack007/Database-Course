namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Writer : BaseId
    {
        public Writer()
        {
            Songs = new HashSet<Song>();
        }

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$")]//TOCHECK IsRequired?
        public string Pseudonym { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}//done