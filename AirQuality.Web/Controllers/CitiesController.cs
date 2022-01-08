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

        public ActionResult Index()
        {
            var cities = _openAqApiService.GetAllCities();

            return View(cities);
        }
    }
}
