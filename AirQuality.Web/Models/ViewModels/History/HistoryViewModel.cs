namespace AirQuality.Web.Models.ViewModels.History
{
    public class HistoryViewModel
    {
        public IList<HistoryItem> HistoryItems { get; set; } = new List<HistoryItem>();
    }
}
