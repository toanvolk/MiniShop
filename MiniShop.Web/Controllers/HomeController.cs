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
            var model = new Tuple<InfoServerConfig, ICollection<BlogDto>, CounterDto>(_infoServerConfig, _blogService.BlogMains(), _homeService.GetCounter());
            return View(model);
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
