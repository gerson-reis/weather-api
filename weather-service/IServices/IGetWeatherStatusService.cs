using weather_models;

namespace weather_application.IServices
{
    public interface IGetWeatherStatusService
    {
        Task<IList<ForecastDay>>? GetForecastByAddress(string address);
    }
}
