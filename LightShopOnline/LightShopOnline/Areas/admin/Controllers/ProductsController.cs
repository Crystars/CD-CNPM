using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Controllers
{
    // Chưa đăng nhập thì không được truy cập
    [Authorize]
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly ShopContext _db = new ShopContext();
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            List<Product> result = await _db.Products
                .Where(o => o.isHidden == 0)
                .Select(o => new Product
                {
                    Product_Id = o.Product_Id,
                    Product_Name = o.Product_Name,
                    url = o.url,
                    Price = o.Price,
                    Warrant = o.Warrant,
                    Size = o.Size,
                    Color = o.Color,
                    Brand = o.Brand,
                    Picture1 = o.Picture1
                })
                .ToListAsync();
            return View(result);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
