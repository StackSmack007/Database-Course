namespace VaporStore.Data.Models
{
        using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Genre : ClassWithIdBase
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}