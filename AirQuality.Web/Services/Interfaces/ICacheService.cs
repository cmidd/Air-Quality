using AirQuality.Web.Models;
using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Services.Interfaces
{
    public interface ICacheService
    {
        IList<CitiesRow> CitiesList { get; set; }
        IList<HistoryItem> SearchHistory { get; set; }
    }
}
