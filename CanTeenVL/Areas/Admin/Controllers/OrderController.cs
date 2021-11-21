using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanTeenVL.Areas.Admin.Middleware;
using CanTeenVL.Models;

namespace CanTeenVL.Areas.Admin.Controllers
{
    [LoginVerification]
    public class OrderController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var order = model.ORDERs.OrderByDescending(x => x.ID).ToList();
            return View(order);
        }

        public ActionResult Print(int id)
        {
            var printData = model.ORDERs.FirstOrDefault(x => x.ID == id);
            return View(printData);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var editData = model.ORDERs.FirstOrDefault(x => x.ID == id);
            ViewBag.status_type = model.STATUS_BILL.OrderByDescending(x => x.ID).ToList();
            ViewBag.account_type = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            return View(editData);
        }
        [HttpPost]
        public ActionResult Edit(int id, ORDER o)
        {
            var order = model.ORDERs.FirstOrDefault(x => x.ID == id);
            order.STATUSBILL_ID = o.STATUSBILL_ID;
            order.ACCOUNT_ID = o.ACCOUNT_ID;
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}