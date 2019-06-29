namespace FastFood.Web.ViewModels.Items
{
    using System.ComponentModel.DataAnnotations;
    public class CreateItemInputModel
    {
        [Required]
        [MinLength(3), MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}