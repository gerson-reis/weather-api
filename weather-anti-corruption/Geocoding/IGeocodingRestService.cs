using weather_anti_corruption.Geocoding.ResultModels;

namespace weather_anti_corruption.Geocoding
{
    public interface IGeocodingRestService
    {
        Task<CoordinatesModel>? Get(string address);
    }
}
