using Newtonsoft.Json;

namespace Cinema.DataProcessor.ImportDto
{
    public  class hallJsonDto
    {
        public string Name { get; set; }

        public bool Is4Dx { get; set; }
        public bool Is3D { get; set; }

        [JsonProperty(PropertyName ="Seats")]
        public int SeatCount { get; set; }
    }
}