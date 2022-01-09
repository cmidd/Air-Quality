using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Models.ViewModels.Cities
{
    public class DataViewModel
    {
        public string City { get; set; } = string.Empty;

        public IList<LocationsRow> Locations { get; set; } = new List<LocationsRow>();
    }
}
