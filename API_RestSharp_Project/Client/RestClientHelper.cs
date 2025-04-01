using RestSharp;
using RestSharp.Serializers.Xml;

namespace API_RestSharp_Project.Client
{
    public static class RestClientHelper
    {
        public static RestResponse GetCurrentWeatherRestRequest(this RestClient restClient, double lat, double lon, string apiKey)
        {
            var restRequest = new RestRequest(restClient.Options.BaseUrl, Method.Get);

            restRequest.AddQueryParameter("lat", lat);
            restRequest.AddQueryParameter("lon", lon);
            restRequest.AddQueryParameter("appid", apiKey);
            restRequest.AddQueryParameter("units", "metric");

            return restClient.Execute(restRequest);
        }

        public static RestResponse GetMultilingualSupportCurrentWeatherRestRequest(this RestClient restClient, 
            double lat, double lon, string apiKey, string lang)
        {
            var restRequest = new RestRequest(restClient.Options.BaseUrl, Method.Get);
            restRequest.AddQueryParameter("lat", lat);
            restRequest.AddQueryParameter("lon", lon);
            restRequest.AddQueryParameter("appid", apiKey);
            restRequest.AddQueryParameter("units", "metric");
            restRequest.AddQueryParameter("lang", lang);
            return restClient.Execute(restRequest);
        }

        public static RestResponse GetGeocodeRestRequest(this RestClient restClient, string city, string state, string apiKey)
        {
            var restRequest = new RestRequest(restClient.Options.BaseUrl, Method.Get);
            restRequest.AddQueryParameter("q", city + "," + state);
            restRequest.AddQueryParameter("limit", "5");
            restRequest.AddQueryParameter("appid", apiKey);
            return restClient.Execute(restRequest);
        }
    }
}
