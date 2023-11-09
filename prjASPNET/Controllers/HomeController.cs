using prjASPNET.Models;
using prjASPNET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjASPNET.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session[CDictionary.SK_LIGINED_USER] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
            CCustomer user = (new CCustomerFactory()).queryByEmail(vm.txtAccount);
            if (user != null)
            {
                if (user.fPassword.Equals(vm.txtPassword))
                {
                    Session[CDictionary.SK_LIGINED_USER] = user;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}