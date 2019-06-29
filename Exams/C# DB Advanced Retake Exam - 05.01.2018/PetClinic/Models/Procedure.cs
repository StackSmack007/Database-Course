using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PetClinic.Models
{
    public class Procedure : BaseId
    {
        public Procedure()
        {
            ProcedureAnimalAids = new HashSet<ProcedureAnimalAid>();
        }

        public int AnimalId { get; set; }
        [ForeignKey(nameof(AnimalId))]
        public virtual Animal Animal { get; set; }

        public int VetId { get; set; }
        [ForeignKey(nameof(VetId))]
        public virtual Vet Vet { get; set; }

     public virtual   ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }

        [NotMapped]
        public decimal Cost => ProcedureAnimalAids.Select(x => x.AnimalAid.Price).Sum();

        public DateTime DateTime  { get; set; }


//	Id – integer, Primary Key
//	AnimalId ¬– integer, foreign key
//	Animal – the animal on which the procedure is performed(required)
//	VetId ¬– integer, foreign key
//	Vet – the clinic’s employed doctor servicing the patient(required)
//	ProcedureAnimalAids – collection of type ProcedureAnimalAid
//	Cost – the cost of the procedure, calculated by summing the price of the different services performed; does not need to be inserted in the database
//	DateTime – the date and time on which the given procedure is performed(required)


    }
}
