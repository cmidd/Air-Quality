using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Models.ViewModels.Cities
{
    public class CitiesDataViewModel
    {
        public string City { get; set; } = string.Empty;

        public IList<LocationsRow> Locations { get; set; } = new List<LocationsRow>();
    }
}
