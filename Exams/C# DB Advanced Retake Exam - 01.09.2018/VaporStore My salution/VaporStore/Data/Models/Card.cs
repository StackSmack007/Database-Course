namespace VaporStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VaporStore.Data.Models.Enumerations;

    public class Card : ClassWithIdBase
    {
        public Card()
        {
            Purchases = new HashSet<Purchase>();
        }
        [Required, RegularExpression(@"^(\d{4}\s){3}\d{4}$")]
        public string Number { get; set; }
        [Required, RegularExpression(@"(\d{3}$")]
        public string Cvc { get; set; }
        [Required]
        public CardType Type { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}