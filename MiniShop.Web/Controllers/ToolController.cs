using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniShop.App;

namespace MiniShop.Web.Controllers
{
    [Route("tool")]
    public class ToolController : BaseController
    {
        private readonly InfoServerConfig _infoServerConfig;
        private readonly IProductService _productService;

        public ToolController(IBaseService baseService, IBlogService blogService
            , IOptions<InfoServerConfig> optionAccessor
            , IProductService productService) : base(baseService)
        {
            _infoServerConfig = optionAccessor.Value;
            _productService = productService;
        }
        [Route("posts")]
        public IActionResult Posts()
        {
            //var model = new Tuple<InfoServerConfig>(_infoServerConfig);
            return View();
        }

    }
}
