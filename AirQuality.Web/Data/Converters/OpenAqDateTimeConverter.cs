using System.Text.Json;
using System.Text.Json.Serialization;

namespace AirQuality.Web.Data.Converters
{
    public class OpenAqDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "yyyy-MM-dd HH:mm:sszz";

        public OpenAqDateTimeConverter() { }

        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(_format));
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var val = reader.GetString() ?? string.Empty;
            try
            {
                return DateTime.ParseExact(val, _format, null);
            }
            catch (Exception ex)
            {
                return DateTime.Parse(val);
            }
        }
    }
}
