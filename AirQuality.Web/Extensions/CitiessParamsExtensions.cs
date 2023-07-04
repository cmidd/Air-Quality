using AirQuality.Web.Models.OpenAq;
using System.Text;

namespace AirQuality.Web.Extensions
{
    public static class CitiesParamsExtensions
    {
        public static string ToQueryString(this CitiesParams citiesParams)
        {
            var query = new StringBuilder($"limit={citiesParams.Limit}");

            query.Append($"&page={citiesParams.Page}");
            query.Append($"&offset={citiesParams.Offset}");
            query.Append($"&sort={citiesParams.Sort.GetName()}");
            query.Append($"&order_by={citiesParams.OrderBy.GetName()}");

            if (!string.IsNullOrWhiteSpace(citiesParams.CountryId))
                query.Append($"&country_id={citiesParams.CountryId}");

            if (citiesParams.Countries?.Any() == true)
                query.Append(string.Join("", citiesParams.Countries.Select(c => $"&country={c}")));

            if (citiesParams.Cities?.Any() == true)
                query.Append(string.Join("", citiesParams.Cities.Select(c => $"&city={c}")));

            if (!string.IsNullOrWhiteSpace(citiesParams.Entity))
                query.Append($"&entity={citiesParams.Entity}");

            return query.ToString();
        }
    }
}
