using System.ComponentModel.DataAnnotations;

namespace BillsPaymentSystem.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        [Required]
        [Range(typeof(decimal),"0.0", "79228162514264337593543950335")]
        public decimal Balance { get; set; }
        [Required]
        [MaxLength(50)]
        public string BankName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SwiftCode { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}