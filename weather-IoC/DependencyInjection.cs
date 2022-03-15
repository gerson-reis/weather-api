using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using weather_anti_corruption.Geocoding;
using weather_application.IServices;
using weather_application.Services;
using weather_infrastructure.CacheServices;

namespace weather_IoC
{
    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IGetWeatherStatusService, GetWeatherStatusService>();
            serviceCollection.AddScoped<ICacheService, CacheService>();

            AddHttpClientServices(serviceCollection, configuration);
        }

        private static void AddHttpClientServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHttpClient<IGeocodingRestService, GeocodingRestService>(client =>
            {
                client.BaseAddress = new Uri(configuration["GeocodingBaseUrl"]);
                client.DefaultRequestHeaders.Add("benchmark", "2020");
                client.DefaultRequestHeaders.Add("searchtype", "onelineaddress");
                client.DefaultRequestHeaders.Add("returntype", "locations");
                client.DefaultRequestHeaders.Add("format", "json");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(HttpRetryPolices.GetRetryPolicy());

            serviceCollection.AddHttpClient<INationalWeatherRestService, NationalWeatherRestService>(client =>
            {
                client.BaseAddress = new Uri(configuration["WeatherBaseUrl"]);
                client.DefaultRequestHeaders.Add("User-Agent", "(myweatherapp.com, contact@myweatherapp.com)");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(HttpRetryPolices.GetRetryPolicy());
        }
    }
}
