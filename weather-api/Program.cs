using weather_anti_corruption.Geocoding;
using weather_api;
using weather_core.IServices;
using weather_core.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGetWeatherStatusService, GetWeatherStatusService>();

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

builder.Services.AddHttpClient<INationalWeatherRestService, NationalWeatherRestService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WeatherBaseUrl"]);
    client.DefaultRequestHeaders.Add("User-Agent", "(dsaatherapp.com, contact@msdweatherapp.com)");
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5))
.AddPolicyHandler(HttpRetryPolices.GetRetryPolicy());

var app = builder.Build();

app.MapGet("/GetRoles", Task (IGeocodingRestService service, INationalWeatherRestService serviceWeather) =>
{
    var result = service.Get("4600 Silver Hill Rd, Washington, DC 20233").WaitAsync(TimeSpan.FromSeconds(5)).Result;

    var forecast = serviceWeather.Get(result.X, result.Y).Result;

    return Task.FromResult(result);
});

app.Run();
