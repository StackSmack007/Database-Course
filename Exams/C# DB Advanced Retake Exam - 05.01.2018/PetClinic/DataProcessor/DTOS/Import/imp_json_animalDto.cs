namespace PetClinic.DataProcessor.DTOS.Import
{
    using System.ComponentModel.DataAnnotations;
    public class imp_json_animalDto
    {
        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public imp_json_passportDto Passport { get; set; }
    }
}