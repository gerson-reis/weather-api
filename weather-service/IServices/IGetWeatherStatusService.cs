using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;

namespace weather_application.IServices
{
    public interface IGetWeatherStatusService
    {
        Task<IList<Period>>? GetForecastByAddress(string address);
    }
}
