namespace VaporStore.DTOS
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class jsonGame_inp_dto
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        [JsonProperty(PropertyName = "Developer")]
        public string DeveloperName { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [JsonProperty(PropertyName = "Tags")]
        public ICollection<string> TagNames { get; set; } = new List<string>();


    }
}

//{
	//	"Name": "Invalid",
	//	"Price": -5,
	//	"ReleaseDate": "2013-07-09",
	//	"Developer": "Valid Dev",
	//	"Genre": "Valid Genre",
	//	"Tags": [
	//		"Valid Tag"
	//	] 