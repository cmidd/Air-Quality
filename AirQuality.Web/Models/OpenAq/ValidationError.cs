namespace AirQuality.Web.Models.OpenAq
{
    public record ValidationError
    {
        public IList<string> Loc { get; set; } = new List<string>();
        public string Msg { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
