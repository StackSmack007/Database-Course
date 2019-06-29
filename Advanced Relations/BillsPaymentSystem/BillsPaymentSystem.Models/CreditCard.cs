namespace BillsPaymentSystem.Models
{
    using BillsPaymentSystem.Models.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        public int CreditCardId { get; set; }

        [ExpirationCHK]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Limit { get; set; }

        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal MoneyOwed { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public decimal LimitLeft => Limit - MoneyOwed;
    }
}