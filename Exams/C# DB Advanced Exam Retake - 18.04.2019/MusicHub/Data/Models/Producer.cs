namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Producer : BaseId
    {
        public Producer()
        {
            Albums = new HashSet<Album>();
        }

        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$")]//TOCHECK IsRequired?
        public string Pseudonym { get; set; }

        [RegularExpression(@"^\+359\s\d{3}\s\d{3}\s\d{3}$")]//TOCHECK IsRequired?
        public string PhoneNumber { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}