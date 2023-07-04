namespace AirQuality.Web.Models.AppSettings
{
    public class CachingConfig
    {
        public string RedisCacheUrl { get; set; } = string.Empty;
        public string CitiesKey { get; set; } = "cities:{0}";
        public string LocationsKey { get; set; } = "locations:{0}";
        public string SearchHistoryKey { get; set; } = "searchhistory:{0}";
        public int CacheKeyExpirationInSeconds { get; set; } = 86400;
    }
}
