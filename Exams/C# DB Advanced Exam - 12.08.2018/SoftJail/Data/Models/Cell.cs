namespace SoftJail.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Cell : BaseEntityId
    {
        public Cell()
        {
            Prisoners = new HashSet<Prisoner>();
        }

        [Range(1, 1000)]
        public int CellNumber { get; set; }

        public bool HasWindow { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public virtual ICollection<Prisoner> Prisoners { get; set; }
    }
}