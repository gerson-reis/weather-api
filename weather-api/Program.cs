using Microsoft.AspNetCore.Diagnostics;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;
using weather_application.IServices;
using weather_infrastructure.Exceptions;
using weather_IoC;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

DependencyInjection.Configure(builder.Services, builder.Configuration);

AddProjectStructureBase(builder);

var app = builder.Build();

ConfigureLog(builder);
ConfigureSwagger(app);
ConfigureExceptionHandler(app);

app.MapGet("/api/check", () => StatusCodes.Status200OK);

app.MapGet("/api/get-forecast-from", async Task<IList<Period>> (string address, IGetWeatherStatusService service) =>
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

static void ConfigureExceptionHandler(WebApplication app)
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        var logger = app.Services.GetRequiredService(typeof(ILogger<Program>)) as ILogger<Program>;

        exceptionHandlerApp.Run(async context =>
        {
            context.Response.ContentType = Text.Plain;

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ClientExceptionBase)
            {
                var exception = exceptionHandlerPathFeature.Error as ClientExceptionBase;
                await context.Response.WriteAsync(exception.Message);
                context.Response.StatusCode = exception.HttpStatusCode;
                logger.LogInformation(exception.Message);
            }
            else if (exceptionHandlerPathFeature?.Error is InternalExceptionBase)
            {
                var exception = exceptionHandlerPathFeature.Error as InternalExceptionBase;
                logger.LogCritical(exception.EventId, exception, $"Endpoint: {exceptionHandlerPathFeature.Path}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else
            {
                logger.LogCritical(exceptionHandlerPathFeature.Error.Message);
            }
        });
    });
}