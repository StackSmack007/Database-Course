namespace VaporStore.DTOS
{
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    public   class jsonUser_inp_dto
    {

        [Required, MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [Required, RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        public virtual ICollection<jsonCard_inp_dto> Cards { get; set; }

    }

    public class jsonCard_inp_dto
    {
        [Required, RegularExpression(@"^(\d{4}\s){3}\d{4}$")]
        public string Number { get; set; }
        [Required, RegularExpression(@"(\d{3}$")]
        public string Cvc { get; set; }
        [Required,RegularExpression(@"(^Debit$)|(^Credit$)")]
        public string Type { get; set; } 
    }
}
