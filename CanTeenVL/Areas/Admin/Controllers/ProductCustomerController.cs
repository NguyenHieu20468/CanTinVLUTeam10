using CanTeenVL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    public class ProductCustomerController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        [HttpGet]
        // GET: Admin/ProductCustomer
        public ActionResult Index()
        {
            var product = model.FOODs.ToList().OrderByDescending(x => x.ID).ToList();
            return View(product);
        }
        public ActionResult Picture(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }
        private const string PICTURE_PATH = "~/Upload/Products/";
        [HttpGet, ValidateInput(false)]
        public ActionResult Detail(int id)
        {
            var product = model.FOODs.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
    }
}