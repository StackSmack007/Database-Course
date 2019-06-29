namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Mail : BaseEntityId
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required, RegularExpression(@"^[\sA-Za-z0-9]+str\.$")]
        public string Address { get; set; }

        public int PrisonerId { get; set; }
        [ForeignKey(nameof(PrisonerId))]
        public virtual Prisoner Prisoner { get; set; }
    }
}