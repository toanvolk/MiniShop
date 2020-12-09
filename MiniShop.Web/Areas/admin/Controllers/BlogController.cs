using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Authorize]
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private ILogger<BlogController> _logger { get; set; }

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetDataAdmin([DataSourceRequest] DataSourceRequest request, ProductPageFilterDto paramSearch)
        {
            var source = _blogService.GetDataAdmin(request.Page, request.PageSize, paramSearch);
            var response = new DataResponeCommon<ICollection<BlogDto>>()
            {
                Data = source.Item1,
                Total = source.Item2,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BlogDto blogDto)
        {
            try
            {
                if (_blogService.Insert(blogDto))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Thêm thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Thêm thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
