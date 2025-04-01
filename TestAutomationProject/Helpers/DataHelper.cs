using API_RestSharp_Project.Models.Response;
using Newtonsoft.Json;
using RestSharp;

namespace API_RestSharp_Project.Helpers
{
    public class DataHelper
    {
        public static double GetLatValueFromResponse(RestResponse response, string countryCode)
        {
            var jsonResponse = response.Content.ToString();
            var locations = JsonConvert.DeserializeObject<List<GeocodeModel>>(jsonResponse);
            double lat = 0;
            foreach (var location in locations)
            {
                if (location.Country.Equals(countryCode))
                    lat = location.Lat;
            }
            return lat;

        }

        public static double GetLonValueFromResponse(RestResponse response, string countryCode)
        {
            var jsonResponse = response.Content.ToString();
            var locations = JsonConvert.DeserializeObject<List<GeocodeModel>>(jsonResponse);
            double lon = 0;
            foreach (var location in locations)
            {
                if (location.Country.Equals(countryCode))
                lon = location.Lon;
            }
            return lon;
        }
    }
}
