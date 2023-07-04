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

        public async Task<List<HistoryItem>> GetHistory(string userId)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            var searchHistory = await _cacheService.GetAsync<List<HistoryItem>>(cacheKey);
            return searchHistory ?? new List<HistoryItem>();
        }

        public async Task AddToHistory(string userId, HistoryItem item)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            var searchHistory = await _cacheService.GetAsync<List<HistoryItem>>(cacheKey);
            if (searchHistory == null)
            {
                searchHistory = new List<HistoryItem>();
            }

            // Add the new item to the top of the list
            searchHistory.Insert(0, item);

            _logger.LogInformation($"Added location {item.Id} to search history for user {userId}");

            await _cacheService.SetAsync(searchHistory, cacheKey);
        }

        public async Task ClearHistory(string userId)
        {
            var cacheKey = string.Format(_cacheService.CacheConfig.SearchHistoryKey, userId);
            await _cacheService.DeleteAsync(cacheKey);
        }
    }
}
