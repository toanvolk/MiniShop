using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniShop.App;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;

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
        [Route("{blogId}")]
        public IActionResult Index(Guid blogId)
        {
            var blogDto = blogId == Guid.Empty
                ? new BlogDto() {
                    Title = "Thực phẩm chức năng | Sản phẩm đặc trị | Sản phẩm hỗ trợ sinh lý nam giới | Sản phẩm giúp chị Em làm đẹp",
                    Author = "hanglink.info",
                    PublishDate = new DateTime(2020, 12, 01),
                    Content = "Cung cấp thông tin các sản phẩm từ các nguồn sản xuất chính hãng"
                }
                : _blogService.GetDataById(blogId);
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            HttpUtility.HtmlDecode(blogDto.Content, myWriter);
            blogDto.Content = myWriter.ToString();
            var model = new Tuple<InfoServerConfig,BlogDto>(_infoServerConfig, blogDto);
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
