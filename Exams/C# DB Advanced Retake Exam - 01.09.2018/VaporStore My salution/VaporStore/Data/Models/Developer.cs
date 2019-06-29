namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Developer : ClassWithIdBase
    {
        public Developer()
        {
            Games = new HashSet<Game>();
        }

        [Required,MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}