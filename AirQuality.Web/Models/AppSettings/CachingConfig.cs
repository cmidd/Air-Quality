namespace AirQuality.Web.Models.AppSettings
{
    public class CachingConfig
    {
        public string CitiesListKey { get; set; } = string.Empty;
        public int CitiesListExpiration { get; set; }
        public string SearchHistoryKey { get; set; } = string.Empty;
        public int SearchHistoryExpiration { get; set; }
    }
}
