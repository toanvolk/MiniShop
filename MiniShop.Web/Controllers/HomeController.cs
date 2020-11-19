using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;
using MiniShop.Web.Models;
using Newtonsoft.Json;

namespace MiniShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService  productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            //var productDtos = _productService.LoadDataPage(page, pageSize);

            //----------------
            //var model = new Tuple<Tuple<ICollection<ProductDto>, int>>(productDtos);
            return View();
        }
        public IActionResult ProductPage(int pageNumber = 1, int pageSize = 9, string paramStrs = null)
        {
            ProductPageFilterDto filterDto = new ProductPageFilterDto();
            if (!string.IsNullOrWhiteSpace(paramStrs))
                filterDto = JsonConvert.DeserializeObject<ProductPageFilterDto>(paramStrs);
            var productDtos = _productService.LoadDataPage(pageNumber, pageSize, filterDto);
            var model = new
            {
                source = productDtos.Item1,
                total = productDtos.Item2
            };
            return Json(model);
        }
        public IActionResult ProductHero()
        {
            var productDtos = _productService.LoadDataHero();
            var model = new
            {
                source = productDtos
            };
            return Json(model);
        }        
        public IActionResult GetCategorys()
        {
            var model = _categoryService.LoadData();
            return Json(model);
        }
        #region Generic system
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


    }
}
