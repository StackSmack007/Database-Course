namespace SoftJail.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Department : BaseEntityId
    {
        public Department()
        {
            Cells = new HashSet<Cell>();
            Officers = new HashSet<Officer>();
        }

        [Required, MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        public virtual ICollection<Cell> Cells { get; set; }
        public virtual ICollection<Officer> Officers { get; set; }
    }
}