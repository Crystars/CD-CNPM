using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly ShopContext _db = new ShopContext();
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var listCat = from c in _db.Categories
                              where c.isHidden == 0
                              select c;
                return View(await listCat.ToListAsync());
            }
            catch
            {
                return Redirect("/");
            }
        }

        // GET: CategoriesController/Create
        public async Task<ActionResult> Create()
        {
            List<Category> cate = await _db.Categories
                                        .Where(x => x.Category_Id == 1 || x.Category_Id == 2 || x.Category_Id == 9 || x.Category_Id == 11)
                                        .Select(o => new Category
                                        {
                                            Category_Id = o.Category_Id,
                                            Category_Name = o.Category_Name
                                        })
                                        .ToListAsync();

            ViewBag.CategoryList = cate;

            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                Category tempCat = await _db.Categories
                                .FirstOrDefaultAsync(c => c.url == category.url);
                if (category.Category_Name == null || category.Category_Name.Length < 3)
                {
                    ModelState.AddModelError(nameof(Category.Category_Name), "Caterogy Name must have at least 3 characters");
                }
                else if (category.url == null || category.url.Length < 1)
                {
                    ModelState.AddModelError(nameof(Category.url), "Category url must have at least than 1 character");
                }
                else if (tempCat != null)
                {
                    ModelState.AddModelError(nameof(Category.url), "Url already exist");
                }
                else if (ModelState.IsValid)
                {
                    _db.Categories.Add(category);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                Category tempCat = await _db.Categories
                                .FirstOrDefaultAsync(c => c.url == category.url || c.Category_Id != category.Category_Id);
                if (category.Category_Name == null || category.Category_Name.Length < 3)
                {
                    ModelState.AddModelError(nameof(Category.Category_Name), "Caterogy Name must have at least 3 characters");
                }
                else if (category.url == null || category.url.Length < 1)
                {
                    ModelState.AddModelError(nameof(Category.url), "Category url must have at least than 1 character");
                }
                else if (tempCat != null)
                {
                    ModelState.AddModelError(nameof(Category.url), "Url already exist");
                }
                else if (ModelState.IsValid)
                {
                    _db.Entry(category).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index"); // success
                }
                return View(category); // error in form
            }
            catch
            {
                return RedirectToAction("Index"); // error function
            }

            
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = _db.Categories.Find(id);
            category.isHidden = 1;
            _db.Entry(category).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
