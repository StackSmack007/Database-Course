namespace SoftJail.Data.Models
{
    using SoftJail.Data.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Officer : BaseEntityId
    {
        public Officer()
        {
            OfficerPrisoners = new HashSet<OfficerPrisoner>();
        }

        [Required,MinLength(3),MaxLength(30)]
        public string FullName { get; set; }
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        [Required]
        public Position Position { get; set; }
        [Required]
        public Weapon Weapon { get; set; }

        public int  DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }

        public virtual ICollection<OfficerPrisoner> OfficerPrisoners  { get; set; }
    }
}