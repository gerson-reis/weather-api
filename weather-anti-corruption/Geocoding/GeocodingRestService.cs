
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using weather_anti_corruption.Geocoding.ResultModels;

namespace weather_anti_corruption.Geocoding
{
    public class GeocodingRestService : IGeocodingRestService
    {
        private readonly HttpClient _httpClient;
        public GeocodingRestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private string serviceAddress = string.Empty;
        private string ServiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(serviceAddress))
                {
                    IDictionary<string, string> defaultParameters = new Dictionary<string, string>()
                    {
                        { "benchmark", "2020" },
                        { "searchtype", "address" },
                        { "returntype", "locations" },
                        { "format", "json" },
                    };

                    serviceAddress = QueryHelpers.AddQueryString("/geocoder/locations/onelineaddress?", defaultParameters);
                }

                return serviceAddress;
            }
        }

        public async Task<CoordinatesModel>? Get(string address)
        {
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(ServiceAddress, "address", address));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var requestResult = JsonConvert.DeserializeObject<ResponseModel>(jsonString.Result);

                return requestResult.Result.AddressMatches.First().Coordinates;
            }
            return null;
        }
    }
}
