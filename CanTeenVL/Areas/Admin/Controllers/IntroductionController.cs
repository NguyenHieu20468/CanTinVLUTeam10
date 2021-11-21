using CanTeenVL.Areas.Admin.Middleware;
using CanTeenVL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    public class IntroductionController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        private const string PICTURE_PATH = "~/Upload/Introduction/";
        // GET: Admin/Introduction
        [LoginVerification]
        public ActionResult Index()
        {
            var introduction = model.INTRODUCTIONs.ToList().OrderByDescending(x => x.ID).ToList();
            return View(introduction);
        }
        [AllowAnonymous]
        public ActionResult IndexCus()
        {
            var introduction = model.INTRODUCTIONs.ToList().OrderByDescending(x => x.ID).ToList();
            return View(introduction);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        [LoginVerification]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false),LoginVerification]
        public ActionResult Create(INTRODUCTION i, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var introduction = new INTRODUCTION();
                if (picture != null)
                {
                    if (ModelState.IsValid)
                    {
                        using (var scope = new TransactionScope())
                        {
                            introduction.CONTENT = i.CONTENT;
                            model.INTRODUCTIONs.Add(introduction);
                            model.SaveChanges();

                            var path = Server.MapPath(PICTURE_PATH);
                            picture.SaveAs(path + introduction.ID);
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
        [LoginVerification]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = model.INTRODUCTIONs.FirstOrDefault(x => x.ID == id);
            //ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        [LoginVerification]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, INTRODUCTION i, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var product = model.INTRODUCTIONs.FirstOrDefault(x => x.ID == id);
                using (var scope = new TransactionScope())
                {
                    product.CONTENT = i.CONTENT;
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
        [LoginVerification]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var introduction = model.INTRODUCTIONs.FirstOrDefault(x => x.ID == id);
            return View(introduction);
        }
        [LoginVerification]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var introduction = model.INTRODUCTIONs.FirstOrDefault(x => x.ID == id);
            model.INTRODUCTIONs.Remove(introduction);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}