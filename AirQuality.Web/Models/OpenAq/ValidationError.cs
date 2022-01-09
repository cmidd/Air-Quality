using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record ValidationError
    {
        [JsonPropertyName("loc")]
        public IList<string> Loc { get; set; } = new List<string>();

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
