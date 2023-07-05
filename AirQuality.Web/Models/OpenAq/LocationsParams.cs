namespace AirQuality.Web.Models.OpenAq
{
    public class LocationsParams
    {
        public int Limit { get; set; } = 100;
        public int Page { get; set; } = 1;
        public int Offset { get; set; } = 0;
        public SortOrder Sort { get; set; } = SortOrder.Descending;
        public bool? HasGeo { get; set; } = null;
        public int? ParameterId { get; set; } = null;
        public string[]? Parameters { get; set; } = null;
        public string[]? Units { get; set; } = null;
        public Coordinates? Coordinates { get; set; } = null;
        public int Radius { get; set; } = 1000;
        public string? CountryId { get; set; } = null;
        public string[]? Countries { get; set; } = null;
        public string[]? Cities { get; set; } = null;
        public int? LocationId { get; set; } = null;
        public string[]? Locations { get; set; } = null;
        public LocationsOrder OrderBy { get; set; } = LocationsOrder.LastUpdated;
        public bool? IsMobile { get; set; } = null;
        public bool? IsAnalysis { get; set; } = null;
        public string[]? SourceNames { get; set; } = null;
        public string? Entity { get; set; } = null;
        public SensorType? SensorType { get; set; } = null;
        public string[]? ModelNames { get; set; } = null;
        public string[]? ManufacturerNames { get; set; } = null;
        public bool DumpRaw { get; set; } = false;
    }
}
