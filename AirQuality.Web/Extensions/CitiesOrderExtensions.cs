using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Extensions
{
    public static class CitiesOrderExtensions
    {
        public static string GetName(this CitiesOrder citiesOrder) => citiesOrder switch
        {
            CitiesOrder.City => "city",
            CitiesOrder.Country => "country",
            CitiesOrder.FirstUpdated => "firstUpdated",
            CitiesOrder.LastUpdated => "lastUpdated",
            _ => string.Empty,
        };
    }
}
