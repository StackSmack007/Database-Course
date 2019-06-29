namespace SoftJail.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Prisoner : BaseEntityId
    {
        public Prisoner()
        {
            PrisonerOfficers = new HashSet<OfficerPrisoner>();
            Mails = new HashSet<Mail>();
        }
        
        [Required, MinLength(3), MaxLength(20)]
        public string FullName { get; set; }

        [Required, RegularExpression(@"^The\s[A-Z][a-z]+$")]
        public string Nickname { get; set; }

        [Range(18, 65)]
        public int Age { get; set; }

        public DateTime IncarcerationDate { get; set; }
       
        public DateTime? ReleaseDate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        [ForeignKey(nameof(CellId))]
        public virtual Cell Cell { get; set; }

        public virtual ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
        public virtual ICollection<Mail> Mails { get; set; }
    }
}
