using AirQuality.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOpenAqApiService _openAqApiService;

        public HomeController(IOpenAqApiService openAqApiService)
        {
            _openAqApiService = openAqApiService;
        }

        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
