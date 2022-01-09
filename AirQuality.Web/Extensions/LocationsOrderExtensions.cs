using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Extensions
{
    public static class LocationsOrderExtensions
    {
        public static string GetName(this LocationsOrder locationsOrder) => locationsOrder switch
        {
            LocationsOrder.City => "city",
            LocationsOrder.Country => "country",
            LocationsOrder.Location => "location",
            LocationsOrder.SourceName => "sourceName",
            LocationsOrder.FirstUpdated => "firstUpdated",
            LocationsOrder.LastUpdated => "lastUpdated",
            LocationsOrder.Count => "count",
            LocationsOrder.Random => "random",
            _ => string.Empty,
        };
    }
}
