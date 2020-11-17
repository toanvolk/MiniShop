using System;
using System.Collections.Generic;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private ILogger<ProductController> _logger { get; set; }
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Product";
            return View();
        }
        public JsonResult LoadData()
        {
            var model = _productService.LoadData();
            var response = new DataResponeCommon<ICollection<ProductDto>>()
            {
                Data = model,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
        public JsonResult LoadDataPage([DataSourceRequest] DataSourceRequest request)
        {
            var source = _productService.LoadDataPage(request.Page, request.PageSize);
            var response = new DataResponeCommon<ICollection<ProductDto>>()
            {
                Data = source.Item1,
                Total = source.Item2,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
        [HttpPost]
        public IActionResult Add()
        {

            var model = new Tuple<ICollection<CategoryDto>,ICollection<AreaDto>, ICollection<string>>(
                _productService.GetCategories(), 
                _productService.GetAreas(),
                _productService.TagList());
            return PartialView("_add", model);
        }
        [HttpPost]
        public IActionResult Edit(Guid productId)
        {
            var model = new Tuple<ICollection<CategoryDto>, ICollection<AreaDto>, ProductDto, ICollection<string>>(
                _productService.GetCategories(),
                _productService.GetAreas(),
                _productService.GetData(productId),
                 _productService.TagList()
                );
            return PartialView("_edit", model);
        }
        [HttpPost]
        public JsonResult Create(ProductDto data)
        {
            try
            {
                if (_productService.Insert(data))
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
        public JsonResult Update(ProductDto data)
        {
            try
            {
                if (_productService.Update(data))
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
        public JsonResult Delete(Guid productId)
        {
            try
            {
                if (_productService.Delete(productId))
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
        public JsonResult UpdateStatu(Guid productId, bool ischecked)
        {
            try
            {
                if (_productService.UpdateStatu(productId, ischecked))
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
        public JsonResult UpdateHero(Guid productId, bool ischecked)
        {
            try
            {
                if (_productService.UpdateHero(productId, ischecked, this.User?.Identity.Name))
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
