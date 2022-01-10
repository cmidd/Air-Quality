using AirQuality.Web.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using AirQuality.Web.Models.AppSettings;
using System.Text.Json;
using AirQuality.Web.Models.OpenAq;
using AirQuality.Web.Helpers;
using AirQuality.Web.Extensions;

namespace AirQuality.Web.Services
{
    public sealed class OpenAqService : IOpenAqService
    {
        private readonly OpenAqConfig _openAqConfig;
        private readonly ICacheService _cacheService;
        private readonly ILogger<OpenAqService> _logger;

        public OpenAqService(IOptions<OpenAqConfig> openAqConfig,
            ICacheService cacheService, 
            ILogger<OpenAqService> logger)
        {
            _openAqConfig = openAqConfig.Value;
            _cacheService = cacheService;
            _logger = logger;
        }

        /// <inheritdoc />
        public CitiesResult? GetCitiesResult(
            int limit = 100,
            int page = 1,
            int offset = 0,
            SortOrder sort = SortOrder.Ascending,
            string? countryId = null,
            string[]? countries = null,
            string[]? cities = null,
            CitiesOrder orderBy = CitiesOrder.City,
            string? entity = null)
        {
            var openAqCitiesResult = new CitiesResult();

            // Build query string to send to API
            var query = BuildCitiesQuery(limit, page, offset, sort, countryId, countries, cities, orderBy, entity);

            // Request data from client
            var response = GetClientResponse(_openAqConfig.Endpoints.Cities, query);

            // Deserialize client response
            try
            {
                openAqCitiesResult = JsonSerializer.Deserialize<CitiesResult>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (openAqCitiesResult == null)
                {
                    throw new Exception($"Could not deserialize client response to type {nameof(CitiesResult)}:\n{response}");
                }

                return openAqCitiesResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCitiesResult)} failed");
                return null;
            }
        }

        /// <inheritdoc />
        public IList<CitiesRow> GetAllCities()
        {
            var cacheValue = _cacheService.CitiesList;

            if (cacheValue.Any())
                return cacheValue;

            // Get first page worth of results
            var citiesResult = GetCitiesResult();

            if (citiesResult == null)
                return new List<CitiesRow>();

            var cities = citiesResult.Results.ToList();

            var pageCount = PaginationHelper.GetTotalPageCount(citiesResult.Meta.Found, citiesResult.Meta.Limit);

            if (pageCount > 1)
            {
                // Get remaining pages of results
                for (var i = 2; i <= pageCount; i++)
                {
                    citiesResult = GetCitiesResult(page: i);

                    if (citiesResult != null)
                        cities.AddRange(citiesResult.Results);
                }
            }

            // Update cache
            _cacheService.CitiesList = cities;

            return cities;
        }

        /// <inheritdoc />
        public LocationsResult? GetLocationsResult(
            int limit = 100,
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
            bool dumpRaw = false)
        {
            var locationsResult = new LocationsResult();

            // Build query string to send to API
            var query = BuildLocationsQuery(limit, page, offset, sort, hasGeo, parameterId, 
                parameters, units, coordinates, radius, countryId, countries, cities, locationId, 
                locations, orderBy, isMobile, isAnalysis, sourceNames, entity, sensorType, modelNames, manufacturerNames, dumpRaw);

            // Request data from client
            var response = GetClientResponse(_openAqConfig.Endpoints.Locations, query);

            // Deserialize client response
            try
            {
                locationsResult = JsonSerializer.Deserialize<LocationsResult>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (locationsResult == null)
                {
                    throw new Exception($"Could not deserialize client response to type {nameof(LocationsResult)}:\n{response}");
                }

                return locationsResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetLocationsResult)} failed");
                return null;
            }
        }

        /// <inheritdoc />
        public IList<LocationsRow> GetLocations(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return new List<LocationsRow>();

            var locationsResult = GetLocationsResult(cities: new string[1] { city });

            return locationsResult?.Results ?? new List<LocationsRow>();
        }

        /// <inheritdoc />
        public LocationsRow GetLocation(int id)
        {
            var locationsResult = GetLocationsResult(locationId: id);
                
            return locationsResult?.Results?.FirstOrDefault() ?? new LocationsRow();
        }

        private string GetClientResponse(string endpoint, string? query = null)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_openAqConfig.BaseAddress)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(query))
                    endpoint += $"?{query}";

                var request = client.GetAsync(endpoint);
                var response = request.Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errors = JsonSerializer.Deserialize<HttpValidationError>(response.Content.ReadAsStringAsync().Result);

                    throw new Exception($"Request to {client.BaseAddress} failed. " +
                        $"Client returned {response.StatusCode}: {response.ReasonPhrase}.");
                }

                string? result = response.Content?.ReadAsStringAsync()?.Result;

                if (string.IsNullOrWhiteSpace(result))
                    throw new Exception($"Request to {client.BaseAddress} failed. Client returned empty content");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetClientResponse)} failed");
                return string.Empty;
            }
        }

        private string BuildCitiesQuery(
            int limit = 100,
            int page = 1,
            int offset = 0,
            SortOrder sort = SortOrder.Ascending,
            string? countryId = null,
            string[]? countries = null,
            string[]? cities = null,
            CitiesOrder orderBy = CitiesOrder.City,
            string? entity = null)
        {
            var query = $"limit={limit}&page={page}&offset={offset}&sort={sort.GetName()}&order_by={orderBy.GetName()}";

            if (!string.IsNullOrWhiteSpace(countryId))
                query += $"&country_id={countryId}";

            if (!string.IsNullOrWhiteSpace(entity))
                query += $"&entity={entity}";

            if (countries?.Any() == true)
                query += string.Join("", countries.Select(c => $"&country={c}"));

            if (cities?.Any() == true)
                query += string.Join("", cities.Select(c => $"&city={c}"));

            return query;
        }

        private string BuildLocationsQuery(
            int limit = 100,
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
            bool dumpRaw = false)
        {
            var query = $"limit={limit}&page={page}&offset={offset}&sort={sort.GetName()}&radius={radius}&order_by={orderBy.GetName()}&dumpRaw={dumpRaw.ToString().ToLower()}";

            if (hasGeo != null)
                query += $"&has_geo={hasGeo.Value.ToString().ToLower()}";

            if (parameterId != null)
                query += $"&parameter_id={parameterId.Value}";

            if (parameters?.Any() == true)
                query += string.Join("", parameters.Select(p => $"&parameter={p}"));

            if (units?.Any() == true)
                query += string.Join("", units.Select(u => $"&unit={u}"));

            if (coordinates != null)
                query += $"&coordinates={coordinates.ToString}";

            if (!string.IsNullOrWhiteSpace(countryId))
                query += $"&country_id={countryId}";

            if (countries?.Any() == true)
                query += string.Join("", countries.Select(c => $"&country={c}"));

            if (cities?.Any() == true)
                query += string.Join("", cities.Select(c => $"&city={c}"));

            if (locationId != null)
                query += $"&location_id={locationId}";

            if (locations?.Any() == true)
                query += string.Join("", locations.Select(l => $"&location={l}"));

            if (isMobile != null)
                query += $"&isMobile={isMobile.Value.ToString().ToLower()}";

            if (isAnalysis != null)
                query += $"&isAnalysis={isAnalysis.Value.ToString().ToLower()}";

            if (sourceNames?.Any() == true)
                query += string.Join("", sourceNames.Select(sn => $"&sourceName={sn}"));

            if (!string.IsNullOrWhiteSpace(entity))
                query += $"&entity={entity}";

            if (sensorType != null)
                query += $"&sensorType={sensorType.Value.GetName()}";

            if (modelNames?.Any() == true)
                query += string.Join("", modelNames.Select(mn => $"&modelName={mn}"));

            if (manufacturerNames?.Any() == true)
                query += string.Join("", manufacturerNames.Select(mn => $"&manufacturerName={mn}"));

            return query;
        }
    }
}
