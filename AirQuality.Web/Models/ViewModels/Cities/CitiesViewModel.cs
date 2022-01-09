using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Models.ViewModels.Cities
{
    public class CitiesViewModel
    {
        public IList<CitiesRow> Cities { get; set; } = new List<CitiesRow>();
    }
}
