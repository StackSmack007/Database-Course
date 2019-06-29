using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        public int ProcedureId { get; set; }
        [ForeignKey(nameof(ProcedureId))]
        public virtual Procedure Procedure { get; set; }

        public int AnimalAidId { get; set; }
        [ForeignKey(nameof(AnimalAidId))]
        public virtual AnimalAid AnimalAid { get; set; }
        //TODO FLUENT PK
    }
}