using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record CitiesRow
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("locations")]
        public int Locations { get; set; }

        [JsonPropertyName("firstUpdated")]
        public DateTime FirstUpdated { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("parameters")]
        public IList<string> Parameters { get; set; } = new List<string>();
    }
}
