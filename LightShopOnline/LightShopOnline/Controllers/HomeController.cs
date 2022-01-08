using LightShopOnline.Models;
using LightShopOnline.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lstHomeProduct = ProductRes.GetAll();
            return View(ViewBag.lstHomeProduct = lstHomeProduct);
        }

        public IActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Account([Bind] UserTable account)
        {
            int res = AccountRes.LoginCheck(account);
            if (res == 1)
            {
                return Redirect("/Admin");
            }
            else
            {
                TempData["msg"] = "Username or Password is wrong!";
                return Redirect("/Account");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
