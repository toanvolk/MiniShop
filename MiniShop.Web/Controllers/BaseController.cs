using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiniShop.App;

namespace MiniShop.Web.Controllers
{    
    public class BaseController : Controller
    {
        private readonly IBaseService _baseService;

        public BaseController(IBaseService baseService)
        {
            _baseService = baseService;
        }
        [HttpPost]
        public void CountClick(string userHostAddress = "", string url = "", string keyView = "")
        {
            if(userHostAddress == "") userHostAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            if(url == "") url = Request.Path.Value;
            _baseService.CountClick(userHostAddress, url, keyView);
        }
    }
}
