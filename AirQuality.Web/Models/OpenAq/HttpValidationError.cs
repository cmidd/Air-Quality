using System.Text.Json.Serialization;

namespace AirQuality.Web.Models.OpenAq
{
    public record HttpValidationError
    {
        [JsonPropertyName("detail")]
        public IList<ValidationError> Detail { get; set; } = new List<ValidationError>();
    }
}
