using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjASPNET.Models
{
    public class CCustomerFactory
    {
        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter> ();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            List<CCustomer> list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        private List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader dr = cmd.ExecuteReader();
            List<CCustomer> list = new List<CCustomer>();
            while (dr.Read())
            {
                CCustomer x = new CCustomer()
                {
                    fId = (int)dr["fId"],
                    fName = dr["fName"].ToString(),
                    fPassword = dr["fPassword"].ToString(),
                    fEmail = dr["fEmail"].ToString(),
                    fAddress = dr["fAddress"].ToString(),
                    fPhone = dr["fPhone"].ToString()
                };
                list.Add(x);
            }
            conn.Close();
            return list;
        }

        public List<CCustomer> queryAll()
        {
            string sql = "SELECT * FROM tCustomer";
            return queryBySql(sql, null);
        }

        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "UPDATE tCustomer SET";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " fName=@K_fNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "fPhone=@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "fEmail=@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "fAddress=@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "fPassword=@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += " WHERE fId=@K_FID";
            paras.Add(new SqlParameter("K_FID", (object)p.fId));            
            executeSql(sql, paras);
        }
        public void delete(int? fId)
        {
            string sql = "DELETE FROM tCustomer WHERE fId=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            executeSql(sql, paras);
        }

        public void create(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer(";
            if(!string.IsNullOrEmpty(p.fName))
                sql += "fName,";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone,";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail,";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword,";
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += ")VALUES(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += ")";
            executeSql(sql, paras);
        }

        private static void executeSql(string sql,List<SqlParameter>paras)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
        }

        public List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE ";
            sql += " fName LIKE @K_KEYWORD ";
            sql += " OR fPHONE LIKE @K_KEYWORD ";
            sql += " OR fEMAIL LIKE @K_KEYWORD ";
            sql += " OR fADDRESS LIKE @K_KEYWORD";

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_KEYWORD", "%" + (object)keyword + "%"));
            return queryBySql(sql, paras);
        }
    }
}