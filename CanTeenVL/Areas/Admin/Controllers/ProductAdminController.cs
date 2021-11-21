using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using CanTeenVL.Models;
using System.Data.Entity;
using CanTeenVL.Areas.Admin.Middleware;

namespace CanTeenVL.Areas.Admin.Controllers
{
    [LoginVerification]
    public class ProductAdminController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        // GET: Admin/ProductAdmin
        public ActionResult Index()
        {
            var product = model.FOODs.ToList().OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = model.FOODs.FirstOrDefault(x => x.ID == id);
            ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id , FOOD f, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var product = model.FOODs.FirstOrDefault(x => x.ID == id);
                using (var scope = new TransactionScope())
                {
                    product.FOOD_CODE = f.FOOD_CODE;
                    product.FOOD_NAME = f.FOOD_NAME;
                    product.DESCRIPTION = f.DESCRIPTION;
                    product.CATEGORY = f.CATEGORY;
                    product.PRICE = f.PRICE;
                    product.STATUS = f.STATUS;
                    model.SaveChanges();

                    if (picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + product.ID);
                    }
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FOOD f , HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var product = new FOOD();
                if (picture != null)
                {
                        using (var scope = new TransactionScope())
                        {
                            product.FOOD_CODE = f.FOOD_CODE;
                            product.FOOD_NAME = f.FOOD_NAME;
                            product.DESCRIPTION = f.DESCRIPTION;
                            product.PRICE = f.PRICE;
                            product.STATUS = f.STATUS;
                            product.CATEGORY_ID = f.CATEGORY_ID;
                            model.FOODs.Add(product);
                            model.SaveChanges();

                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + product.ID);
                            scope.Complete();
                        }
                }
                    else
                {
                    ModelState.AddModelError("", "Picture not found!");
                }
                return RedirectToAction("Index");
            }
            return View();
        }   
public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/Upload/Products/";
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = model.FOODs.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var product = model.FOODs.FirstOrDefault(x => x.ID == id);
            model.FOODs.Remove(product);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet, ValidateInput(false)]
        public ActionResult Detail(int id)
        {
            var product = model.FOODs.FirstOrDefault(x => x.ID == id);
            return View(product); 
        }
    }
}