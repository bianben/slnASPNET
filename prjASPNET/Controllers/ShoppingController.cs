using prjASPNET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjASPNET.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\1.ispan\class\13_ASPNET\hello.jpg");
            return View();
        }
        public ActionResult List()
        {
            IEnumerable<tProduct> datas = null;
            dbDemoEntities db = new dbDemoEntities();
            datas = from t in db.tProduct
                    select t;

            return View(datas);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel p)
        {
            dbDemoEntities db = new dbDemoEntities();
            var pBuy = db.tProduct.FirstOrDefault(t => t.fId == p.txtFId);
            if (pBuy == null)
                return RedirectToAction("List");

            tShoppingCart n = new tShoppingCart();
            n.fProductId = p.txtFId;
            n.fDate = DateTime.Now.ToString("yyyymmddhhmmss");
            n.fPrice = pBuy.fPrice;
            n.fCustomerId = 1;
            n.fCount = p.txtCount;
            
            db.tShoppingCart.Add(n);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}