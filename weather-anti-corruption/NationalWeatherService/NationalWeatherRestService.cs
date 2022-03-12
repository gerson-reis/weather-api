using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using weather_anti_corruption.Geocoding.ResultModels;
using weather_anti_corruption.NationalWeatherService.ResultModels;

namespace weather_anti_corruption.Geocoding
{
    public class NationalWeatherRestService : INationalWeatherRestService
    {
        private readonly HttpClient _httpClient;

        public NationalWeatherRestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CoordinatesModel>? Get(string latitude, string longitude)
        {
            try
            {
                var properties = await GetPropertiesFromGeocode(latitude, longitude);

                _ = properties ?? throw new ArgumentNullException(nameof(properties));



            }
            catch (Exception e)
            {

                throw;
            }

        }

        private async Task<Properties>? GetForecastByGridPoints(string gridId, string gridX, string gridY)
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
