namespace BillsPaymentSystem.Models
{
    using BillsPaymentSystem.Models.Attributes;
    using BillsPaymentSystem.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class PaymentMethod
    {
        public int Id { get; set; }
                
        public int? BankAccountId { get; set; }

        public int? CreditCardId { get; set; }

        [Required]
        public PaymentType Type { get; set; }
   
        public int UserId { get; set; }

        public virtual User User { get; set; }
        [Xor(nameof(BankAccount))]
        public virtual CreditCard CreditCard { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}