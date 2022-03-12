using weather_anti_corruption.Geocoding.ResultModels;

namespace weather_anti_corruption.Geocoding
{
    public interface INationalWeatherRestService
    {
        Task<CoordinatesModel>? Get(string latitude, string longitude);
    }
}
