using AirQuality.Web.Models.ViewModels.History;
using AirQuality.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public ActionResult Index()
        {
            var userId = Request.Cookies[Constants.UserIdCookie];
            var history = _historyService.GetHistory(userId);

            var model = new HistoryViewModel()
            {
                HistoryItems = history
            };

            return View(model);
        }

        public ActionResult Clear()
        {
            var userId = Request.Cookies[Constants.UserIdCookie] ?? Guid.NewGuid().ToString();

            _historyService.ClearHistory(userId);

            return RedirectToAction("Index");
        }
    }
}
