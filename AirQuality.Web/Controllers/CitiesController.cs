using AirQuality.Web.Models.ViewModels.Cities;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AirQuality.Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IOpenAqService _openAqApiService;

        public CitiesController(IOpenAqService openAqApiService)
        {
            _openAqApiService = openAqApiService;
        }

        public ActionResult Index()
        {
            var model = new CitiesViewModel()
            {
                Cities = _openAqApiService.GetAllCities()
            };

            return View(model);
        }

        [HttpGet]
        [HttpPost]
        public ActionResult Data(string city = "")
        {
            var model = new DataViewModel()
            {
                City = city,
                Locations = _openAqApiService.GetLocations(city)
            };

            return View(model);
        }
    }
}
