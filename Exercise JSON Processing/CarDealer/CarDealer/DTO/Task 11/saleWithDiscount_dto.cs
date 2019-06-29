using Newtonsoft.Json;

namespace CarDealer.DTO.Task_11
{
    public  class saleWithDiscount_dto
    {
        [JsonProperty(PropertyName = "car")]
        public car_out_dto Car { get; set; }
        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; set; }

        [JsonIgnore]
        public decimal DecimalPrice { get; set; }
        [JsonIgnore]
        public decimal DecimalDiscount { get; set; }

        public string Discount => string.Format("{0:F2}", DecimalDiscount);

        [JsonProperty(PropertyName = "price")]
        public string Price => string.Format("{0:F2}", DecimalPrice);

        [JsonProperty(PropertyName = "priceWithDiscount")]
        public string PriceWithDiscount => string.Format("{0:F2}", DecimalPrice * (100 - DecimalDiscount) / 100m);
    }

    public class car_out_dto
    {
       public string Make { get; set; }
       public string Model { get; set; }
       public long TravelledDistance { get; set; }
    }
}