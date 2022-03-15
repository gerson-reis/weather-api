using weather_application.Dtos;

namespace weather_application.IServices
{
    public interface IGetWeatherStatusService
    {
        Task<IList<ForecastDayDto>>? GetForecastByAddress(string address);
    }
}
