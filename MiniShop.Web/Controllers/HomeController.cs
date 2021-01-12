using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniShop.App;
using MiniShop.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly InfoServerConfig _infoServerConfig;
        private readonly IHomeService _homeService;
        private readonly IBlogService _blogService;

        public HomeController(IBaseService baseService, 
            ILogger<HomeController> logger, 
            IProductService  productService, 
            ICategoryService categoryService, 
            IOptions<InfoServerConfig> optionAccessor, 
            IHomeService homeService,
            IBlogService blogService)
        : base(baseService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _infoServerConfig = optionAccessor.Value;
            _homeService = homeService;
            _blogService = blogService;
        }
        [Route("{f?}/{group?}")]
        public IActionResult Index()
        {
            var model = new Tuple<InfoServerConfig, ICollection<BlogDto>, CounterDto, ICollection<CategoryProductDto>>(
                _infoServerConfig,
                _blogService.BlogMains(),
                _homeService.GetCounter(),
                _productService.LoadDataPageDefault(15)
                ); 
            return View(model);
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
