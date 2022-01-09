using LightShopOnline.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            CategoryRes categoryRes = new CategoryRes();
            ViewBag.Category = CategoryRes.GetAll();
            return View();
        }

        // GET: ProductController/san-pham/
        public ActionResult Details(String productURL)
        {
            try {
                // Phương thức Dẫn đến trang chi tiết sản phẩm
                // Sử dụng cú pháp: Tên host/san-pham/Url của Product
                // Ví dụ: localhost:8888/san-pham/2
                string[] productURLTokens = productURL.Split('/');
                var productDetail = ProductRes.GetDetailByURL(productURLTokens[0]);
                // Nếu thỏa các điều kiện thì dẫn đến trang chi tiết
                CategoryRes categoryRes = new CategoryRes();
                ViewBag.Category = CategoryRes.GetAll();
                return View(productDetail);
            } catch {
                // Nếu xảy ra lỗi thì trở về trang chủ
                return Redirect("/");
            }
        }

        
    }
}
