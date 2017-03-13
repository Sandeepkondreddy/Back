using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OleDb;

namespace WindowsApp
{
    class AddUsers
    {
        public Boolean checkuserinfo(string username)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection cn = new OleDbConnection(CONNSTRING);
            cn.Open();
            string sql;
            sql = "Select User_Name from UserMast where User_Name='" + username + "'";
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

        public Boolean insertuser(string username, string psaaword,string empno,string empname)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection cn = new OleDbConnection(CONNSTRING);
            cn.Open();
            string sql = "Insert into UserMast(Emp_No,Emp_Name,User_Name,Passwd,Status) values ('" + empno + "','" + empname + "','" + username + "','" + psaaword + "','Active')";
            //string sql = "Insert into UserMast(User_Name,Passwd) values ('sss','sss')";
            OleDbCommand cmd = new OleDbCommand(sql, cn);
            cmd.ExecuteNonQuery();
            //string sql1;
            //sql1 = "Select username from UserMast where User_Name='" + username + "'";
            //OleDbCommand cmd1 = new OleDbCommand(sql1, cn);
            //OleDbDataReader dr = cmd1.ExecuteReader();

            //if (dr.Read())
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            cn.Close();
            return true;
        }
    }
}
