namespace AirQuality.Web.Models.OpenAq
{
    public record Meta
    {
        public string Name { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Found { get; set; }
    }
}
