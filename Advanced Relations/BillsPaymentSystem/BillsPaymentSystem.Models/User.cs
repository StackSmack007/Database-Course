namespace BillsPaymentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            PaymentMethods = new List<PaymentMethod>();
        }
        public int UserId { get; set; }
        [Required]
        [MaxLength(80)]

        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}