namespace AirQuality.Web.Models.OpenAq
{
    public class CitiesParams
    {
        public int Limit { get; set; } = 100;
        public int Page { get; set; } = 1;
        public int Offset { get; set; } = 0;
        public SortOrder Sort { get; set; } = SortOrder.Ascending;
        public string? CountryId { get; set; } = null;
        public string[]? Countries { get; set; } = null;
        public string[]? Cities { get; set; } = null;
        public CitiesOrder OrderBy { get; set; } = CitiesOrder.City;
        public string? Entity { get; set; } = null;
    }
}
