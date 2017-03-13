using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DelegateMgtSystem.DAL
{
    class GlobalVariables
    {
        #region TestDB
        //For Oracle DB Connection       
        //public static readonly string CONN_STRING = @"Data Source=EPMSREP;User ID=appl;password=appl;Connection TimeOut=180; pooling=true; Min Pool Size=1; Max Pool Size=1000; Incr Pool Size=1; Decr Pool Size=1;";
        //public static readonly string MenuConnString = @"Data Source=EPMSREP;User ID=appl;password=appl;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=1000; Incr Pool Size=1; Decr Pool Size=1;";
        
        // For Acess DB Connection
        public static readonly string CON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=venkat;Data Source=C:\DB\RailOffline.mdb;";
        //For SQL DB Connection
        public static readonly string Sqlconnection = ConfigurationSettings.AppSettings["LocalSYSTEMIP"];//"Data Source=172.168.3.122;Initial Catalog=DMS;User ID=sa;Password=123456;";
        public static readonly string MainServerSqlconnection = ConfigurationSettings.AppSettings["MainSYSTEMIP"];//"Data Source=172.168.3.122;Initial Catalog=DMS;User ID=sa;Password=123456;";
        //For Crystal Report Printing 
        public static readonly string Servername = "EPMSREP";
        public static readonly string ServerUID = "appl";
        public static readonly string ServerPWD = "appl";
        public static readonly bool WeightReadOnly = false;
        public static string ApplicType = "TEST";
        public static string ReaderIP=string.Empty;
        public static string ReaderIP2 = string.Empty;
        public static string Rstatus = string.Empty;
        public static string TagCount1 = "0";
        public static string TagCount2 = "0";
        #endregion

        #region LIVE DB
        ////For Oracle DB Connection
        //public static readonly string CONN_STRING = @"Data Source=KPCLEPMS;User ID=rfid;password=windows;Connection TimeOut=180; pooling=true; Min Pool Size=1; Max Pool Size=1000; Incr Pool Size=1; Decr Pool Size=1;";
        //public static readonly string MenuConnString = @"Data Source=KPCLEPMS;User ID=rfid;password=windows;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=1000; Incr Pool Size=1; Decr Pool Size=1;";
        
        //// For Acess DB Connection
        //public static readonly string CON_STRING = @"Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=venkat;Data Source=C:\DB\RailOffline.mdb;";
        ////For SQL DB Connection
        //public static readonly string Sqlconnection = "Data Source=172.168.6.239;Initial Catalog=RFID;User ID=sa;Password=123456;";
        ////For Crystal Report Printing 
        //public static readonly string Servername = "KPCLEPMS";
        //public static readonly string ServerUID = "rfid";
        //public static readonly string ServerPWD = "windows";
        //public static readonly bool WeightReadOnly = true;
        //public static string ApplicType = "LIVE";
        #endregion

        #region Variable Declaration
        public static string UserID = string.Empty;  //user group name
        public static string LGNID = string.Empty;
        public static string EmpName = string.Empty;
        public static string EmpId = string.Empty;
        public static string DeptName = string.Empty;
        public static string ScreenGroupName = string.Empty;
        public static string GrpMapp = string.Empty;
       // public static int LoginID = 0;
        public static string EditMode = "N";
        public static string GrpName = string.Empty;

        public static string OfflineFlag = "N";
        #endregion

        #region ApplicationName and Version
        public static string ApplicationName = "DelegateMgtSystem";
        public static string version = "V 1.0";
        public static string versionLog = "V 1.0 Design";
        #endregion
    }

}
