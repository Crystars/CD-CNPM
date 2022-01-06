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
                // get domain url
                ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                
                // get list categories
                var listCat = from c in _db.Categories
                              where c.isHidden == 0
                              select c;
                return View(await listCat.ToListAsync());
            }
            catch // any error redirect to Home Index
            {
                return Redirect("/");
            }
        }

        // GET: CategoriesController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                // check new cat url is duplicate or not
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
                    // create 
                    _db.Categories.Add(category);
                    _db.SaveChanges();

                    // redirect to Category Index
                    return RedirectToAction(nameof(Index));
                }
                // render create view with error
                return View(category);
            }
            catch // any error redirect to Category Index
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // check if id input available
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // get domain url
            ViewBag.GuestHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            
            // get category
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                // if not found any category -> redirect to Category Index
                return RedirectToAction(nameof(Index));
            }

            // render view
            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            try
            {
                // find if edit category url duplicate or not (exclude url not change)
                Category tempCat = await _db.Categories
                                .FirstOrDefaultAsync(c => c.url == category.url && c.Category_Id != category.Category_Id);
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
                    // update category
                    _db.Entry(category).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    // return to index on success
                    return RedirectToAction("Index");
                }

                // render edit view with error
                return View(category);
            }
            catch // any error redirect to Category Index
            {
                return RedirectToAction("Index");
            }

            
        }

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // check if id input avalable
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // get category
            Category category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                // return to Category Index if not found category
                return RedirectToAction(nameof(Index));
            }

            // render category detail
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Safe delete category
            Category category = _db.Categories.Find(id);
            category.isHidden = 1;
            _db.Entry(category).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            // return to category Index
            return RedirectToAction("Index");
        }
    }
}
