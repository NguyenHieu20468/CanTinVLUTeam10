using CanTeenVL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanTeenVL.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        AD1Team3Entities model = new AD1Team3Entities();
        // GET: Admin/Customer
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = model.CUSTOMERs.FirstOrDefault(u => u.EMAIL.Equals(email));
            if (user != null)
            {
                if (user.PASSWORD.Equals(password))
                {
                    Session["user-fullnamec"] = user.FULL_NAME;
                    Session["user-idc"] = user.ID;
                    Session["user-phonec"] = user.PHONE_NUMBER;
                    Session["user-addressc"] = user.ADDRESS;
                    Session["user-emailc"] = user.EMAIL;
                    return RedirectToAction("Index", "ProductCustomer");

                    //return View();
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
        [HttpGet]
        public ActionResult Login1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login1(string email, string password)
        {
            var user = model.CUSTOMERs.FirstOrDefault(u => u.EMAIL.Equals(email));
            if (user != null)
            {
                if (user.PASSWORD.Equals(password))
                {
                    Session["user-fullnamec"] = user.FULL_NAME;
                    Session["user-idc"] = user.ID;
                    Session["user-phonec"] = user.PHONE_NUMBER;
                    Session["user-addressc"] = user.ADDRESS;
                    Session["user-emailc"] = user.EMAIL;
                    return RedirectToAction("Create", "Bill");

                    //return View();
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
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(CUSTOMER c)
        {
            var customer = new CUSTOMER();
            if (ModelState.IsValid)
            {
                var check = model.CUSTOMERs.FirstOrDefault(u => u.EMAIL == c.EMAIL);
                if(check == null)
                {
                    customer.EMAIL = c.EMAIL;
                    customer.ADDRESS = c.ADDRESS;
                    customer.FULL_NAME = c.FULL_NAME;
                    customer.PASSWORD = c.PASSWORD;
                    customer.PHONE_NUMBER = c.PHONE_NUMBER;
                    model.CUSTOMERs.Add(customer);
                    model.SaveChanges();
                    return RedirectToAction("Login", "Customer");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return RedirectToAction("Login", "Customer");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "ProductCustomer");
        }
    }
}