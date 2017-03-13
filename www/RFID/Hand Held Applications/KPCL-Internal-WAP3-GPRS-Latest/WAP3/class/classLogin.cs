/*********************************
Author Name:		Sandeep.K
Project Name:		RFID
Purpose:	        Login Screen
Created Date:		21 Oct 2012
Updated Date:       14 Feb 2015
******************************************************/
using System;
using System.Data.SqlServerCe;
using System.Collections.Generic;
using System.Text;

namespace WAP3
{
    class classLogin
    {
        
        private static string m_globalVar = "";

        public static string User
        {
            get { return m_globalVar; }
            set { m_globalVar = value; }
        }

        private static string n_globalVar = "";

        public static string UserType
        {
            get { return n_globalVar; }
            set { n_globalVar = value; }
        }

        private static string o_globalVar = "";

        public static string OpType
        {
            get { return o_globalVar; }
            set { o_globalVar = value; }
        }

        private static string p_globalVar = "";

        public static string ReaderIP
        {
            get { return p_globalVar; }
            set { p_globalVar = value; }
        }
        private static string q_globalVar = "";

        public static string ConnType
        {
            get { return q_globalVar; }
            set { q_globalVar = value; }
        }

        private static string AppNameDtl = "KPCL-Internal-WAP3";
        public static string AppName
        {
            get { return AppNameDtl; }
            set { AppNameDtl = value; }
        }

        private static string VersionDtl = "2.3";
        public static string Version
        {
            get { return VersionDtl; }
            set { VersionDtl = value; }
        }

        public static void LoginLog(string user, string usertype)
        {
            string localConnection = classServerDetails.SdfConnection;
            SqlCeConnection dbCon = new SqlCeConnection(localConnection);
            dbCon.Open();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            SqlCeCommand cmd1 = new SqlCeCommand("Insert into UserLoginLog(Loginid,UserType,LoginTime)" + "Values('" + user + "','" + usertype + "'," + ndt + ")", dbCon);
            cmd1.ExecuteNonQuery();
            dbCon.Close();
        }
    }
}
