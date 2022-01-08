using AirQuality.Web.Models.OpenAq.Responses;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IOpenAqService
    {
        /// <summary>
        /// Obtain list of cities available which have Air Quality data
        /// </summary>
        /// <param name="limit">Number of results returned</param>
        /// <param name="page">Paginate through results</param>
        /// <param name="offset"></param>
        /// <param name="sort">Define sort order</param>
        /// <param name="countryId">Limit results by a certain country using two letter country code, e.g. US</param>
        /// <param name="countries">Limit results by a certain country using two letter country codes, e.g. ["US", "MX"]</param>
        /// <param name="cities">Limit results by a certain city or cities, e.g. ["Chicago", "Boston"]</param>
        /// <param name="orderBy">Order by a field (city, country, firstUpdated, lastUpdated</param>
        /// <param name="entity"></param>
        OpenAqCitiesResult? GetCitiesResult(int limit = 100,
                                      int page = 1,
                                      int offset = 0,
                                      string sort = "asc",
                                      string? countryId = null,
                                      string[]? countries = null,
                                      string[]? cities = null,
                                      string orderBy = "city",
                                      string? entity = null);

        /// <summary>
        /// Obtain entire list of cities available which have Air Quality data
        /// </summary>
        IList<CitiesRow> GetAllCities();
    }
}
