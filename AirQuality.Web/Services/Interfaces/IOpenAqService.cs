using AirQuality.Web.Models.OpenAq;

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
        /// <param name="sort">Define sort order (asc, desc)</param>
        /// <param name="countryId">Limit results by a certain country using two letter country code, e.g. US</param>
        /// <param name="countries">Limit results by a certain country using two letter country codes, e.g. ["US", "MX"]</param>
        /// <param name="cities">Limit results by a certain city or cities, e.g. ["Chicago", "Boston"]</param>
        /// <param name="orderBy">Order by a field (city, country, firstUpdated, lastUpdated</param>
        /// <param name="entity"></param>
        CitiesResult? GetCitiesResult(int limit = 100,
                                      int page = 1,
                                      int offset = 0,
                                      SortOrder sort = SortOrder.Ascending,
                                      string? countryId = null,
                                      string[]? countries = null,
                                      string[]? cities = null,
                                      CitiesOrder orderBy = CitiesOrder.City,
                                      string? entity = null);

        /// <summary>
        /// Obtain entire list of cities available which have Air Quality data
        /// </summary>
        IList<CitiesRow> GetAllCities();

        /// <summary>
        /// Obtain list of locations which have a sensor to measure air quality
        /// </summary>
        LocationsResult? GetLocationsResult(int limit = 100,
                                            int page = 1,
                                            int offset = 0,
                                            SortOrder sort = SortOrder.Descending,
                                            bool? hasGeo = null,
                                            int? parameterId = null,
                                            string[]? parameters = null,
                                            string[]? units = null,
                                            Coordinates? coordinates = null,
                                            int radius = 1000,
                                            string? countryId = null,
                                            string[]? countries = null,
                                            string[]? cities = null,
                                            int? locationId = null,
                                            string[]? locations = null,
                                            LocationsOrder orderBy = LocationsOrder.LastUpdated,
                                            bool? isMobile = null,
                                            bool? isAnalysis = null,
                                            string[]? sourceNames = null,
                                            string? entity = null,
                                            SensorType? sensorType = null,
                                            string[]? modelNames = null,
                                            string[]? manufacturerNames = null,
                                            bool dumpRaw = false);

        /// <summary>
        /// Obtain list of locations for the given city
        /// </summary>
        /// <param name="city">Name of the city to get locations for</param>
        IList<LocationsRow> GetLocations(string city);

        /// <summary>
        /// Obtain data on one location
        /// </summary>
        LocationsRow GetLocation(int id);
    }
}
