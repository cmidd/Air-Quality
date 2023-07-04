using AirQuality.Web.Models.ViewModels.Cities;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IOpenAqService _openAqApiService;

        public CitiesController(IOpenAqService openAqApiService)
        {
            _openAqApiService = openAqApiService;
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> Data(string city = "")
        {
            var model = new CitiesDataViewModel()
            {
                City = city,
                Locations = await _openAqApiService.GetLocations(city)
            };

            return View(model);
        }
    }
}
