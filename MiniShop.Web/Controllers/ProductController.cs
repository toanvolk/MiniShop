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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly InfoServerConfig _infoServerConfig;
        public ProductController(IProductService productService, IOptions<InfoServerConfig> optionAccessor)
        {
            _productService = productService;
            _infoServerConfig = optionAccessor.Value;
        }
        [Route("{code}")]
        public IActionResult Index(string code)
        {
            //filter code => productId
            var productId = _productService.GetProductId(code);
            var productDto = _productService.GetData(productId);
            if (productId == Guid.Empty) return Redirect(_infoServerConfig.PathPageNoteFound);
            var useHostAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            _productService.CountClick(productId, useHostAddress, string.Empty);
            productDto.Picture = $"{_infoServerConfig.FileRootPath}/{productDto.Picture}";

            var model = new Tuple<ProductDto, InfoServerConfig>(productDto, _infoServerConfig);
            return View("Index", model);
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
    }
}
