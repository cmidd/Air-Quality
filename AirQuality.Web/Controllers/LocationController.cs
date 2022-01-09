using AirQuality.Web.Models;
using AirQuality.Web.Models.ViewModels.Location;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly IOpenAqService _openAqApiService;

        public LocationController(IOpenAqService openAqApiService)
        {
            _openAqApiService = openAqApiService;
        }

        public ActionResult Data(int? locationId = null)
        {
            if (locationId == null)
                return View(new LocationDataViewModel());

            var location = _openAqApiService.GetLocation(locationId.Value);

            var measurements = location.Parameters
                .Select(p => new Measurement(p))
                .ToList();

            var model = new LocationDataViewModel()
            {
                Id = location.Id,
                Name = location.Name,
                City = location.City,
                Country = location.Country,
                SensorType = location.SensorType,
                Measurements = measurements
            };

            return View(model);
        }
    }
}
