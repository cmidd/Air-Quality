using AirQuality.Web.Models.AppSettings;
using AirQuality.Web.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace AirQuality.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly CachingConfig _cachingConfig;
        private readonly ILogger<CacheService> _logger;
        private readonly IDistributedCache _cache;

        public CacheService(IOptions<CachingConfig> cachingConfig, 
            ILogger<CacheService> logger,
            IDistributedCache cache)
        {
            _cachingConfig = cachingConfig.Value;
            _logger = logger;
            _cache = cache;
        }

        public CachingConfig CacheConfig => _cachingConfig;

        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            try
            {
                var cachedData = await _cache.GetAsync(cacheKey);

                if (cachedData != null)
                {
                    // If the data is found in the cache, encode and deserialize cached data.
                    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                    var data = JsonSerializer.Deserialize<T>(cachedDataString);
                    return data;
                }

                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when getting cache data using key {cacheKey}: {ex.InnerException}", this);
                return default;
            }
        }

        public async Task<bool> SetAsync<T>(T data, string cacheKey, int expirationInSeconds = 0)
        {
            try
            {
                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(data);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                if (expirationInSeconds == 0)
                    expirationInSeconds = _cachingConfig.CacheKeyExpirationInSeconds;

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(expirationInSeconds));

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when setting cache data for key {cacheKey}: {ex.InnerException}", this);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string cacheKey)
        {
            try
            {
                await _cache.RemoveAsync(cacheKey);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when deleting cache data for key {cacheKey}: {ex.InnerException}", this);
                return false;
            }
        }
    }
}
