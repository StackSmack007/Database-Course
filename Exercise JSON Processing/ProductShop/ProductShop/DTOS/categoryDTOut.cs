using Newtonsoft.Json;

namespace ProductShop.DTOS
{
    public class categoryDTOut
    {
        [JsonProperty(PropertyName = "category")]
        public string Name { get; set; }
        public int productsCount { get; set; }
    
        public string averagePrice { get; set; }
        public string totalRevenue { get; set; }
    }
}
