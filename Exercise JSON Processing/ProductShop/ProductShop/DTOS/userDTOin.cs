namespace ProductShop.DTOS
{
using System.ComponentModel.DataAnnotations;
    public class userDTOin
    {
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        public int? Age { get; set; }


    }
}
