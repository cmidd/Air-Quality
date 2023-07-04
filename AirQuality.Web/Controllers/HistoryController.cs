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

        public async Task<ActionResult> Index()
        {
            var userId = Request.Cookies[Constants.UserIdCookie];
            var history = await _historyService.GetHistory(userId);

            var model = new HistoryViewModel()
            {
                HistoryItems = history
            };

            return View(model);
        }

        public async Task<ActionResult> Clear()
        {
            var userId = Request.Cookies[Constants.UserIdCookie] ?? Guid.NewGuid().ToString();

            await _historyService.ClearHistory(userId);

            return RedirectToAction("Index");
        }
    }
}
