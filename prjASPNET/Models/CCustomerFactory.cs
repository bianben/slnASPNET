using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjASPNET.Models
{
    public class CCustomerFactory
    {
        public void create(CCustomer p)
        {
            string sql = "INSERT INTO tCustomer(";
            sql += "fName,";
            sql += "fPhone,";
            sql += "fEmail,";
            sql += "fAddress,";
            sql += "fPassword,";
            sql += ")VALUES(";
            sql += "'"+p.fName+"',";
            sql += "'" + p.fPhone + "',";
            sql += "'" + p.fEmail + "',";
            sql += "'" + p.fAddress + "',";
            sql += "'" + p.fPassword + "',";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}