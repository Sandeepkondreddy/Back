/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication- HHD Configuaration
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
    public class classReaderConfiguration
    {
        #region HHD Config-Fetch Reader Configuaration Details  -- Oracle
        public static DataSet FetchReaderDetailsOracle(string ReaderIP)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select READERID,READERNAME,READERMACID,SERVERIP,SERVERPORT from KPCT.KM_READERMASTER where READERIP='" + ReaderIP + "' and RSTATUS='A'";
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
        #endregion



        #region HHD Config-Fetch Reader Configuaration Details  -- SQL
        public static DataSet FetchReaderDetailsSQL(string ReaderIP)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select Readerno,ReaderName,Readermacid,ServerIP,Serverport  from [dbo].[ReaderMaster] where ReaderIP='" + ReaderIP + "' and RStatus='A'";
                classLog.writeLog("Message @: Config-FetchReaderDetails: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Reader Config : " + ex.ToString());
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



        #region Sync Location Master details from -- CFMS SQL DB 
        public static DataTable getLocationMasterDetailsCFSSQL(string getLocationMasterDtls)
        {
            DataTable dt = new DataTable("LocationMaster");
             try
            {
                string Query = "";
                if (getLocationMasterDtls == "CT-Yard")
                {
                    Query = "Select [Id],[Code],[Name] from [dbo].[TBL_GRID_LOCATION_MASTER] where [GroupName]=2 and [DeleteStatus]=0";
                }
                else if (getLocationMasterDtls == "CT-Warehouse")
                {
                    Query = "Select [Id],[Code],[Name] from [dbo].[TBL_GRID_LOCATION_MASTER] where [GroupName]=1 and [DeleteStatus]=0";
                }
                 classLog.writeLog("Message @: Location Master Details : " + Query.ToString());
                 classServerConnections.CFSSQLCMD.Connection = classServerConnections.CFSSQLdbCon;
                 classServerConnections.CFSSQLdbCon.Open();
                 classServerConnections.CFSSQLCMD.CommandText = Query;
                 SqlDataAdapter da = new SqlDataAdapter(classServerConnections.CFSSQLCMD);
                 classServerConnections.CFSSQLdbCon.Close();
                 da.Fill(dt);
            }
             catch (Exception ex)
             {
                 classLog.writeLog("Error @: Location Master Details : " + ex.ToString());
             }
             finally
             {
                 if (classServerConnections.CFSSQLCMD.Connection.State != ConnectionState.Closed)
                 {
                     classServerConnections.CFSSQLCMD.Connection.Close();
                 }
             }
             return dt;
        }
        #endregion
    }
}
