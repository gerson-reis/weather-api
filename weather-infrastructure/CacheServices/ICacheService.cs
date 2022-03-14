namespace weather_infrastructure.CacheServices
{
    public interface ICacheService
    {
        Task Set<T>(string key, object value, DateTime expirationDate);
        Task<T> Get<T>(string key);
    }
}
