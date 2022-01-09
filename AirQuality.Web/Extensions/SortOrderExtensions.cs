using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Extensions
{
    public static class SortOrderExtensions
    {
        public static string GetName(this SortOrder sortOrder) => sortOrder switch
        {
            SortOrder.Ascending => "asc",
            SortOrder.Descending => "desc",
            _ => string.Empty,
        };
    }
}
