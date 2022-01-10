using AirQuality.Web.Models;
using AirQuality.Web.Models.ViewModels.Location;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly IOpenAqService _openAqApiService;
        private readonly IHistoryService _historyService;

        public LocationController(IOpenAqService openAqApiService, 
            IHistoryService historyService)
        {
            _openAqApiService = openAqApiService;
            _historyService = historyService;
        }

        public ActionResult Data(int? locationId = null)
        {
            if (locationId == null)
                return View(new LocationDataViewModel());

            // Obtain location
            var location = _openAqApiService.GetLocation(locationId.Value);

            // Obtain air quality readings from location
            var measurements = location.Parameters
                .Select(p => new Measurement(p))
                .ToList();

            // Update history with this search
            _historyService.AddToHistory(new HistoryItem(location));

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
