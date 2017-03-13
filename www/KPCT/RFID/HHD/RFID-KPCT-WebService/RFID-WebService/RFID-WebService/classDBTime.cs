/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication- DB Time Sync
Created Date:		29 June 2016
Updated Date:		29 June 2016
******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using System.Data.SqlClient;
namespace RFID_WebService
{
    public class classDBTime
    {

        #region Login-Fetch DB Time Details  -- Oracle
        public static DataSet FetchDBTimeOracle()
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
        #endregion


        #region Login-Fetch DB Time Details  -- SQL
        public static DataSet FetchDBTimeSQL()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select GetDATE()";
                classLog.writeLog("Message @: Gate Login-Get DB Time Details: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Login-Get DB Time Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }
            return ds;
        }
        #endregion
    }
}
