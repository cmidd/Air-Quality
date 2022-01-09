using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Extensions
{
    public static class SensorTypeExtensions
    {
        public static string GetName(this SensorType sensorType) => sensorType switch
        {
            SensorType.ReferenceGrade => "reference grade",
            SensorType.LowCostSensor => "low-cost sensor",
            _ => string.Empty,
        };
    }
}
