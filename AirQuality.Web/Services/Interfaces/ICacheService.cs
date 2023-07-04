using AirQuality.Web.Models.AppSettings;

namespace AirQuality.Web.Services.Interfaces
{
    public interface ICacheService
    {
        CachingConfig CacheConfig { get; }
        Task<bool> SetAsync<T>(T data, string cacheKey, int expirationInSeconds = 0);
        Task<T?> GetAsync<T>(string cacheKey);
        Task<bool> DeleteAsync(string cacheKey);
    }
}
