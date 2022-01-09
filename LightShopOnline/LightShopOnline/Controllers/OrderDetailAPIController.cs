using LightShopOnline.Areas.admin.Data;
using LightShopOnline.Areas.admin.Helpers;
using LightShopOnline.Areas.admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LightShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailAPIController : Controller
    {
        private readonly ShopContext _db = new ShopContext();
        // GET: api/<OrderDetailAPIController>
        [HttpGet]
        public JsonResult Get()
        {
            var session = HttpContext.Session;
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");
            return Json(cart);
        }

        // POST api/<OrderDetailAPIController>
        [HttpPost("{id}")]
        public IActionResult Buy(int id, IFormCollection collection)
        {
            // get quantity
            int quantity = string.IsNullOrEmpty(collection["quantity"].ToString()) ? 1 : int.Parse(collection["quantity"].ToString());
            quantity = (quantity < 1) ? 1 : quantity; // filer negative number

            // get session
            var session = HttpContext.Session;

            if (session.GetString("cart") == null)
            {// no session cart
                //create cart
                Cart cart = new Cart();

                // get product
                Product product = GetProduct(id);
                if (product == null)
                {// invalid product id
                    return StatusCode(400);
                }

                // valid product id
                cart.OrderDetails.Add(new OrderDetail {Product_Id = product.Product_Id, Quantity = quantity });

                // save obj 
                SessionHelper.SetObjectAsJson(session, "cart", cart);

                //success
                return Ok(cart.OrderDetails.Count);
                
            }
            else
            {// already have session cart
                Cart cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");

                // check if product id in cart
                OrderDetail orderDetail = cart.OrderDetails.FirstOrDefault<OrderDetail>(od => od.Product_Id == id);

                if (orderDetail == null)
                {// product is not in cart => add new product to cart

                    // get product
                    Product product = GetProduct(id);
                    if (product == null)
                    {// invalid product id
                        return StatusCode(400);
                    }

                    // valid product id
                    cart.OrderDetails.Add(new OrderDetail {Product_Id = product.Product_Id, Quantity = quantity });

                }
                else
                {// product is already in cart => add quantity
                    orderDetail.Quantity = orderDetail.Quantity + quantity;
                }
                // save obj 
                SessionHelper.SetObjectAsJson(session, "cart", cart);

                //success
                return Ok(cart.OrderDetails.Count);
            }
        }

        // DELETE api/<OrderDetailAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            //List<OrderDetail> cart = (List<OrderDetail>)Session["cart"];
            //int index = isExist(id);
            //cart.RemoveAt(index);
            //Session["cart"] = cart;

            // get session
            var session = HttpContext.Session;
            if (session.GetString("cart") == null)
            {// no session cart
                return StatusCode(400);
            }

            Cart cart = SessionHelper.GetObjectFromJson<Cart>(session, "cart");

            OrderDetail orderDetail = cart.OrderDetails.FirstOrDefault(od => od.Product_Id == id);
            if (orderDetail == null)
            {// no orderdetail found
                return StatusCode(400);
            }
            cart.OrderDetails.Remove(orderDetail);

            // save obj 
            SessionHelper.SetObjectAsJson(session, "cart", cart);

            return Ok("Deleted");
        }

        private Product GetProduct (int id)
        {
            // create relate data object 
            Product product = _db.Products
                                .Select(o => new Product
                                {
                                    Product_Id = o.Product_Id,
                                })
                                .Where(c => c.Product_Id == id)
                                .FirstOrDefault();
            return product; // null or product
        }
    }
}
