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

        public CacheService(IMemoryCache cache,
            IOptions<CachingConfig> cachingConfig)
        {
            _memoryCache = cache;
            _cachingConfig = cachingConfig.Value;
            _citiesListCacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cachingConfig.CitiesListExpiration));
            _searchHistoryCacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cachingConfig.SearchHistoryExpiration));
        }

        public IList<CitiesRow> CitiesList
        {
            get
            {
                _memoryCache.TryGetValue(_cachingConfig.CitiesListKey, out IList<CitiesRow> cacheValue);
                return cacheValue ?? new List<CitiesRow>();
            }
            set
            {
                if (value == null)
                    return;

                _memoryCache.Set(_cachingConfig.CitiesListKey,
                    value,
                    _citiesListCacheOptions);
            }
        }

        public IList<HistoryItem> SearchHistory
        {
            get
            {
                _memoryCache.TryGetValue(_cachingConfig.SearchHistoryKey, out IList<HistoryItem> cacheValue);
                return cacheValue ?? new List<HistoryItem>();
            }
            set
            {
                if (value == null)
                    return;

                _memoryCache.Set(_cachingConfig.SearchHistoryKey,
                    value,
                    _searchHistoryCacheOptions);
            }
        }
    }
}
