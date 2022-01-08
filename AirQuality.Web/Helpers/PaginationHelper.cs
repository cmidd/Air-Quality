namespace AirQuality.Web.Helpers
{
    public static class PaginationHelper
    {
        public static int GetTotalPageCount(int results, int pageSize) => (results + pageSize - 1) / pageSize;
    }
}
