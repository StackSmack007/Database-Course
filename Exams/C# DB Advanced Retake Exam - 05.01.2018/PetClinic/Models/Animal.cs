using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal : BaseId
    {

        public Animal()
        {
            Procedures = new HashSet<Procedure>();
        }

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public string PassportSerialNumber { get; set; }

        [ForeignKey(nameof(PassportSerialNumber))]
        public virtual Passport Passport { get; set; }


        public virtual ICollection<Procedure> Procedures { get; set; }
    

        //	Id – integer, Primary Key
        //	Name – text with min length 3 and max length 20 (required)
        //	Type – text with min length 3 and max length 20 (required)
        //	Age – integer, cannot be negative or 0 (required)
        //	PassportSerialNumber ¬– string, foreign key
        //	Passport – the passport of the animal(required)
        //	Procedures – the procedures, performed on the animal


    }
}