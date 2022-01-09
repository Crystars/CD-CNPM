using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Helpers;
using LightShopOnline.Areas.admin.Models;
using LightShopOnline.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightShopOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _db = new ShopContext();

        // GET: CartController
        public async Task<IActionResult> Index()
        {// view cart
            CategoryRes categoryRes = new CategoryRes();
            ViewBag.Category = CategoryRes.GetAll();

            Cart cart = new Cart();
            ViewBag.Cart = cart;

            // get cart product list
            var session = HttpContext.Session;
            cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");
            if (cart == null)
            {// no session
                return Redirect("/");
            }

            
            // add product to product list
            foreach(OrderDetail tempOd in cart.OrderDetails)
            {
                Product tempProduct = await _db.Products
                                        .Select(o => new Product
                                        {
                                            Product_Id = o.Product_Id,
                                            Product_Name = o.Product_Name,
                                            Discount = o.Discount,
                                            Picture1 = o.Picture1,
                                        })
                                        .Where(p => p.Product_Id == tempOd.Product_Id)
                                        .FirstOrDefaultAsync();
                if (tempProduct != null)
                {
                    tempOd.Product = tempProduct;
                }
            }

            ViewBag.Cart = cart;
            return View();
        }

        // GET: invoice
        public async Task<IActionResult> Invoice()
        {// invoice
            CategoryRes categoryRes = new CategoryRes();
            ViewBag.Category = CategoryRes.GetAll();

            Cart cart = new Cart();

            List<double> subtotal = new List<double>();
            // get cart product list
            var session = HttpContext.Session;
            cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");
            if (cart == null)
            {// no session
                return RedirectToAction(nameof(Index));
            }

            // add product to product list
            foreach (OrderDetail tempOd in cart.OrderDetails)
            {
                Product tempProduct = await _db.Products
                                        .Select(o => new Product
                                        {
                                            Product_Id = o.Product_Id,
                                            Product_Name = o.Product_Name,
                                            Discount = o.Discount,
                                        })
                                        .Where(p => p.Product_Id == tempOd.Product_Id)
                                        .FirstOrDefaultAsync();
                if (tempProduct != null)
                {
                    tempOd.Product = tempProduct;
                    subtotal.Add((double)(tempOd.Quantity * tempProduct.Discount));
                }
            }

            ViewBag.subtotal = subtotal;
            ViewBag.total = subtotal.Sum();
            ViewBag.Cart = cart;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invoice(Order order)
        {
            try
            {
                Cart cart = new Cart();
                var session = HttpContext.Session;
                // check input value
                if (order.Guest_Name == null || order.Guest_Name.Length < 3)
                {
                    ModelState.AddModelError(nameof(order.Guest_Name), "Tên khách hàng phải dài hơn 3 kí tự");
                }
                else if (order.Guest_Phone == null || order.Guest_Phone.Length < 9 || order.Guest_Phone.Length > 11)
                {
                    ModelState.AddModelError(nameof(order.Guest_Phone), "Số điện thoại phải chứa 9-11 số");
                }
                else if (order.Address == null || order.Address.Length < 9)
                {
                    ModelState.AddModelError(nameof(order.Address), "Vui lòng nhập địa chỉ");
                }
                else if (ModelState.IsValid)
                {
                    double totalPrice = 0;
                    // get cart product list
                    cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");
                    if (cart == null)
                    {// no session
                        return RedirectToAction(nameof(Index));
                    }
                    order.Order_Id = DateTime.Now.ToString("MM/dd/yyyy-HH:mm:ss");
                    order.dateCreate = DateTime.Now;


                    // add product to product list
                    OrderDetailRes orderDetailRes = new OrderDetailRes();
                    foreach (OrderDetail tempOd in cart.OrderDetails)
                    {
                        Product tempProduct = await _db.Products
                                                .Select(o => new Product
                                                {
                                                    Product_Id = o.Product_Id,
                                                    Product_Name = o.Product_Name,
                                                    Discount = o.Discount,
                                                })
                                                .Where(p => p.Product_Id == tempOd.Product_Id)
                                                .FirstOrDefaultAsync();
                        if (tempProduct != null)
                        {
                            totalPrice = totalPrice + (double)(tempOd.Quantity * tempProduct.Discount);
                            tempOd.Order_Id = order.Order_Id;
                            tempOd.Product_Id = tempProduct.Product_Id;
                            orderDetailRes.Insert(tempOd);
                        }
                    }

                    order.paymentMethod = "Tiền mặt";
                    order.Price = (long)totalPrice;

                    OrderRes orderRes = new OrderRes();
                    orderRes.Insert(order);

                    foreach (OrderDetail tempOd in cart.OrderDetails)
                    {
                        Product tempProduct = await _db.Products
                                                .Select(o => new Product
                                                {
                                                    Product_Id = o.Product_Id,
                                                    Product_Name = o.Product_Name,
                                                    Discount = o.Discount,
                                                })
                                                .Where(p => p.Product_Id == tempOd.Product_Id)
                                                .FirstOrDefaultAsync();
                        if (tempProduct != null)
                        {
                            orderDetailRes.Insert(tempOd);
                        }
                    }

                    return await InvoiceDetail(order.Order_Id);
                }


                List<double> subtotal = new List<double>();
                // get cart product list
                cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");
                if (cart == null)
                {// no session
                    return RedirectToAction(nameof(Index));
                }

                // add product to product list
                foreach (OrderDetail tempOd in cart.OrderDetails)
                {
                    Product tempProduct = await _db.Products
                                            .Select(o => new Product
                                            {
                                                Product_Id = o.Product_Id,
                                                Product_Name = o.Product_Name,
                                                Discount = o.Discount,
                                            })
                                            .Where(p => p.Product_Id == tempOd.Product_Id)
                                            .FirstOrDefaultAsync();
                    if (tempProduct != null)
                    {
                        tempOd.Product = tempProduct;
                        subtotal.Add((double)(tempOd.Quantity * tempProduct.Discount));
                    }
                }

                ViewBag.subtotal = subtotal;
                ViewBag.total = subtotal.Sum();
                ViewBag.Cart = cart;

                return await Invoice();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: invoice
        public async Task<IActionResult> InvoiceDetail(string id)
        {// invoice
            
            try 
            {
                CategoryRes categoryRes = new CategoryRes();
                ViewBag.Category = CategoryRes.GetAll();
                // get order detail
                // check if id input available
                if (id == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                // get order
                OrderRes orderRes = new OrderRes();
                Order order = orderRes.getOrderbyOrderId(id);
                if (order == null)
                {
                    // if not found any category -> redirect to Category Index
                    return RedirectToAction(nameof(Index));
                }
                // get order detail
                OrderDetailRes orderDetailRes = new OrderDetailRes();
                order.OrderDetails = orderDetailRes.getOrderbyOrderId(order.Order_Id);
                
                // populate product
                foreach(OrderDetail tempOd in order.OrderDetails)
                {
                    Product tempProduct = await _db.Products
                                        .Select(o => new Product
                                        {
                                            Product_Id = o.Product_Id,
                                            Product_Name = o.Product_Name,
                                            Discount = o.Discount,
                                        })
                                        .Where(p => p.Product_Id == tempOd.Product_Id)
                                        .FirstOrDefaultAsync();
                    if (tempProduct != null)
                    {
                        tempOd.Product = tempProduct;
                    }
                }
                ViewBag.order = order;

                return View();
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
