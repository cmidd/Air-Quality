namespace AirQuality.Web.Models.OpenAq.Responses
{
    public record OpenAqResult<T>
    {
        public Meta Meta { get; set; } = new Meta();
        public IList<T> Results { get; set; } = new List<T>();
    }
}
