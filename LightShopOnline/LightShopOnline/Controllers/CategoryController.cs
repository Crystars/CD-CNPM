using LightShopOnline.Repositories;
using LightShopOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index(String categoryURL)
        {
            try
            {
                string[] categoryURLTokens = categoryURL.Split('/');
                int pageNum = 1;
                int.TryParse(categoryURLTokens[1], out pageNum);
                pageNum = pageNum < 1 ? 1 : pageNum;
                int productPerPage = 12;
                int beginRow = (pageNum - 1) * productPerPage;
                List<Product> lstProduct = CategoryProductRes.GetProductByPage(categoryURLTokens[0], beginRow, productPerPage);
                int sumProducts = CategoryProductRes.CountSumProduct(categoryURLTokens[0]);
                int maxPage = (sumProducts / productPerPage) <= 0 ? 1 : sumProducts / productPerPage;
                if (pageNum > maxPage) return Redirect("/");
                ViewBag.lstProduct = lstProduct;
                ViewBag.currCateURL = categoryURLTokens[0];
                ViewBag.currPage = pageNum;
                ViewBag.maxPage = maxPage;

                ViewBag.beginProduct = beginRow+1;
                ViewBag.endProduct = beginRow + lstProduct.Count;
                ViewBag.sumProduct = sumProducts;
                return View();
            }
            catch
            {
                return Redirect("/");
            }
        }

        
    }
}
