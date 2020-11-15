using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private ILogger<HomeController> _logger { get; set; }

        public HomeController(IHomeService homeService, ILogger<HomeController> logger)
        {
            _homeService = homeService;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetProductReview([FromForm] DateTime fromDate, [FromForm] DateTime toDate)
        {
            var model = _homeService.GetProductReview(fromDate, toDate);
            return Json(new
            {
                Name = model.Select(o=>o.ProductName).ToList(),
                Count = new List<List<int>>() { model.Select(o => o.Count).ToList() },
            });
        }
    }
}
