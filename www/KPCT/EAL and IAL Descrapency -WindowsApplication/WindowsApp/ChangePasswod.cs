using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace WindowsApp
{
    class ChangePasswod
    {
        public Boolean checkuserinfo(string username, string oldpassword)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection cn = new OleDbConnection(CONNSTRING);
            cn.Open();
            string sql;
            sql = "Select User_Name from UserMast where User_Name='" + username + "' and Passwd='" + oldpassword + "'";
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
        public Boolean updatePasswd(string username, string newpsaaword)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection cn = new OleDbConnection(CONNSTRING);
            cn.Open();
            string sql = "Update UserMast set User_Name='" + username + "',Passwd='" + newpsaaword + "' where User_Name='" + username + "'";
            OleDbCommand cmd = new OleDbCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            return true;
        }
    }
}
