using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniShop.App;

namespace MiniShop.Web.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult LoadData()
        {
            //var model = _categoryService.LoadDatas(rootCategoryType, true);
            var model = new List<CategoryDto>();
            var response = new DataResponeCommon<List<CategoryDto>>()
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
    }
}
