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
    public class ToolController : Controller
    {
        private readonly IBlogService _blogService;
        private ILogger<ToolController> _logger { get; set; }

        private readonly IPostService _postService;

        public ToolController(IBlogService blogService, ILogger<ToolController> logger, IPostService postService)
        {
            _blogService = blogService;
            _logger = logger;
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Posts()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreatePost(PostDto data)
        {
            try
            {
                if (_postService.Create(data))
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
        [HttpPost]
        public JsonResult UpdatePost(PostDto data)
        {
            try
            {
                if (_postService.Update(data))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Cập nhật thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Cập nhật thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        [HttpPost]
        public JsonResult DeletePost(Guid postId)
        {
            try
            {
                if (_postService.Delete(postId))
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.OK, Message = "Xóa thành công" };
                    return Json(response);
                }
                else
                {
                    var response = new DataResponeCommon() { Statu = StatuCodeEnum.InternalServerError, Message = "Xóa thất bại" };
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        public JsonResult LoadPost()
        {
            var postDtos = _postService.LoadData();
            var response = new DataResponeCommon<PageDataDto<PostDto>>()
            {
                Statu = StatuCodeEnum.OK,
                Message = "Truy vấn thành công",
                Data = new PageDataDto<PostDto>(postDtos.ToList(), postDtos.Count)
            };
            return Json(response);
        }
        public JsonResult GetDataByIdPost(Guid postId)
        {
            var postDto = _postService.GetDataById(postId);
            var response = new DataResponeCommon<PostDto>()
            {
                Statu = StatuCodeEnum.OK,
                Message = "Truy vấn thành công",
                Data = postDto
            };
            return Json(response);
        }
    }
}
