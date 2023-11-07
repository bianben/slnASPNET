﻿using prjASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjASPNET.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> datas = null;
            if (string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactory()).queryAll();
            else
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = (new CCustomerFactory()).queryById((int)id);
            if(x== null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Update(CCustomer x)
        {
            (new CCustomerFactory()).update(x);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete(id);
            return RedirectToAction("List");
        }
        
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x= new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }
    }
}