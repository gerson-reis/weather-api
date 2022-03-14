using Newtonsoft.Json;
using System.Net;
using weather_anti_corruption.Geocoding.ResultModels.Properties;
using weather_anti_corruption.NationalWeatherService.ResultModels;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;
using weather_infrastructure.Exceptions;

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
            var properties = await GetPropertiesFromGeocode(latitude, longitude);

            _ = properties ?? throw new PropertiesNotFoundException();

            var forecast = await GetForecastByGridPoints(properties.GridId, properties.GridX, properties.GridY);

            _ = forecast ?? throw new PeriodNotFoundException();

            return forecast.Properties.Periods;
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
            var response = await _httpClient.GetAsync($"points/{latitude},{longitude}");

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
