using Newtonsoft.Json;

namespace ProductShop.DTOS.UsersAndProducts
{
    public class product_dto
    {
        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }



    }
}