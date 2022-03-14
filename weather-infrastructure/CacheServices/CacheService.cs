using Microsoft.Extensions.Caching.Memory;

namespace weather_infrastructure.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task Set<T>(string key, object value, DateTime expirationDate) => memoryCache.Set(key, value, expirationDate);
        public async Task<T> Get<T>(string key) => memoryCache.Get<T>(key);

    }
}
