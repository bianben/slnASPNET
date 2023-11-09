using prjASPNET.Models;
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

        #region UploadFile
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
        #endregion UploadFile
        public ActionResult List()
        {
            IEnumerable<tProduct> datas = null;
            dbDemoEntities db = new dbDemoEntities();
            datas = from t in db.tProduct
                    select t;

            return View(datas);
        }

        #region AddToCart
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
        #endregion AddToCart

        #region AddToSession
        public ActionResult AddToSession(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel p)
        {
            dbDemoEntities db = new dbDemoEntities();
            var pBuy = db.tProduct.FirstOrDefault(t => t.fId == p.txtFId);
            if (pBuy == null)
                return RedirectToAction("List");
            List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
            if (cart == null)
            {
                cart= new List<CShoppingCartItem>();
                Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] = cart;
            }
            CShoppingCartItem x= new CShoppingCartItem();
            x.price = (decimal)pBuy.fPrice;
            x.count = p.txtCount;
            x.productId = pBuy.fId;
            x.product = pBuy;
            cart.Add(x);
            return RedirectToAction("List");
        }
        #endregion AddToSession
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = (List<CShoppingCartItem>)Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST];
            return View(cart);
        }
    }
}