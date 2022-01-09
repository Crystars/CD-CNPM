using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Models;
using Microsoft.AspNetCore.Hosting;
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
                product.url = product.url.Replace(' ', '-');
                product.url = product.url.Replace('/', '-');
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
                else if (product.Price == 0.0)
                {
                    ModelState.AddModelError(nameof(Product.Price), "Product price must be larger than 0");
                }
                else if (product.Discount == 0.0)
                {
                    ModelState.AddModelError(nameof(Product.Discount), "Product Discount must be larger than 0");
                }
                else if (tempProduct != null)
                {
                    ModelState.AddModelError(nameof(Category.url), "Url already exist");
                }
                else if (ModelState.IsValid)
                {
                    if (Image != null)
                    {// if upload image
                        product.Picture1 = await SummerExController.SaveImage(Image, _appEnvironment.WebRootPath);
                    }
                    
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
        public async Task<ActionResult> Edit(int id)
        {
            // check if id input available
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            //get product
            Product product = await _db.Products
                                .FindAsync(id);
            if (product == null)
            {
                // if not found any product -> redirect to product Index
                return RedirectToAction(nameof(Index));
            }
            // get selected category
            Category_Product category_Product = await _db.Entry(product)
                .Collection(c => c.Category_Product)
                .Query()
                .FirstOrDefaultAsync();
            Category selectedCat = await _db.Categories
                                    .Select(o => new Category
                                    {
                                        Category_Id = o.Category_Id,
                                    })
                                    .Where(c => c.Category_Id == category_Product.Category_Id)
                                    .FirstOrDefaultAsync();
            ViewBag.SelectedCatId = selectedCat.Category_Id;

            // get domain url
            ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            // get category list
            List<Category> cate = await _db.Categories
                                    .Select(o => new Category
                                    {
                                        Category_Id = o.Category_Id,
                                        Category_Name = o.Category_Name
                                    })
                                    .ToListAsync();

            ViewBag.CategoryList = cate;
            // render view
            return View(product);

        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,Product product, IFormCollection collection, IFormFile Image)
        {
            try
            {
                // check new cat url is duplicate or not
                product.url = product.url.Replace(' ', '-');
                product.url = product.url.Replace('/', '-');

                Product tempProduct = await _db.Products
                                .FirstOrDefaultAsync(c => c.url == product.url && c.Product_Id != product.Product_Id);
                if (product.Product_Name == null || product.Product_Name.Length < 3)
                {
                    ModelState.AddModelError(nameof(Product.Product_Name), "Product Name must have at least 3 characters");
                }
                else if (product.url == null || product.url.Length < 1)
                {
                    ModelState.AddModelError(nameof(Product.url), "Product url must have at least 1 character");
                }
                else if (product.Price == 0.0)
                {
                    ModelState.AddModelError(nameof(Product.Price), "Product price must be larger than 0");
                }
                else if (product.Discount == 0.0)
                {
                    ModelState.AddModelError(nameof(Product.Discount), "Product Discount must be larger than 0");
                }
                else if (tempProduct != null)
                {
                    ModelState.AddModelError(nameof(Category.url), "Url already exist");
                }
                else if (ModelState.IsValid)
                {
                    if (Image != null)
                    {// if upload image
                        product.Picture1 = await SummerExController.SaveImage(Image, _appEnvironment.WebRootPath);
                    }

                    // update product
                    _db.Entry(product).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    // update product-category
                    int catId = string.IsNullOrEmpty(collection["category"].ToString()) ? -1 : int.Parse(collection["category"].ToString());


                    // check if catid change
                    Category_Product category_Product = _db.Category_Product
                                                    .FirstOrDefault(c => c.Product_Id == product.Product_Id);

                    if (category_Product.Category_Id != catId)
                    {// cat id change

                        // delete old
                        _db.Category_Product.Remove(category_Product);
                        await _db.SaveChangesAsync();
                        //create new

                        Category category = await _db.Categories.FindAsync(catId);
                        Category_Product new_category_Product = new Category_Product { Category = category, Product = product };
                        _db.Category_Product.Add(new_category_Product);
                        await _db.SaveChangesAsync();
                    }

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

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // check if id input available
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            //get product
            Product product = await _db.Products
                                .FindAsync(id);
            if (product == null)
            {
                // if not found any product -> redirect to product Index
                return RedirectToAction(nameof(Index));
            }
            
            // get domain url
            ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            
            // render view
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // Safe delete product
                Product product = _db.Products.Find(id);
                product.isHidden = 1;
                await _db.SaveChangesAsync();
                // delete category_product link
                _db.Entry(product).State = EntityState.Modified;
                Category_Product category_Product = _db.Category_Product
                                                    .FirstOrDefault(c => c.Product_Id == id);

                _db.Category_Product.Remove(category_Product);

                await _db.SaveChangesAsync();
                // return to product Index
                return RedirectToAction("Index");
            }
            catch // any error redirect to Product Index
            {
                return RedirectToAction(nameof(Index));
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
