using CanTeenVL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    public class ShoppingCartController : Controller
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
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/Upload/Products/";
        // GET: Admin/ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            var hashtable = new Hashtable();
            foreach (var billdetail in ShoppingCart)
            {
                if (hashtable[billdetail.FOOD.ID] != null)
                {
                    (hashtable[billdetail.FOOD.ID] as ORDER_DETAIL).QUANTITY += billdetail.QUANTITY;
                }
                else
                {
                    hashtable[billdetail.FOOD.ID] = billdetail;
                }
            }
            ShoppingCart.Clear();
            foreach (ORDER_DETAIL billdetail in hashtable.Values)
                ShoppingCart.Add(billdetail);
            return View(ShoppingCart);
        }
        [HttpPost]
        public ActionResult Create(int productId, int quantity)
        {
            GetShoppingCart();
            var product = model.FOODs.Find(productId);
            ShoppingCart.Add(new ORDER_DETAIL
            {
                FOOD = product,
                QUANTITY = quantity
            });
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(int[] product_id, int[] quantity)
        {
            GetShoppingCart();
            ShoppingCart.Clear();
            if (product_id != null)
                for (int i = 0; i < product_id.Length; i++)
                    if (quantity[i] > 0)
                    {
                        var product = model.FOODs.Find(product_id[i]);
                        ShoppingCart.Add(new ORDER_DETAIL
                        {
                            FOOD = product,
                            QUANTITY = quantity[i]
                        });
                    }
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            GetShoppingCart();
            ShoppingCart.Clear();
            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("Index");
        }
        public ActionResult DeleteId(int id)
        {
            List<ORDER_DETAIL> giohang = Session["ShoppingCart"] as List<ORDER_DETAIL>;
            ORDER_DETAIL itemXoa = giohang.FirstOrDefault(m => m.FOOD.ID == id);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        // POST: ShopingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GetShoppingCart();
            ORDER_DETAIL chiTietHD = model.ORDER_DETAIL.Find(id);
            model.ORDER_DETAIL.Remove(chiTietHD);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}