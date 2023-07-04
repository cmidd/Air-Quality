using AirQuality.Web.Models;
using AirQuality.Web.Services.Interfaces;

namespace AirQuality.Web.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<HistoryService> _logger;

        public HistoryService(ICacheService cacheService, 
            ILogger<HistoryService> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public List<HistoryItem> GetHistory(string userId)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            var searchHistory = _cacheService.GetAsync<List<HistoryItem>>(cacheKey).Result;
            return searchHistory ?? new List<HistoryItem>();
        }

        public void AddToHistory(string userId, HistoryItem item)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            var searchHistory = _cacheService.GetAsync<List<HistoryItem>>(cacheKey).Result;
            if (searchHistory == null)
            {
                searchHistory = new List<HistoryItem>();
            }

            // Add the new item to the top of the list
            searchHistory.Insert(0, item);

            _logger.LogInformation($"Added location {item.Id} to search history for user {userId}");

            _cacheService.SetAsync(searchHistory, cacheKey);
        }

        public void ClearHistory(string userId)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            _cacheService.DeleteAsync(cacheKey);
        }
    }
}
