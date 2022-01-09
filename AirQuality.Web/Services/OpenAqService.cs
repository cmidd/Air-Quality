using AirQuality.Web.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using AirQuality.Web.Models.AppSettings;
using System.Text.Json;
using AirQuality.Web.Models.OpenAq.Responses;
using AirQuality.Web.Helpers;

namespace AirQuality.Web.Services
{
    public sealed class OpenAqService : IOpenAqService
    {
        private readonly OpenAqConfig _openAqConfig;
        private readonly ICacheService _cacheService;

        public OpenAqService(IOptions<OpenAqConfig> openAqConfig, 
            ICacheService cacheService)
        {
            _openAqConfig = openAqConfig.Value;
            _cacheService = cacheService;
        }

        /// <inheritdoc />
        public OpenAqCitiesResult? GetCitiesResult(int limit = 100, int page = 1, int offset = 0, string sort = "asc", string? countryId = null, string[]? countries = null, string[]? cities = null, string orderBy = "city", string? entity = null)
        {
            var openAqCitiesResult = new OpenAqCitiesResult();

            // Build query string to send to API
            var query = $"limit={limit}&page={page}&offset={offset}&sort={sort}&orderBy={orderBy}";

            if (!string.IsNullOrWhiteSpace(countryId))
                query += $"&countryId={countryId}";

            if (!string.IsNullOrWhiteSpace(entity))
                query += $"&entity={entity}";

            if (countries?.Any() == true)
                query += countries.Select(c => $"&country={c}");

            if (cities?.Any() == true)
                query += cities.Select(c => $"&city={c}");

            // Request data from client
            var response = GetClientResponse(_openAqConfig.Endpoints.Cities, query);

            // Deserialize client response
            try
            {
                openAqCitiesResult = JsonSerializer.Deserialize<OpenAqCitiesResult>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (openAqCitiesResult == null)
                {
                    throw new Exception($"Could not deserialize client response to type {nameof(OpenAqCitiesResult)}:\n{response}");
                }

                return openAqCitiesResult;
            }
            catch (Exception ex)
            {
                // todo: log error
                var errorMsg = ex.Message;
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
                return null;

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
                    var errors = JsonSerializer.Deserialize<ValidateOptionsResult>(response.Content.ReadAsStringAsync().Result);

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
                // todo: log error
                var errorMsg = ex.Message;
                return string.Empty;
            }
        }
    }
}
