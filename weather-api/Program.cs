using weather_anti_corruption.Geocoding;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;
using weather_api;
using weather_application.IServices;
using weather_application.Services;
using weather_infrastructure.CacheServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGetWeatherStatusService, GetWeatherStatusService>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddHttpClient<IGeocodingRestService, GeocodingRestService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["GeocodingBaseUrl"]);
    client.DefaultRequestHeaders.Add("benchmark", "2020");
    client.DefaultRequestHeaders.Add("searchtype", "onelineaddress");
    client.DefaultRequestHeaders.Add("returntype", "locations");
    client.DefaultRequestHeaders.Add("format", "json");
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5))
.AddPolicyHandler(HttpRetryPolices.GetRetryPolicy());

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<INationalWeatherRestService, NationalWeatherRestService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WeatherBaseUrl"]);
    client.DefaultRequestHeaders.Add("User-Agent", "(myweatherapp.com, contact@myweatherapp.com)");
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5))
.AddPolicyHandler(HttpRetryPolices.GetRetryPolicy());

var app = builder.Build();

app.MapGet("/get-forecast-from", async Task<IList<Period>> (string address, IGetWeatherStatusService service) =>
{
    var forecast = await service.GetForecastByAddress(address);

    return forecast;
});

app.Run();
