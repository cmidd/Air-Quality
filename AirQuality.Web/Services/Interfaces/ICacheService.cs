using AirQuality.Web.Models.OpenAq.Responses;
using Microsoft.Extensions.Caching.Memory;

namespace AirQuality.Web.Services.Interfaces
{
    public interface ICacheService
    {
        IList<CitiesRow> CitiesList { get; set; }
        IList<CitiesRow> CitiesSearchHistory { get; set; }
    }
}
