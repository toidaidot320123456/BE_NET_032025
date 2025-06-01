using Microsoft.Extensions.Caching.Distributed;

namespace CRMProject.Cache
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        //
        Task SetStringAsync(string key, string value, TimeSpan? expiration = null);
        Task<string> GetStringAsync(string key);
        Task RemoveStringAsync(string key);
    }
}
