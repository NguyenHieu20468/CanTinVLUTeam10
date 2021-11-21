using CanTeenVL.Areas.Admin.Middleware;
using CanTeenVL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    [LoginVerification]
    public class StatusBillController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        // GET: Admin/StatusBill
        public ActionResult Index()
        {
            var status = model.STATUS_BILL.ToList().OrderByDescending(x => x.ID).ToList();
            return View(status);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(STATUS_BILL s)
        {
            if (ModelState.IsValid)
            {
                var status = new STATUS_BILL();
                status.NAME = s.NAME;
                model.STATUS_BILL.Add(status);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var status = model.STATUS_BILL.FirstOrDefault(x => x.ID == id);
            return View(status);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, STATUS_BILL s)
        {
            var status = model.STATUS_BILL.FirstOrDefault(x => x.ID == id);
            if (ModelState.IsValid)
            {
                status.NAME = s.NAME;
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var status = model.STATUS_BILL.FirstOrDefault(x => x.ID == id);
            return View(status);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var status = model.STATUS_BILL.FirstOrDefault(x => x.ID == id);
            model.STATUS_BILL.Remove(status);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}