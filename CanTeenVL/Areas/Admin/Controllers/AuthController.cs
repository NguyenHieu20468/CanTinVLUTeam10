using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanTeenVL.Models;

namespace CanTeenVL.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            Session["password-incorrect"] = false;
            Session["user-not-found"] = false;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = model.ACCOUNTs.FirstOrDefault(u => u.EMAIL.Equals(email));
            if(user != null)
            {
                if (user.PASSWORD.Equals(password))
                {
                    Session["user-fullname"] = user.FULL_NAME;
                    Session["user-id"] = user.ID;
                    Session["user-role"] = user.ROLE;
                    return RedirectToAction("Index", "CategoryAdmin");
                }
                else
                {
                    Session["password-incorrect"] = true;
                    return View();
                }
            }
            else
            {
                Session["user-not-found"] = true;
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["user-fullname"] = null;
            Session["user-id"] = null;
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Index()
        {
            var admin = model.ACCOUNTs.OrderBy(a => a.ID).ToList();
            return View(admin);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var account = model.ACCOUNTs.FirstOrDefault(x => x.ID == id);
            return View(account);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, ACCOUNT s)
        {
            var account = model.ACCOUNTs.FirstOrDefault(x => x.ID == id);
            if (ModelState.IsValid)
            {
                account.EMAIL = s.EMAIL;
                account.PASSWORD = s.PASSWORD;
                account.FULL_NAME = s.FULL_NAME;
                account.STATUS = s.STATUS;
                account.ROLE = s.ROLE;
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ACCOUNT s)
        {
            if (ModelState.IsValid)
            {
                var check = model.ACCOUNTs.FirstOrDefault(u => u.EMAIL == s.EMAIL);
                if(check == null)
                {
                    var account = new ACCOUNT();
                    account.EMAIL = s.EMAIL;
                    account.FULL_NAME = s.FULL_NAME;
                    account.PASSWORD = s.PASSWORD;
                    account.ROLE = s.ROLE;
                    account.STATUS = s.STATUS;
                    model.ACCOUNTs.Add(account);
                    model.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error1 = "Email already exists";
                    return View();
                }    
            }
            return RedirectToAction("Index", "Auth");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var account = model.ACCOUNTs.FirstOrDefault(x => x.ID == id);
            return View(account);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var account = model.ACCOUNTs.FirstOrDefault(x => x.ID == id);
            model.ACCOUNTs.Remove(account);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}