using Newtonsoft.Json;

namespace API_RestSharp_Project.Models.Response
{
    public class GeocodeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("local_names", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> LocalNames { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
