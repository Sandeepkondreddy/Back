/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Comminication-YardOperations
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
    public class classYardOperations
    {
        //----Start---Oracle Section--------
        #region YardOperations-Fetch Truck Details  -- Oracle
        public static DataSet FetchTruckDetailsOracle(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select PID,TAGNO,TRUCKNO,YARDOPS_STATUS from KPCT.VW_TRUCKSWITHGP_YARDOPSSTATUS where TAGNO='" + tagno + "' order by PID desc";
                classLog.writeLog("Message @: Yard Operations-Get Truck Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Yard Operations-Get Truck Details : " + ex.ToString());
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

        #region YardOperations-Insert Yard Operation Details  -- Oracle
        public static string InsertYardOperationDetailsOracle(string tagno, string truckno, string loaderid, string readerno, string readerip, string user, string yardoptime)
        {
            string OpStatus = ""; string Pid = "";
            DataSet ds = FetchTruckDetailsOracle(tagno);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Pid = "0";
                classLog.writeLog("Message @: Yard Operations: Truck Details Not found.");
            }
            else
            {
                Pid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                classLog.writeLog("Message @: Yard Operations: Details Received from DB.");
            }
            try
            {

                string Query = "Insert into KPCT.KT_YARDTRANS(YTID,PID,TAGNO,TRUCKNO,YARDOPSTIME,READERID,READERIP,LOADERID,SUPERVISORNAME,STATUS,CREATEDBY,CREATEDDATE,SYNCTIME)" + "Values(RFID.RT_YARDTRANS_SEQ.NEXTVAL,'" + Pid + "','" + tagno + "','" + truckno + "',TO_DATE ('" + yardoptime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','" + loaderid + "','" + user + "','Active','" + user + "',TO_DATE ('" + yardoptime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: Yard Operation-Save: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";

                
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Yard Operation-Save : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return OpStatus;
        }
        #endregion
        //----END---Oracle Section--------



        //----Start---SQL Section--------
        #region YardOperations-Fetch Truck Details  -- SQL
        public static DataSet FetchTruckDetailsSQL(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select PID,TAGNO,TRUCKNO,YARDOPS_STATUS from dbo.VW_TRUCKSWITHGP_YARDOPSSTATUS where TAGNO='" + tagno + "' order by PID desc";
                classLog.writeLog("Message @: Yard Operations-Get Truck Details: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Yard Operations-Get Truck Details : " + ex.ToString());
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

        #region YardOperations-Insert Yard Operation Details  -- SQL
        public static string InsertYardOperationDetailsSQL(string tagno, string truckno, string loaderid, string readerno, string readerip, string user, string yardoptime)
        {
            string OpStatus = ""; string Pid = "";
            DataSet ds = FetchTruckDetailsSQL(tagno);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Pid = "0";
                classLog.writeLog("Message @: Yard Operations: Truck Details Not found.");
            }
            else
            {
                Pid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                classLog.writeLog("Message @: Yard Operations: Details Received from DB.");
            }
            try
            {
                DateTime dt = Convert.ToDateTime(yardoptime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string Query = "Insert into [dbo].[YardOperations]([TagRegid],[TagNo],[TruckNo],[YardDateTime],[YardHandleNo],[YardHandleIP],[LoaderId],[YardUserId])" + "Values('" + Pid + "','" + tagno + "','" + truckno + "'," + ndt + ",'" + readerno + "','" + readerip + "','" + loaderid + "','" + user + "')";
                classLog.writeLog("Message @: Yard Operation-Save: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                classServerConnections.SQLCMD.ExecuteNonQuery();
                classServerConnections.SQLdbCon.Close();

                OpStatus = "SUCCESS";


            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Yard Operation-Save : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }
            return OpStatus;
        }
        #endregion
        //----End---SQL Section--------
    }
}
