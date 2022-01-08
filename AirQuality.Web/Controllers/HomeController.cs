﻿using AirQuality.Web.Services.Interfaces;
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
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
