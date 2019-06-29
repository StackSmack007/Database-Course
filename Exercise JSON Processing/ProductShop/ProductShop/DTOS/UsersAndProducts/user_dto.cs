using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProductShop.DTOS.UsersAndProducts
{
    public class user_dto
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "age")]
        public int? Age { get; set; }
        [JsonProperty(PropertyName = "soldProducts")]
        public sold_products_dto ProductsSold { get; set; }
        
    }
}