/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication-Server Communication
Created Date:		07 Sep 2015 
Updated Date:		09 Dec 2015  
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
namespace RFID_WebService
{
    public class classServerConnections
    {

        private static string ConnectionString = "Data Source=EPMSREP;User ID=appl;password=appl;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=10; Incr Pool Size=1; Decr Pool Size=1;";
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

        
    }
}
