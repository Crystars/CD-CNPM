using LightShopOnline.Areas.admin.Models;
using LightShopOnline.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LightShopOnline.Controllers
{
    public class AccountController : Controller
    {
        // Máy client truy cập đến thì đưa ra form nhập liệu để
        // Cho phép họ được mở khóa quyền đăng nhập vào trang quản lý
        public IActionResult Login(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }

        // Gửi Form
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // Nếu không phải là quản trị viên thì đưa về trang Login
            if (!IsAuthenticated(model.Username, model.Password))
                return View();

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cookie authentication demo"),
                new Claim(ClaimTypes.Email, model.Username)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // Đăng nhập thành công
            await HttpContext.SignInAsync(
                    scheme: "DemoSecurityScheme",
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });

            return Redirect(model.RequestPath ?? "/Admin");
        }

        // Hàm gọi để Logout
        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "DemoSecurityScheme");
            // Nếu hàm chạy thành công thì sẽ điều hướng người dùng về trang Login
            return RedirectToAction("Login");
        }

        public IActionResult Access()
        {
            return View();
        }

        // Thiết lập cứng Tài khoản quản trị viên
        // Phương thức kiểm tra tài khoản quản trị có hợp lệ hay không
        private bool IsAuthenticated(string username, string password)
        {
            return (username == "admin" && password == "123456");
        }
    }
}
