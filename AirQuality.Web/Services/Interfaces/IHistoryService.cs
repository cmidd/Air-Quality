using AirQuality.Web.Models;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IHistoryService
    {
        void AddToHistory(string userId, HistoryItem item);
        List<HistoryItem> GetHistory(string userId);
        void ClearHistory(string userId);
    }
}
