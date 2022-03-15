using Microsoft.AspNetCore.Diagnostics;
using weather_infrastructure.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace weather_api.StartUpSettings
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
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
    }
}
