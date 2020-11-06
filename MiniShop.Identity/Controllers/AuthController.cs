using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniShop.Identity.Controllers
{
    [Area("admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View("login",returnUrl);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] AuthUser authUser, [FromForm] string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var result = await _signInManager.PasswordSignInAsync(authUser.UserName,
                                   authUser.Password, authUser.RememberMe, lockoutOnFailure: true);
            var response = new DataResponeCommon<string>()
            {
                Data = "Sai thông tin tài khoản hoặc mật khẩu!",
                Message = "Đăng nhập không thành công",
                Statu = StatuCodeEnum.Unauthorized
            }; ;
            if (result.Succeeded)
            {
                response.Data = returnUrl;
                response.Message = "Đăng nhập thành công";
                response.Statu = StatuCodeEnum.OK;
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new
                {
                    ReturnUrl = returnUrl,
                    RememberMe = authUser.RememberMe
                });
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            
            return Json(response);
        }       
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectPermanent("/admin");
        }
    }
}
