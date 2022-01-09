using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record Result<T>
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; } = new Meta();

        [JsonPropertyName("results")]
        public IList<T> Results { get; set; } = new List<T>();
    }
}
