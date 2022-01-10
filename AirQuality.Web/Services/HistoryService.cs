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

        public IList<HistoryItem> GetHistory() => _cacheService.SearchHistory;

        public void AddToHistory(HistoryItem item)
        {
            var searchHistory = _cacheService.SearchHistory;

            // Add the new item to the top of the list
            searchHistory.Insert(0, item);

            _logger.LogInformation($"Added location {item.Id} to search history");

            _cacheService.SearchHistory = searchHistory;
        }

        public void ClearHistory() => _cacheService.SearchHistory = new List<HistoryItem>();
    }
}
