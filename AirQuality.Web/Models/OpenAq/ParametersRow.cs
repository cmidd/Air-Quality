namespace AirQuality.Web.Models.OpenAq
{
    public class ParametersRow
    {
        public int Id { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Average { get; set; }
        public int LastValue { get; set; }
        public string Parameter { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public int ParameterId { get; set; }
        public DateTime FirstUpdated { get; set; }
    }
}
