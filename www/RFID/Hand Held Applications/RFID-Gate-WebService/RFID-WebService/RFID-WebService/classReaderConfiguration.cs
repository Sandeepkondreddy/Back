/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication- HHD Configuaration
Created Date:		07 Sep 2015 
Updated Date:		25 Sep 2015  
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
namespace RFID_WebService
{
    public class classReaderConfiguration
    {
        public static DataSet FetchReaderDetails(string ReaderIP)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select READERNO,READERNAME,READERMACID,SERVERIP,SERVERPORT from RFID.RM_READERMASTER where READERIP='" + ReaderIP + "' and RSTATUS='A'";
                classLog.writeLog("Message @: Config-FetchReaderDetails: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds);

               
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Reader Config : " + ex.ToString());
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
