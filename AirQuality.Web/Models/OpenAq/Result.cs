namespace AirQuality.Web.Models.OpenAq
{
    public record Result<T>
    {
        public Meta Meta { get; set; } = new Meta();
        public IList<T> Results { get; set; } = new List<T>();
    }
}
