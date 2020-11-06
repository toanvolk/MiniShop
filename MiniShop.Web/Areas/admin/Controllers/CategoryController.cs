using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private ILogger<CategoryController> _logger { get; set; }
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Category";
            return View();
        }
        public JsonResult LoadData()
        {
            //var model = _categoryService.LoadDatas(rootCategoryType, true);
            var model = _categoryService.LoadData();
            var response = new DataResponeCommon<ICollection<CategoryDto>>()
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
        public IActionResult Edit(Guid categoryId)
        {
            var entity = _categoryService.GetData(categoryId);
            return PartialView("_edit", entity);
        }
        [HttpPost]
        public JsonResult Create(CategoryDto data)
        {
            try
            {
                if (_categoryService.Insert(data))
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
        public JsonResult Update(CategoryDto data)
        {
            try
            {
                if (_categoryService.Update(data))
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
        public JsonResult Delete(Guid categoryId)
        {
            try
            {
                if (_categoryService.Delete(categoryId))
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
        public JsonResult UpdateStatu(Guid categoryId, bool ischecked)
        {
            try
            {
                if (_categoryService.UpdateStatu(categoryId, ischecked))
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
