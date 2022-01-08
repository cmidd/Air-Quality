namespace AirQuality.Web.Models.AppSettings
{
    public class OpenAqConfig
    {
        public string BaseAddress { get; set; } = string.Empty;
        public OpenAqEndpoints Endpoints { get; set; } = new OpenAqEndpoints();
    }
}
