using prjASPNET.Models;
using prjMAUIDEMO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        static int a=0;
        public ActionResult DemoCount()
        {
            a++;
            ViewBag.Count = a;
            return View();
        }
        public ActionResult DemoCountSession()
        {
            int count = 0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;
            ViewBag.COUNT = count;

            return View();
        }

        public ActionResult demoFrom()
        {
            ViewBag.ANS = "X=?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                int a = Convert.ToInt32(Request.Form["txtA"]);
                ViewBag.A = a;
                int b = Convert.ToInt32(Request.Form["txtB"]);
                ViewBag.B = b;
                int c = Convert.ToInt32(Request.Form["txtC"]);
                ViewBag.C = c;

                double d = (b * b) - (4 * a * c);
                double e = Math.Sqrt(d);

                double f = (-b + e) / (2 * a);
                double g = (-b - e) / (2 * a);
                


                ViewBag.ANS =$"X={f}或{g}";
            }
            return View();
        }

        public string testingQuery()
        {
            return "目前客戶共有:" + (new CCustomerFactory()).queryAll().Count();
        }

        public string testingUpdete()
        {
            CCustomer x = new CCustomer();
            x.fId = 4;
            x.fName= "test";
            x.fPhone = "03-556677";
            x.fEmail = "1234567890@gmail.com";
            x.fAddress = "Tao";
            x.fPassword = "1234567890";
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }

        public string testingDelete()
        {
            CCustomer x = new CCustomer();
            (new CCustomerFactory()).delete(1);
            return "刪除資料成功";
        }

        public string testingInsert()
        {
            CCustomer x = new CCustomer();
            x.fName = "Lin Chen";
            //x.fPhone = "0980800553";
            x.fEmail = "lin@gmail.com";
            //x.fAddress = "Taipei";
            x.fPassword = "123456";

            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }

        public ActionResult bindingCustomerById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //if (dr.Read())
                //{
                //    ViewBag.II = dr["fId"].ToString();
                //    ViewBag.KK = dr["fName"].ToString();
                //    ViewBag.PP = dr["fPhone"].ToString();
                //    ViewBag.MM = dr["fEmail"].ToString();
                //}

                if (dr.Read())
                {
                    x = new CCustomer()
                    {
                        fId = (int)dr["fId"],
                        fName = dr["fName"].ToString(),
                        fPhone = dr["fPhone"].ToString(),
                        fEmail = dr["fEmail"].ToString(),
                    };

                }
                conn.Close();
            }
            return View(x);
        }

        public ActionResult displayCustomerById(int? id)
        {
            if (id != null)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //if (dr.Read())
                //{
                //    ViewBag.II = dr["fId"].ToString();
                //    ViewBag.KK = dr["fName"].ToString();
                //    ViewBag.PP = dr["fPhone"].ToString();
                //    ViewBag.MM = dr["fEmail"].ToString();
                //}

                if (dr.Read())
                {
                    CCustomer x = new CCustomer()
                    {
                        fId = (int)dr["fId"],
                        fName = dr["fName"].ToString(),
                        fPhone = dr["fPhone"].ToString(),
                        fEmail = dr["fEmail"].ToString(),
                    };
                    ViewBag.KK=x;
                }
                conn.Close();
            }
            return View();
        }

        public string queryCustomer(int? id)
        {
            if (id == null)
                return "沒有指定id";
            
            string s = "沒有符合查詢的資料";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), conn);
            SqlDataReader dr= cmd.ExecuteReader();

            if(dr.Read())
                s = dr["fName"].ToString()+" / " + dr["fPhone"].ToString();

            conn.Close();
            return s;
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