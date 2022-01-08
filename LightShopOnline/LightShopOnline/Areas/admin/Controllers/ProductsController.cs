using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Controllers
{
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly ShopContext _db = new ShopContext();
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductsController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

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


        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            List<Category> cate = await _db.Categories
                                        .Select(o => new Category
                                        {
                                            Category_Id = o.Category_Id,
                                            Category_Name = o.Category_Name
                                        })
                                        .ToListAsync();
            ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            ViewBag.CategoryList = cate;

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormCollection collection, IFormFile Image)
        {
            try
            {
                // check new cat url is duplicate or not
                Product tempProduct = await _db.Products
                                .FirstOrDefaultAsync(c => c.url == product.url);
                if (product.Product_Name == null || product.Product_Name.Length < 3)
                {
                    ModelState.AddModelError(nameof(Product.Product_Name), "Product Name must have at least 3 characters");
                }
                else if (product.url == null || product.url.Length < 1)
                {
                    ModelState.AddModelError(nameof(Product.url), "Product url must have at least 1 character");
                }
                else if (tempProduct != null)
                {
                    ModelState.AddModelError(nameof(Category.url), "Url already exist");
                }
                else if (ModelState.IsValid)
                {
                    product.url = product.url.Replace(' ', '-');
                    product.url = product.url.Replace('/', '-');
                    product.Picture1 = await SummerExController.SaveImage(Image, _appEnvironment.WebRootPath);
                    // create product
                    _db.Products.Add(product);

                    // create product-category
                    int catId = string.IsNullOrEmpty(collection["category"].ToString()) ? -1 : int.Parse(collection["category"].ToString());
                    Category category = await _db.Categories.FindAsync(catId);
                    Category_Product category_Product = new Category_Product { Category = category, Product = product };
                    _db.Category_Product.Add(category_Product);
                    await _db.SaveChangesAsync();

                   
                    // redirect to Category Index
                    return RedirectToAction(nameof(Index));
                }

                // render create view with error
                List<Category> cate = await _db.Categories
                                        .Select(o => new Category
                                        {
                                            Category_Id = o.Category_Id,
                                            Category_Name = o.Category_Name
                                        })
                                        .ToListAsync();

                ViewBag.CategoryList = cate;
                ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

                return View(product);
            }
            catch // any error redirect to Product Index
            {
                return RedirectToAction(nameof(Index));
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
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult RealDelete(int id, IFormCollection collection)
        {
            try
            {
                Product product = _db.Products.Find(id);
                Category_Product category_Product = _db.Category_Product
                                                    .FirstOrDefault(c => c.Product_Id == id);

                _db.Category_Product.Remove(category_Product);
                _db.Products.Remove(product);
                _db.SaveChanges();
                return Json("OK");
            }
            catch
            {
                return Json("Error");
            }
        }
    }
}
