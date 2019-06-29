namespace VaporStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VaporStore.Data.Models.Enumerations;
    public class Purchase : ClassWithIdBase
    {
        [Required]
        public PurchaseType Type { get; set; }

        [Required,RegularExpression(@"^([A-Z\d]{4}-){2}[A-Z\d]{4}$")]
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        public int CardId { get; set; }
        [ForeignKey(nameof(CardId))]
        public virtual Card Card { get; set; }

        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; }

    }
}