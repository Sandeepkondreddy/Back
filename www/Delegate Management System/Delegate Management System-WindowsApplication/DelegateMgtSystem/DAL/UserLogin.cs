using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using DelegateMgtSystem.DAL;

namespace DelegateMgtSystem
{
    class UserLogin
    {
        public Boolean getUserInfo(string username, string password)
        {
            try
            {
                //string CONNSTRING = ConfigurationManager.ConnectionStrings["connection1"].ConnectionString;
                string CONNSTRING = GlobalVariables.Sqlconnection;
                SqlConnection cn = new SqlConnection(CONNSTRING);
                if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
                {
                    cn.Open();
                }
                string sql;
                sql = "Select [UId],[UserName],[LoginType] from [HHLoginMst] where [Loginid]='" + username.ToLower() + "' and [Password] ='" + password + "' and [UStatus]='Active'";
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    GlobalVariables.EmpId = dr[0].ToString();
                    GlobalVariables.LGNID = dr[1].ToString();
                    GlobalVariables.GrpName = dr[2].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
    }
}
