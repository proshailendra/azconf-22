using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Extensions;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICatalogService _catalogService;
        private IMemoryCache _cache;
        private IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService, IMemoryCache cache, IDistributedCache distributedCache)
        {
            _catalogService = catalogService;
            _logger = logger;
            _cache = cache;
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Item> itemList = await LoadItems();
           //var itemList = _catalogService.GetItems();
            return View(itemList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<Item>> LoadItems()
        {
            IEnumerable<Item> listItems = null;
            string recordKey = "ItemList";

            listItems = await _distributedCache.GetRecordAsync<IEnumerable<Item>>(recordKey);

            if (listItems is null)
            {
                listItems = _catalogService.GetItems();

                await _distributedCache.SetRecordAsync(recordKey, listItems);
            }

            return listItems;
        }
    }
}