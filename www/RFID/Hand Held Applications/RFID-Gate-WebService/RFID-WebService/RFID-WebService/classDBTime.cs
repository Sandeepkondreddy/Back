/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication- DB Time Sync
Created Date:		25 Sep 2015 
Updated Date:		09 Dec 2015  
******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;

namespace RFID_WebService
{
    public class classDBTime
    {

        public static DataSet FetchDBTime()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT TO_CHAR  (SYSDATE, 'YYYY-MM-DD HH24:MI:SS') FROM DUAL";
                classLog.writeLog("Message @: Gate Login-Get DB Time Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Login-Get DB Time Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return ds;
        }
    }
}
