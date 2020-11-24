using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniShop.App;

namespace MiniShop.Web.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("{code}")]
        public IActionResult Index(string code)
        {
            //filter code => productId
            var productId = _productService.GetProductId(code);
            var productDto = _productService.GetData(productId);
            if (productId == Guid.Empty) return Redirect("/html/not-found.html");
            var useHostAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            _productService.CountClick(productId, useHostAddress, string.Empty);

            return View("Index", productDto);
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
