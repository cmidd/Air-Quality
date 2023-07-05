using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IOpenAqService
    {
        /// <summary>
        /// Obtain list of cities available which have Air Quality data
        /// </summary>
        Task<CitiesResult?> GetCitiesResult(CitiesParams citiesParams);

        /// <summary>
        /// Obtain entire list of cities available which have Air Quality data
        /// </summary>
        Task<List<CitiesRow>> GetAllCities();

        /// <summary>
        /// Obtain list of locations which have a sensor to measure air quality
        /// </summary>
        Task<LocationsResult?> GetLocationsResult(LocationsParams locationsParams);

        /// <summary>
        /// Obtain list of locations for the given city
        /// </summary>
        /// <param name="city">Name of the city to get locations for</param>
        Task<List<LocationsRow>> GetLocations(string city);

        /// <summary>
        /// Obtain data on one location
        /// </summary>
        Task<LocationsRow> GetLocation(int id);
    }
}
