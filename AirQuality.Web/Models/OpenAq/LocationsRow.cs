using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record LocationsRow
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("entity")]
        public string Entity { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("sources")]
        public IList<Source> Sources { get; set; } = new List<Source>();

        [JsonPropertyName("isMobile")]
        public bool? IsMobile { get; set; }

        [JsonPropertyName("isAnalysis")]
        public bool? IsAnalysis { get; set; }

        [JsonPropertyName("parameters")]
        public IList<ParametersRow> Parameters { get; set; } = new List<ParametersRow>();

        [JsonPropertyName("sensorType")]
        public string SensorType { get; set; } = string.Empty;

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; } = new Coordinates();

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("firstUpdated")]
        public DateTime FirstUpdated { get; set; }

        [JsonPropertyName("measurements")]
        public int Measurements { get; set; }
    }
}
