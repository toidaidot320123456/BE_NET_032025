using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CRMProject.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value);
            var bytes = Encoding.UTF8.GetBytes(json);

            var options = new DistributedCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }

            await _cache.SetAsync(key, bytes, options);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var bytes = await _cache.GetAsync(key);
            if (bytes == null) return default;

            var json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
        //
        public async Task SetStringAsync(string key, string value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }
            await _cache.SetStringAsync(key, value, options);
        }
        public async Task<string> GetStringAsync(string key)
        {
            var value = await _cache.GetStringAsync(key);
            if (value == null) return null;
            //
            return value;
        }
        public async Task RemoveStringAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
