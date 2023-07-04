using AirQuality.Web.Models.OpenAq;

namespace AirQuality.Web.Services.Interfaces
{
    public interface IOpenAqService
    {
        /// <summary>
        /// Obtain list of cities available which have Air Quality data
        /// </summary>
        CitiesResult? GetCitiesResult(CitiesParams citiesParams);

        /// <summary>
        /// Obtain entire list of cities available which have Air Quality data
        /// </summary>
        IList<CitiesRow> GetAllCities();

        /// <summary>
        /// Obtain list of locations which have a sensor to measure air quality
        /// </summary>
        LocationsResult? GetLocationsResult(LocationsParams locationsParams);

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
