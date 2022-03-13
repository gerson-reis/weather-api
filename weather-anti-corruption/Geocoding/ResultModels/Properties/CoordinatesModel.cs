using Newtonsoft.Json;

namespace weather_anti_corruption.Geocoding.ResultModels
{
    public class CoordinatesModel
    {
        [JsonProperty("y")]
        public string Latitude { get; set; }
        [JsonProperty("x")]
        public string Longitude { get; set; }
    }
}