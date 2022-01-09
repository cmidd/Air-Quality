namespace AirQuality.Web.Models.OpenAq
{
    public record LocationsRow
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public IList<Source> Sources { get; set; } = new List<Source>();
        public bool IsMobile { get; set; }
        public bool IsAnalysis { get; set; }
        public IList<ParametersRow> Parameters { get; set; } = new List<ParametersRow>();
        public string SensorType { get; set; } = string.Empty;
        public Coordinates Coordinates { get; set; } = new Coordinates();
        public DateTime LastUpdated { get; set; }
        public DateTime FirstUpdated { get; set; }
        public int Measurements { get; set; }
    }
}
