using AirQuality.Web.Models;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IHistoryService
    {
        Task AddToHistory(string userId, HistoryItem item);
        Task<List<HistoryItem>> GetHistory(string userId);
        Task ClearHistory(string userId);
    }
}
