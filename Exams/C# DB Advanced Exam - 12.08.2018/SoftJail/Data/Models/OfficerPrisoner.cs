namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class OfficerPrisoner
    {
        public int PrisonerId { get; set; }
        [ForeignKey(nameof(PrisonerId))]
        public virtual Prisoner Prisoner { get; set; }

        public int OfficerId { get; set; }
        [ForeignKey(nameof(OfficerId))]
        public virtual Officer Officer { get; set; }
    }
}