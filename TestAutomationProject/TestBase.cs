using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RestSharp;

namespace API_RestSharp_Project
{
    public class TestBase
    {
        protected IConfiguration Configuration;
        protected RestClient CurrentWeatherRestClient;
        protected RestClient GeocodeRestClient;
        public string ApiKey => Configuration["API_KEY"];

        public TestBase()
        {
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();

            CurrentWeatherRestClient = new RestClient(Configuration["CURRENT_WEATHER_API_URL"]);
            GeocodeRestClient = new RestClient(Configuration["GEOCODE_API_URL"]);
        }
    }
}
