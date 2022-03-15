using Microsoft.AspNetCore.Diagnostics;
using weather_api.StartUpSettings;
using weather_application.Dtos;
using weather_application.IServices;
using weather_infrastructure.Exceptions;
using weather_IoC;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "corsOrigins";

ConfigureCors(builder, MyAllowSpecificOrigins);

DependencyInjection.Configure(builder.Services, builder.Configuration);

AddProjectStructureBase(builder);
var app = builder.Build();

ConfigureLog(builder);
ConfigureSwagger(app);
app.ConfigureExceptionHandler();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/api/check", () => StatusCodes.Status200OK);

app.MapGet("/api/get-forecast-from", async Task<IList<ForecastDayDto>> (string address, IGetWeatherStatusService service) =>
{
    var forecast = await service.GetForecastByAddress(address);

    return forecast;
});

app.Run();

static void AddProjectStructureBase(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMemoryCache();
}

static void ConfigureLog(WebApplicationBuilder builder)
{
    builder.Host.ConfigureLogging(logging =>
    {
        logging.AddConsole();
    });
}

static void ConfigureSwagger(WebApplication app)
{
    app.UseSwaggerUI();
    app.UseSwagger(x => x.SerializeAsV2 = true);
}

static void ConfigureCors(WebApplicationBuilder builder, string MyAllowSpecificOrigins)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
    });
}