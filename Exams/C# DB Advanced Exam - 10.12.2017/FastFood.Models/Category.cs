namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : BaseEntityIdentifiable
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }
        [Required,MinLength(3),MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}