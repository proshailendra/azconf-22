using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ePizzaHub.WebUI.ViewComponents
{
    public class PizzaMenuViewComponent : ViewComponent
    {
        private ICatalogService _catalogService;

        public PizzaMenuViewComponent(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IViewComponentResult Invoke()
        {
            //string key = "catalogs";
            //var itemList = _cache.GetOrCreate(key, (entry) =>
            //{
            //    entry.AbsoluteExpiration = DateTime.Now.AddHours(24);
            //    entry.SlidingExpiration = TimeSpan.FromMinutes(15);
            //    entry.Priority = CacheItemPriority.Normal;

            //    var data = _catalogService.GetItems();
            //    return data;
            //});

            IEnumerable<Item> itemList = _catalogService.GetItems();
            return View("/views/shared/_PizzaMenu.cshtml", itemList);
        }
    }
}