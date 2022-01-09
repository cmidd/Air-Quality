namespace AirQuality.Web.Models.OpenAq
{
    public record Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString() => $"{Latitude},{Longitude}";
    }
}
