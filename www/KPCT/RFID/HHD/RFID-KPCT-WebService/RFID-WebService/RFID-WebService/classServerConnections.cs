/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Comminication-Server Communication
Created Date:		29 June 2016 
Updated Date:		29 June 2016 
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace RFID_WebService
{
    public class classServerConnections
    {
        //KPCT Test
        private static string ConnectionString = "Data Source=NPORTREP;User ID=appl;password=acer;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=10; Incr Pool Size=1; Decr Pool Size=1;";
        //Dev Test
        //private static string ConnectionString = "Data Source=TESTKPCL;User ID=appl;password=acer;Connection TimeOut=180; pooling=true; Min Pool Size=1; Max Pool Size=10; Incr Pool Size=1; Decr Pool Size=1;";
        private static OracleConnection dbCon = new OracleConnection(ConnectionString); private static OracleCommand cmd = new OracleCommand();
        public static string OracleConnection
        {
            get { return ConnectionString; }
            set { ConnectionString = value; }
        }

        public static OracleCommand OracleCMD

        {
            get {   return cmd; }
            set {  cmd = value; }
        }

        public static OracleConnection OracledbCon
        {
            get { return dbCon; }
            set { dbCon = value; }
        }



        private static string ConnectionStringSQL = "Data Source=172.168.1.44 ;Initial Catalog=RFID;User ID=sandeep;Password=san789;";
        
        private static SqlConnection SQLCon = new SqlConnection(ConnectionStringSQL); private static SqlCommand SQLcmd = new SqlCommand();
        public static string SQLConnection
        {
            get { return ConnectionStringSQL; }
            set { ConnectionStringSQL = value; }
        }

        public static SqlCommand SQLCMD
        {
            get { return SQLcmd; }
            set { SQLcmd = value; }
        }

        public static SqlConnection SQLdbCon
        {
            get { return SQLCon; }
            set { SQLCon = value; }
        }

        //private static string CFSConnectionStringSQL = "Data Source=172.168.1.44 ;Initial Catalog=CFMS;User ID=sandeep;Password=san789;";
        private static string CFSConnectionStringSQL = "Data Source=172.168.0.85 ;Initial Catalog=CFSMS;User ID=sa;Password=KPCT@!2$;";
        private static SqlConnection CFSSQLCon = new SqlConnection(CFSConnectionStringSQL); private static SqlCommand CFSSQLcmd = new SqlCommand();
        public static SqlCommand CFSSQLCMD
        {
            get { return CFSSQLcmd; }
            set { CFSSQLcmd = value; }
        }
        public static SqlConnection CFSSQLdbCon
        {
            get { return CFSSQLCon; }
            set { CFSSQLCon = value; }
        }
    }
}
