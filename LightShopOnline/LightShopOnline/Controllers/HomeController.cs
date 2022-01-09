﻿using LightShopOnline.Models;
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

        // Trả về trang chủ cùng với danh sách tất cả các sản phẩm
        public IActionResult Index()
        {
            CategoryRes categoryRes = new CategoryRes();
            ViewBag.Category = CategoryRes.GetAll();

            var lstHomeProduct = ProductRes.GetAll();
            return View(ViewBag.lstHomeProduct = lstHomeProduct);
        }

        public IActionResult Privacy()
        {
            CategoryRes categoryRes = new CategoryRes();
            ViewBag.Category = CategoryRes.GetAll();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
