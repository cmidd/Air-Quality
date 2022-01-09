using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record Coordinates
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        public override string ToString() => $"{Latitude},{Longitude}";
    }
}
