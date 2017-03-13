/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication-Gate UnRegistration
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
    public class classGateUnRegister
    {
        //----Start---Oracle Section--------
        #region GateUnRegister-Fetch Truck Details  -- Oracle
        public static DataSet FetchTruckDetailsOracle(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TRUCKNO,REGID from KPCT.KT_TAG_REGISTER where TAGNO='" + tagno + "' and TRANSCTIONSTATUS='P' order by REGID desc";
                classLog.writeLog("Message @: Gate UnRegister-Get Truck Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate UnRegister-Get Truck Details : " + ex.ToString());
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

        #region GateUnRegister-Insert Tag UnRegister Details  -- Oracle
        public static string InsertTagUnRegDetailsOracle(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into KPCT.KT_TAG_UNREGISTER(UNREGID,REGID,TAGNO,TRUCKNO,LOADOREMPTY,UNREGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,SYNCTIME)" + "Values(RFID.RT_TAG_UNREGISTER_SEQ.NEXTVAL,'" + regid + "','" + tagno + "','" + truckno + "','" + trucktype + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: Gate UnRegister-Save: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                
                OpStatus = "SUCCESS";

               string x= RegdtlstatuschangeOracle(tagno,truckno,user,unregtime);
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate UnRegister-Save : " + ex.ToString());
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

        #region Status Change in Registration Table after Unregistration  -- Oracle
        public  static string RegdtlstatuschangeOracle(string tagno, string truckno, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "UPDATE KPCT.KT_TAG_REGISTER SET TRANSCTIONSTATUS='C',MODIFIEDBY='" + user + "',MODIFIEDTIME=TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS') where TAGNO='" + tagno + "' and TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' and TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS')>=REGTIME";
                classLog.writeLog("Message @: Gate UnRegister-Register Status Update: " + Query.ToString());
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
                classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
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

        #region GateUnRegister-Insert Tag UnRegister Details Sync  -- Oracle
        public static string InsertTagUnRegDetailsSyncOracle(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string regid = GetTagRegIdatSyncOracle(tagno, truckno, unregtime);

            string OpStatus = "";
            try
            {
                string Query = "Insert into KPCT.KT_TAG_UNREGISTER(UNREGID,REGID,TAGNO,TRUCKNO,LOADOREMPTY,UNREGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,SYNCTIME)" + "Values(RFID.RT_TAG_UNREGISTER_SEQ.NEXTVAL,'" + regid + "','" + tagno + "','" + truckno + "','" + trucktype + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: Gate UnRegister-Save-Sync: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";

                string x = RegdtlstatuschangeOracle(tagno, truckno, user, unregtime);
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate Register-Save-Sync : " + ex.ToString());
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

        #region Get Tag Registration Id for Offline Data Sync  -- Oracle
        public static string GetTagRegIdatSyncOracle(string tagno, string truckno, string unregtime)
        {
            string tagregid = "";
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select REGID from KPCT.KT_TAG_REGISTER where TAGNO='" + tagno + "' and TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' and TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS')>=REGTIME order by REGID desc";
                classLog.writeLog("Message @: Gate UnRegister-GetTagRegId at Sync: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds); 
                if (ds.Tables[0].Rows.Count == 0)
                {
                    classLog.writeLog("Warning @: Gate UnRegister-Get Truck- Tag RegId Details Not found. ");
                    tagregid = "0";
                }
                else
                {
                    tagregid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate UnRegister-Get Truck- Tag RegId Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }

            return tagregid;
        }
        #endregion
        //----END---Oracle Section--------



        //----Start---SQL Section--------
        #region GateUnRegister-Fetch Truck Details  -- SQL
        public static DataSet FetchTruckDetailsSQL(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select [Truck No],[Tag ID] from [dbo].[Tag_Register] where [Tag No]='" + tagno + "' and [TransctionStatus]='P' order by [Tag ID] desc";
                classLog.writeLog("Message @: Gate UnRegister-Get Truck Details: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate UnRegister-Get Truck Details : " + ex.ToString());
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

        #region GateUnRegister-Insert Tag UnRegister Details  -- SQL
        public static string InsertTagUnRegDetailsSQL(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                DateTime dt = Convert.ToDateTime(unregtime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string Query = "Insert into [dbo].[Tag_UnRegister]([TagRegid],[TagNo],[TruckNo],[LoadorEmpty],[UnRegDateTime],[UnRegHandleNo],[UnRegHandleIP],[RStatus],[CreatedBy],[CreatedDate],[Synctime])" + "Values(" + regid + "," + "'" + tagno + "'" + "," + "'" + truckno + "'" + "," + "'" + trucktype + "'" + "," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active'," + "'" + user + "'" + "," + ndt + ",GETDATE())";
                classLog.writeLog("Message @: Gate UnRegister-Save: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                classServerConnections.SQLCMD.ExecuteNonQuery();
                classServerConnections.SQLdbCon.Close();

                OpStatus = "SUCCESS";

                string x = RegdtlstatuschangeSQL(tagno, truckno, user, unregtime);
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
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

        #region Status Change in Registration Table after Unregistration  -- SQL
        public static string RegdtlstatuschangeSQL(string tagno, string truckno, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                DateTime dt = Convert.ToDateTime(unregtime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string Query = "update [dbo].[Tag_Register] set [TransctionStatus]='C',[ModifiedBy]='" + user + "',[ModifiedDate]=" + dt + " where [Tag No]='" + tagno + "' and [Truck No]='" + truckno + "' and [TransctionStatus]='P' and " + ndt + ">=[Reg Time]";
                classLog.writeLog("Message @: Gate UnRegister-Register Status Update: " + Query.ToString());
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
                classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
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

        #region GateUnRegister-Insert Tag UnRegister Details Sync  -- SQL
        public static string InsertTagUnRegDetailsSyncSQL(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string regid = GetTagRegIdatSyncSQL(tagno, truckno, unregtime);

            string OpStatus = "";
            try
            {
                DateTime dt = Convert.ToDateTime(unregtime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string Query = "Insert into [dbo].[Tag_UnRegister]([TagRegid],[TagNo],[TruckNo],[LoadorEmpty],[UnRegDateTime],[UnRegHandleNo],[UnRegHandleIP],[RStatus],[CreatedBy],[CreatedDate],[Synctime]) Values('" + regid + "'," + "'" + tagno + "'" + ",'" + truckno + "','" + trucktype + "'," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active','" + user + "'," + ndt + ",GETDATE())";
                classLog.writeLog("Message @: Gate UnRegister-Save-Sync: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                classServerConnections.SQLCMD.ExecuteNonQuery();
                classServerConnections.SQLdbCon.Close();

                OpStatus = "SUCCESS";

                string x = RegdtlstatuschangeSQL(tagno, truckno, user, unregtime);
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Gate Register-Save-Sync : " + ex.ToString());
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

        #region Get Tag Registration Id for Offline Data Sync  -- SQL
        public static string GetTagRegIdatSyncSQL(string tagno, string truckno, string unregtime)
        {
            string tagregid = "";
            DataSet ds = new DataSet();
            try
            {
                string Query = "select [Truck No],[Tag ID] from [dbo].[Tag_Register] where [Tag No]='" + tagno + "' and [Truck No]='" + truckno + "' and [TransctionStatus]='P' order by [Tag ID] desc";
                classLog.writeLog("Message @: Gate UnRegister-GetTagRegId at Sync: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.SQLCMD);
                classServerConnections.SQLdbCon.Close();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    classLog.writeLog("Warning @: Gate UnRegister-Get Truck- Tag RegId Details Not found. ");
                    tagregid = "0";
                }
                else
                {
                    tagregid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }

            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate UnRegister-Get Truck- Tag RegId Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }

            return tagregid;
        }
        #endregion
        //----End---SQL Section--------
    }
}
