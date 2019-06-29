namespace CarDealer.DTO
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class car_in_dto
    {
        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "travelledDistance")]
        public long TravelledDistance { get; set; }

        [JsonProperty(PropertyName = "partsId")]
        public virtual ICollection<int> PartIds { get; set; } = new List<int>();
    }
}