using AirQuality.Web.Models.OpenAq;
using System.Text;

namespace AirQuality.Web.Extensions
{
    public static class LocationsParamsExtensions
    {
        public static string ToQueryString(this LocationsParams locationsParams)
        {
            var query = new StringBuilder($"limit={locationsParams.Limit}");

            query.Append($"&page={locationsParams.Page}");
            query.Append($"&offset={locationsParams.Offset}");
            query.Append($"&sort={locationsParams.Sort.GetName()}");
            query.Append($"&radius={locationsParams.Radius}");
            query.Append($"&order_by={locationsParams.OrderBy.GetName()}");
            query.Append($"&dumpRaw={locationsParams.DumpRaw.ToString().ToLower()}");

            if (locationsParams.HasGeo != null)
                query.Append($"&has_geo={locationsParams.HasGeo.Value.ToString().ToLower()}");

            if (locationsParams.ParameterId != null)
                query.Append($"&parameter_id={locationsParams.ParameterId.Value}");

            if (locationsParams.Parameters?.Any() == true)
                query.Append(string.Join("", locationsParams.Parameters.Select(p => $"&parameter={p}")));

            if (locationsParams.Units?.Any() == true)
                query.Append(string.Join("", locationsParams.Units.Select(u => $"&unit={u}")));

            if (locationsParams.Coordinates != null)
                query.Append($"&coordinates={locationsParams.Coordinates.ToString}");

            if (!string.IsNullOrWhiteSpace(locationsParams.CountryId))
                query.Append($"&country_id={locationsParams.CountryId}");

            if (locationsParams.Countries?.Any() == true)
                query.Append(string.Join("", locationsParams.Countries.Select(c => $"&country={c}")));

            if (locationsParams.Cities?.Any() == true)
                query.Append(string.Join("", locationsParams.Cities.Select(c => $"&city={c}")));

            if (locationsParams.LocationId != null)
                query.Append($"&location_id={locationsParams.LocationId}");

            if (locationsParams.Locations?.Any() == true)
                query.Append(string.Join("", locationsParams.Locations.Select(l => $"&location={l}")));

            if (locationsParams.IsMobile != null)
                query.Append($"&isMobile={locationsParams.IsMobile.Value.ToString().ToLower()}");

            if (locationsParams.IsAnalysis != null)
                query.Append($"&isAnalysis={locationsParams.IsAnalysis.Value.ToString().ToLower()}");

            if (locationsParams.SourceNames?.Any() == true)
                query.Append(string.Join("", locationsParams.SourceNames.Select(sn => $"&sourceName={sn}")));

            if (!string.IsNullOrWhiteSpace(locationsParams.Entity))
                query.Append($"&entity={locationsParams.Entity}");

            if (locationsParams.SensorType != null)
                query.Append($"&sensorType={locationsParams.SensorType.Value.GetName()}");

            if (locationsParams.ModelNames?.Any() == true)
                query.Append(string.Join("", locationsParams.ModelNames.Select(mn => $"&modelName={mn}")));

            if (locationsParams.ManufacturerNames?.Any() == true)
                query.Append(string.Join("", locationsParams.ManufacturerNames.Select(mn => $"&manufacturerName={mn}")));

            return query.ToString();
        }
    }
}
