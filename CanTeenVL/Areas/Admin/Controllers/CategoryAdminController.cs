using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using CanTeenVL.Areas.Admin.Middleware;
using CanTeenVL.Models;

namespace CanTeenVL.Areas.Admin.Controllers
{
    [LoginVerification]
    public class CategoryAdminController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        private const string PICTURE_PATH = "~/Upload/Categories/";
        // GET: Admin/CategoryAdmin
        public ActionResult Index()
        {
            var product = model.CATEGORies.ToList().OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            //ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(int id, CATEGORY c, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var product = model.CATEGORies.FirstOrDefault(x => x.ID == id);
                using (var scope = new TransactionScope())
                {
                    product.CATEGORY_CODE = c.CATEGORY_CODE;
                    product.CATEGORY_NAME = c.CATEGORY_NAME;
                    product.STATUS = c.STATUS;
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

        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CATEGORY c, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var product = new CATEGORY();
                if (picture != null)
                {
                    if (ModelState.IsValid)
                    {
                        using (var scope = new TransactionScope())
                        {
                            product.CATEGORY_CODE = c.CATEGORY_CODE;
                            product.CATEGORY_NAME = c.CATEGORY_NAME;
                            product.STATUS = c.STATUS;
                            model.CATEGORies.Add(product);
                            model.SaveChanges();

                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + product.ID);
                            scope.Complete();
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Picture not found!");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var product = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            model.CATEGORies.Remove(product);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = model.CATEGORies.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
    }
}