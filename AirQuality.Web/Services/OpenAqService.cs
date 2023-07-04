using AirQuality.Web.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using AirQuality.Web.Models.AppSettings;
using System.Text.Json;
using AirQuality.Web.Models.OpenAq;
using AirQuality.Web.Helpers;
using AirQuality.Web.Extensions;
using AirQuality.Web.Data.Converters;

namespace AirQuality.Web.Services
{
    public sealed class OpenAqService : IOpenAqService
    {
        private readonly OpenAqConfig _openAqConfig;
        private readonly ICacheService _cacheService;
        private readonly ILogger<OpenAqService> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public OpenAqService(IOptions<OpenAqConfig> openAqConfig,
            ICacheService cacheService, 
            ILogger<OpenAqService> logger)
        {
            _openAqConfig = openAqConfig.Value;
            _cacheService = cacheService;
            _logger = logger;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonSerializerOptions.Converters.Add(new OpenAqDateTimeConverter());
        }

        /// <inheritdoc />
        public CitiesResult? GetCitiesResult(CitiesParams citiesParams)
        {
            var openAqCitiesResult = new CitiesResult();

            // Build query string to send to API
            var query = citiesParams.ToQueryString();

            // Request data from client
            var response = GetClientResponse(_openAqConfig.Endpoints.Cities, query, _openAqConfig.ApiKey);

            // Deserialize client response
            try
            {
                openAqCitiesResult = JsonSerializer.Deserialize<CitiesResult>(response, _jsonSerializerOptions);

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
            var citiesResult = GetCitiesResult(new CitiesParams());

            if (citiesResult == null)
                return new List<CitiesRow>();

            var cities = citiesResult.Results.ToList();

            var pageCount = PaginationHelper.GetTotalPageCount(citiesResult.Meta.Found, citiesResult.Meta.Limit);

            if (pageCount > 1)
            {
                // Get remaining pages of results
                for (var i = 2; i <= pageCount; i++)
                {
                    citiesResult = GetCitiesResult(new CitiesParams() 
                    { 
                        Page = 1 
                    });

                    if (citiesResult != null)
                        cities.AddRange(citiesResult.Results);
                }
            }

            // Update cache
            _cacheService.CitiesList = cities;

            return cities;
        }

        /// <inheritdoc />
        public LocationsResult? GetLocationsResult(LocationsParams locationsParams)
        {
            var locationsResult = new LocationsResult();

            // Build query string to send to API
            var query = locationsParams.ToQueryString();

            // Request data from client
            var response = GetClientResponse(_openAqConfig.Endpoints.Locations, query, _openAqConfig.ApiKey);

            // Deserialize client response
            try
            {
                locationsResult = JsonSerializer.Deserialize<LocationsResult>(response, _jsonSerializerOptions);

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

            var locationsResult = GetLocationsResult(new LocationsParams()
            {
                Cities = new string[1] { city }
            });

            return locationsResult?.Results ?? new List<LocationsRow>();
        }

        /// <inheritdoc />
        public LocationsRow GetLocation(int id)
        {
            var locationsResult = GetLocationsResult(new LocationsParams()
            {
                LocationId = id
            });
                
            return locationsResult?.Results?.FirstOrDefault() ?? new LocationsRow();
        }

        private string GetClientResponse(string endpoint, string? query = null, string apiKey = null)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_openAqConfig.BaseAddress)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(apiKey))
                    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

                if (!string.IsNullOrWhiteSpace(query))
                    endpoint += $"?{query}";

                var request = client.GetAsync(endpoint);
                var response = request.Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errors = JsonSerializer.Deserialize<HttpValidationError>(response.Content.ReadAsStringAsync().Result);

                    throw new Exception($"Request to {client.BaseAddress} failed. Client returned {response.StatusCode}: {response.ReasonPhrase}.");
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
    }
}
