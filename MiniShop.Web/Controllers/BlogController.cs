using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniShop.App;
using Newtonsoft.Json;
using System;

namespace MiniShop.Web.Controllers
{
    [Route("blog")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly InfoServerConfig _infoServerConfig;
        public BlogController(IBaseService baseService, IBlogService blogService, IOptions<InfoServerConfig> optionAccessor) : base(baseService)
        {
            _blogService = blogService;
            _infoServerConfig = optionAccessor.Value;
        }      
        public IActionResult Index()
        {
            var model = new Tuple<InfoServerConfig>(_infoServerConfig);
            return View("Index", model);
        }
        [HttpGet("page")]
        public IActionResult BlogPage(PageFilterDto pageFilterDto)
        {            
            var pageData = _blogService.LoadDataPage(pageFilterDto);            
            return Json(pageData);
        }
    }
}
