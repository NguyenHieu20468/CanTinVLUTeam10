using CanTeenVL.Areas.Admin.Middleware;
using CanTeenVL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    [LoginVerification1]
    public class BillController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();

        private List<ORDER_DETAIL> ShoppingCart = null;
        private void GetShoppingCart()
        {
            if (Session["ShoppingCart"] != null)
                ShoppingCart = Session["ShoppingCart"] as List<ORDER_DETAIL>;
            else
            {
                ShoppingCart = new List<ORDER_DETAIL>();
                Session["ShoppingCart"] = ShoppingCart;
            }
        }
        // GET: Admin/Bill
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetShoppingCart();
            ViewBag.Cart = ShoppingCart;
            ViewBag.product_type = model.STATUS_BILL.OrderBy(x => x.ID).ToList();
            ViewBag.product_type1 = model.CUSTOMERs.OrderByDescending(x => x.ID).ToList();
            ViewBag.product_type2 = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            return View();
        }
        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Create(ORDER o)
        {
            GetShoppingCart();
            ValidateBill(o);
            if (ModelState.IsValid)
            {
                var order = new ORDER();
                order.ORDER_CODE = o.ORDER_CODE;
                order.ACCOUNT_ID = o.ACCOUNT_ID;
                order.STATUSBILL_ID = o.STATUSBILL_ID;
                order.CUSTOMER_ID = o.CUSTOMER_ID;
                order.FEEDBACK = o.FEEDBACK;
                order.DATE = DateTime.Now;
                model.ORDERs.Add(order);
                model.SaveChanges();
                foreach (var item in ShoppingCart)
                {
                    model.ORDER_DETAIL.Add(new ORDER_DETAIL
                    {
                        ORDER_ID = order.ID,
                        FOOD_ID = item.FOOD.ID,
                        PRICE = item.FOOD.PRICE,
                        QUANTITY = item.QUANTITY,
                    });
                }
                model.SaveChanges();
                Session["ShoppingCart"] = null;
                return RedirectToAction("Index", "ProductCustomer");
            }
            return RedirectToAction("Index", "ProductCustomer");
        }

        private void ValidateBill(ORDER o)
        {
            var regex = new Regex(@"^\d[0-9]{3,}");
            var expression = new Regex("[a-zA-Z]{3}@gmail.com");
            GetShoppingCart();
            if (ShoppingCart.Count == 0)
            {
                ModelState.AddModelError("", "Không có sản phẩm trong giỏ hàng");
            }
        }
    }
}