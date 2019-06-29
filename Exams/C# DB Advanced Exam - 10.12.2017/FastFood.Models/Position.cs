namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Position : BaseEntityIdentifiable
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }
        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}