using AirQuality.Web.Models.OpenAq;
using System.Globalization;

namespace AirQuality.Web.Models
{
    public class Measurement
    {
        public string Parameter { get; set; } = string.Empty;
        public double Reading { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;

        public Measurement() { }
        public Measurement(ParametersRow parameter) 
        {
            Parameter = parameter.Parameter;
            Reading = parameter.LastValue;
            Unit = parameter.Unit;
            Date = parameter.LastUpdated.ToString("G", CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}
