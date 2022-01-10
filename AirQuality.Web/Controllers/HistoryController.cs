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
            var history = _historyService.GetHistory();

            var model = new HistoryViewModel()
            {
                HistoryItems = history
            };

            return View(model);
        }

        public ActionResult Clear()
        {
            _historyService.ClearHistory();

            return RedirectToAction("Index");
        }
    }
}
