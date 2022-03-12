using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using weather_anti_corruption.Geocoding.ResultModels;
using weather_anti_corruption.Geocoding.ResultModels.Properties;
using weather_anti_corruption.NationalWeatherService.ResultModels;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;

namespace weather_anti_corruption.Geocoding
{
    public class NationalWeatherRestService : INationalWeatherRestService
    {
        private readonly HttpClient _httpClient;

        public NationalWeatherRestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Period>>? Get(string latitude, string longitude)
        {
            try
            {
                var properties = await GetPropertiesFromGeocode(latitude, longitude);

                //TODO: Geolocation not found
                _ = properties ?? throw new ArgumentNullException(nameof(properties));

                var forecast = await GetForecastByGridPoints(properties.GridId, properties.GridX, properties.GridY);

                //TODO: Exception forecas not found
                _ = forecast ?? throw new ArgumentNullException(nameof(forecast));

                return forecast.Properties.Periods;

            }
            catch (Exception e)
            {

                throw;
            }

        }

        private async Task<ForecastResult>? GetForecastByGridPoints(string gridId, int gridX, int gridY)
        {
            var response = await _httpClient.GetAsync($"gridpoints/{gridId}/{gridX},{gridY}/forecast");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var requestResult = JsonConvert.DeserializeObject<ForecastResult>(jsonString.Result);

                return requestResult;
            }
            return null;
        }


        private async Task<Properties>? GetPropertiesFromGeocode(string latitude, string longitude)
        {
            var parameters = HttpUtility.UrlEncode($"{longitude},{latitude}", Encoding.ASCII);
            var response = await _httpClient.GetAsync($"points/{parameters}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var requestResult = JsonConvert.DeserializeObject<GetGridResult>(jsonString.Result);

                return requestResult.Properties;
            }
            return null;
        }
    }
}
