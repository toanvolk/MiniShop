using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniShop.App;

namespace MiniShop.Web.Controllers
{
    [Route("san-pham")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly InfoServerConfig _infoServerConfig;
        public ICategoryService _categoryService;

        public ProductController(IBaseService baseService, 
            IProductService productService, 
            IOptions<InfoServerConfig> optionAccessor,
            ICategoryService categoryService
            ) : base(baseService)
        {
            _productService = productService;
            _infoServerConfig = optionAccessor.Value;
            _categoryService = categoryService;
        }
        [Route("{code}")]
        public IActionResult Review(string code)
        {
            //filter code => productId
            var productId = _productService.GetProductId(code);
            var productDto = _productService.GetData(productId);
            if (productId == Guid.Empty) return Redirect(_infoServerConfig.PathPageNoteFound);           
            productDto.Picture = $"{_infoServerConfig.FileRootPath}/{productDto.Picture}";

            var model = new Tuple<ProductDto, InfoServerConfig>(productDto, _infoServerConfig);
            return View("Review", model);
        }
        [Route("data/{productId}")]
        public IActionResult ProductView(Guid productId)
        {
            var productDto = _productService.LoadDataView(productId);
            var model = new
            {
                source = productDto
            };
            return Json(model);
        }

        public IActionResult Index()
        {
            var model = new Tuple<InfoServerConfig,ICollection<CategoryProductDto>>(
                  _infoServerConfig,
                  _productService.LoadDataPageDefault(15)
                  );
            return View(model);
        }
        [Route("loai/{code}")]
        public IActionResult ProductWithCategory(string code)
        {
            var categoryProducts = _categoryService.GetDataByCode(code);
            var model = new Tuple<InfoServerConfig, ICollection<CategoryProductDto>>(
                  _infoServerConfig,
                  new List<CategoryProductDto>() { categoryProducts }
                  );
            return View(model);
        }
    }
}
