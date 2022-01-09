namespace AirQuality.Web.Models.OpenAq
{
    public record CitiesRow
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Locations { get; set; }
        public DateTime FirstUpdated { get; set; }
        public DateTime LastUpdated { get; set; }
        public IList<string> Parameters { get; set; } = new List<string>();
    }
}
