using AirQuality.Web.Models;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IHistoryService
    {
        void AddToHistory(HistoryItem item);
        IList<HistoryItem> GetHistory();
        void ClearHistory();
    }
}
