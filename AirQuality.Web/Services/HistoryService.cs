using AirQuality.Web.Models;
using AirQuality.Web.Services.Interfaces;

namespace AirQuality.Web.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ICacheService _cacheService;

        public HistoryService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IList<HistoryItem> GetHistory() => _cacheService.SearchHistory;

        public void AddToHistory(HistoryItem item)
        {
            var searchHistory = _cacheService.SearchHistory;

            // Add the new item to the top of the list
            searchHistory.Insert(0, item);

            _cacheService.SearchHistory = searchHistory;
        }

        public void ClearHistory() => _cacheService.SearchHistory = new List<HistoryItem>();
    }
}
