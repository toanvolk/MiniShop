using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Area("admin")]
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
            //var model = _productService.LoadDatas(rootproductType, true);
            var model = _productService.LoadData();
            var response = new DataResponeCommon<ICollection<ProductDto>>()
            {
                Data = model,
                Message = "OK",
                Statu = StatuCodeEnum.OK
            };
            return Json(response);
        }
        [HttpPost]
        public IActionResult Add()
        {
            return PartialView("_add");
        }
        [HttpPost]
        public IActionResult Edit(Guid productId)
        {
            var entity = _productService.GetData(productId);
            return PartialView("_edit", entity);
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
    }
}
