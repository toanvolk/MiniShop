﻿using System;
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

        private readonly IProductService _productService;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger, IProductService productService)
        {
            _blogService = blogService;
            _logger = logger;
            _productService = productService;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Blog";
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
            var model = new Tuple<ICollection<CategoryDto>>(
                _productService.GetCategories());
            return View(model);
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
        [HttpPost]
        public IActionResult Delete(Guid blogId)
        {
            try
            {
                if (_blogService.Delete(blogId))
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
        [HttpPost]
        public JsonResult UpdateStatu(Guid blogId, bool ischecked)
        {
            try
            {
                if (_blogService.UpdateStatu(blogId, ischecked))
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

        public IActionResult Edit(Guid id)
        {
            var model = new Tuple<BlogDto, ICollection<CategoryDto>>(_blogService.GetDataById(id), _productService.GetCategories());
            return View(model);
        }
        [HttpPost]
        public JsonResult Update(BlogDto blogDto)
        {
            try
            {
                if (_blogService.Update(blogDto))
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

    }
}
