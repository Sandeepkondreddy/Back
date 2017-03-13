using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace WindowsApp
{
    class UserLogin
    {
        public Boolean getUserInfo(string username, string password)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection cn = new OleDbConnection(CONNSTRING);
            cn.Open();
            string sql;
            sql = "Select User_Name from UserMast where User_Name='" + username.ToLower() + "' and Passwd ='" + password + "' and Status='Active'";
            OleDbCommand cmd = new OleDbCommand(sql, cn);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
            cn.Close();
        }
    }
}
