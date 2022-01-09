using AirQuality.Web.Models.ViewModels.Cities;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOpenAqService _openAqApiService;

        public HomeController(IOpenAqService openAqApiService)
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

        public ActionResult Error()
        {
            return View();
        }
    }
}
