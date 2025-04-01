using API_RestSharp_Project.Client;
using API_RestSharp_Project.Helpers;
using API_RestSharp_Project.Models.Response;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using RestSharp;
using Xunit.Abstractions;

namespace API_RestSharp_Project
{
    public class ApiTests: TestBase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private RestResponse _currentWeatherResponse;
        private RestResponse _geocodeResponse;
        private double _lat;
        private double _lon;

        public ApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [Trait("Category", "Api")]
        [InlineData("Belgrade", "Serbia", "RS", "Sremčica", "RS")]
        [InlineData("Paris", "France", "FR", "Palais-Royal", "FR")]
        [InlineData("London", "United Kingdom", "GB", "Chelsea", "GB")]
        [InlineData("Tokyo", "Japan", "JP", "Kokubunji", "JP")]
        public void VerifyCityCurrentWeatherResponse(string city, string state, string countryCode, string expectedName, string expectedCountry)
        {
            _geocodeResponse = GeocodeRestClient.GetGeocodeRestRequest(city, state, ApiKey);
            _lat = DataHelper.GetLatValueFromResponse(_geocodeResponse, countryCode);
            _lon = DataHelper.GetLonValueFromResponse(_geocodeResponse, countryCode);
            _currentWeatherResponse = CurrentWeatherRestClient.GetCurrentWeatherRestRequest(_lat, _lon, ApiKey);

            var currentWeather = JsonConvert.DeserializeObject<CurrentWeatherModel>(_currentWeatherResponse.Content.ToString());
            _testOutputHelper.WriteLine(_currentWeatherResponse.Content.ToString());
            Assert.Equal(200, (int)_currentWeatherResponse.StatusCode);
            Assert.Equal(expectedName, currentWeather.Name);
            Assert.Equal(expectedCountry, currentWeather.Sys.Country);
        }

        [Theory]
        [Trait("Category", "Api")]
        [InlineData("Valjevo", "Serbia", "RS", "sr", "Ваљево", "RS")]
        public void VerifyMultilingualSupportCurrentWeatherResponse(string city, string state, string countryCode, string lang,
            string expectedName, string expectedCountry)
        {
            _geocodeResponse = GeocodeRestClient.GetGeocodeRestRequest(city, state, ApiKey);
            _lat = DataHelper.GetLatValueFromResponse(_geocodeResponse, countryCode);
            _lon = DataHelper.GetLonValueFromResponse(_geocodeResponse, countryCode);
            _currentWeatherResponse = CurrentWeatherRestClient.GetMultilingualSupportCurrentWeatherRestRequest(_lat, _lon, ApiKey, lang);
            var currentWeather = JsonConvert.DeserializeObject<CurrentWeatherModel>(_currentWeatherResponse.Content.ToString());
            _testOutputHelper.WriteLine(_currentWeatherResponse.Content.ToString());
            Assert.Equal(200, (int)_currentWeatherResponse.StatusCode);
            Assert.Equal(expectedName, currentWeather.Name);
            Assert.Equal(expectedCountry, currentWeather.Sys.Country);
        }
    }
}
