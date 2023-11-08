using prjASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjASPNET.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            IEnumerable<tProduct> datas = null;
            dbDemoEntities db = new dbDemoEntities();
            if (string.IsNullOrEmpty(keyword))
                datas = from t in db.tProduct
                        select t;
            else
                datas = db.tProduct.Where(_=>_.fName.Contains(keyword));
           
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            db.tProduct.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            dbDemoEntities db = new dbDemoEntities();
            var item = (from t in db.tProduct
                        where t.fId == id
                        select t).FirstOrDefault();
            db.tProduct.Remove(item);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct x = db.tProduct.FirstOrDefault(p => p.fId == id);
            if (x == null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(tProduct p)
        {
            dbDemoEntities db = new dbDemoEntities();
            var item = (from t in db.tProduct
                        where t.fId == p.fId
                        select t).FirstOrDefault();
            if(p.photo != null)
            {
                string photoName=Guid.NewGuid().ToString()+".jpg";
                item.fImagePath = photoName;
                p.photo.SaveAs(Server.MapPath("../../Images/" + photoName));
            }
            if(item!= null)
            {
                item.fId = p.fId;
                item.fName = p.fName;
                item.fQty = p.fQty;
                item.fCost = p.fCost;
                item.fPrice = p.fPrice;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}