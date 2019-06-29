namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public Supplier()
        {
            Parts = new List<Part>();
        }
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> Parts { get; set; }
    }
}
