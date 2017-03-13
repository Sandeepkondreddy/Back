/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-AccessControl
Purpose:	        RFID-Acccess Control Oracle Comminication-Server Communication
Created Date:		24 Jan 2017 
Updated Date:		11 Mar 2017   
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace CS_RFID3_Host_Sample1
{
	class classServerConnections
	{
        public static readonly string SqlConn_String = "Data Source=172.168.3.174;Initial Catalog=RFID;User ID=sandeep;Password=san789;";

        private static string ConnectionString = "Data Source=STAND;User ID=appl;password=appl;Connection Lifetime=60; pooling=true; Min Pool Size=1; Max Pool Size=10; Incr Pool Size=1; Decr Pool Size=1;";
        private static OracleConnection dbCon = new OracleConnection(ConnectionString); 
        private static OracleCommand cmd = new OracleCommand();
        public static string OracleConnection
        {
            get { return ConnectionString; }
            set { ConnectionString = value; }
        }

        public static OracleCommand OracleCMD
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public static OracleConnection OracledbCon
        {
            get { return dbCon; }
            set { dbCon = value; }
        }
    
    }
}
