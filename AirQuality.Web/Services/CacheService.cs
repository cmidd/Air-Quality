using AirQuality.Web.Models;
using AirQuality.Web.Models.AppSettings;
using AirQuality.Web.Models.OpenAq;
using AirQuality.Web.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AirQuality.Web.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CachingConfig _cachingConfig;
        private readonly MemoryCacheEntryOptions _citiesListCacheOptions;
        private readonly MemoryCacheEntryOptions _searchHistoryCacheOptions;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache,
            IOptions<CachingConfig> cachingConfig, 
            ILogger<CacheService> logger)
        {
            _memoryCache = cache;
            _cachingConfig = cachingConfig.Value;
            _citiesListCacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cachingConfig.CitiesListExpiration));
            _searchHistoryCacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cachingConfig.SearchHistoryExpiration));
            _logger = logger;
        }

        public IList<CitiesRow> CitiesList
        {
            get
            {
                if (_memoryCache.TryGetValue(_cachingConfig.CitiesListKey, out IList<CitiesRow> cacheValue))
                {
                    _logger.LogInformation("Retrieving cities from cache.");
                }
                else
                {
                    _logger.LogWarning("No cities found in cache.");
                }

                return cacheValue ?? new List<CitiesRow>();
            }
            set
            {
                if (value == null)
                    return;

                _memoryCache.Set(_cachingConfig.CitiesListKey,
                    value,
                    _citiesListCacheOptions);

                _logger.LogInformation("Added list of cities to cache.");
            }
        }

        public IList<HistoryItem> SearchHistory
        {
            get
            {
                if (_memoryCache.TryGetValue(_cachingConfig.SearchHistoryKey, out IList<HistoryItem> cacheValue))
                {
                    _logger.LogInformation("Retrieving search history from cache.");
                }
                else
                {
                    _logger.LogWarning("No search history found in cache.");
                }

                return cacheValue ?? new List<HistoryItem>();
            }
            set
            {
                if (value == null)
                    return;

                _memoryCache.Set(_cachingConfig.SearchHistoryKey,
                    value,
                    _searchHistoryCacheOptions);

                _logger.LogInformation("Added search history to cache.");
            }
        }
    }
}
