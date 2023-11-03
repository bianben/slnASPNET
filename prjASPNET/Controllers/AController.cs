using prjMAUIDEMO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjASPNET.Controllers
{
    public class AController : Controller
    {
        // GET: A
        public ActionResult Index()
        {
            return View();
        }

        public string demoParameter(int? pid)
        {
            if (pid == 1)
                return "XBox 加入購物車成功";
            else if (pid == 2)
                return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoRequest()
        {
            string id = Request.QueryString["pid"];

            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            return "找不到該產品資料";
        }
        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\1.ispan\class\12_XAML\slnMauiDEMO\prjMauiDEMO\Resources\Images\fly.jpg");
            Response.End();
            return "";
        }
        public string lotto()
        {
            return new CLotto().pull();
        }
    }
}