namespace AirQuality.Web.Models.ViewModels.Location
{
    public class LocationDataViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string SensorType { get; set; } = string.Empty;
        public IList<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
