using weather_anti_corruption.Geocoding.ResultModels;
using weather_anti_corruption.NationalWeatherService.ResultModels.Forecast;

namespace weather_anti_corruption.Geocoding
{
    public interface INationalWeatherRestService
    {
        Task<IList<Period>>? Get(string latitude, string longitude);
    }
}
