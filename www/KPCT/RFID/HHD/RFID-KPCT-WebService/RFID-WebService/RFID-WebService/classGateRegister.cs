/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Comminication-Gate Registration
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
namespace RFID_WebService
{
    public class classGateRegister
    {
        //----Start---Oracle Section--------
        #region Validate Gateregister-Tag Free Details  -- Oracle
        public static int ValidateTagfreeOracle(string tagno)
        {
            int count =0;
            try{
            string Query = "select count(*) from KPCT.KT_TAG_REGISTER where TAGNO='" + tagno + "' and TRANSCTIONSTATUS='P' ";
            classLog.writeLog("Message @: Gate Register-Tag Validate: " + Query.ToString());
            classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
            classServerConnections.OracledbCon.Open();
            classServerConnections.OracleCMD.CommandText = Query;
             count = int.Parse(classServerConnections.OracleCMD.ExecuteScalar().ToString());
             classServerConnections.OracleCMD.Dispose();
            classServerConnections.OracledbCon.Close();
             }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Register-TagFree  : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return count;
        }
        #endregion

        #region Validate Gateregister-Truck Free Details  -- Oracle
        public static int ValidateTruckfreeOracle(string truckno)
        {
            int count = 0;
            try{
            string Query = "select count(*) from KPCT.KT_TAG_REGISTER where TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' ";
            classLog.writeLog("Message @: Gate Register-Truck Validate: " + Query.ToString());
            classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
            classServerConnections.OracledbCon.Open();
            classServerConnections.OracleCMD.CommandText = Query;
            count = int.Parse(classServerConnections.OracleCMD.ExecuteScalar().ToString());
            classServerConnections.OracleCMD.Dispose();
            classServerConnections.OracledbCon.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Register-TruckFree : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }

            return count;
        }
        #endregion

        #region Gateregister-Insert Tag Register Details  -- Oracle
        public static string InsertTagRegDetailsOracle(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into KPCT.KT_TAG_REGISTER(REGID,TAGNO,TRUCKNO,OPETYPE,REGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,LOADOREMPTY,TRANSCTIONSTATUS,REMARKS,SYNCTIME)" + "Values(RFID.RT_TAG_REGISTER_SEQ.NEXTVAL,'" + tagno + "','" + truckno + "','" + optype + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS')," + "'" + trucktype + "'" + ",'P','" + remarks + "',SYSDATE)";
                classLog.writeLog("Message @: Gate Register-Save: " + Query.ToString());
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

        #region Gateregister-Insert Tag Register Details Sync  -- Oracle
        public static string InsertTagRegDetailsSyncOracle(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                string tstatus;
                int TagFree=ValidateTagfreeOracle(tagno);
                if (TagFree == 0) { int TruckFree = ValidateTruckfreeOracle(truckno); if (TruckFree == 0) { tstatus = "P"; } else { tstatus = "D"; } } else tstatus = "D";
                string Query = "Insert into KPCT.KT_TAG_REGISTER(REGID,TAGNO,TRUCKNO,OPETYPE,REGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,LOADOREMPTY,TRANSCTIONSTATUS,REMARKS,SYNCTIME)" + "Values(RFID.RT_TAG_REGISTER_SEQ.NEXTVAL,'" + tagno + "','" + truckno + "','" + optype + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS')," + "'" + trucktype + "'" + ",'" + tstatus + "','" + remarks + "',SYSDATE)";
                classLog.writeLog("Message @: Gate Register-Save Sync: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                if(tstatus=="P")
                OpStatus = "SUCCESS";
                if (tstatus == "D")
                    OpStatus = "DSUCCESS";
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
        //----END---Oracle Section--------




        //----Start---SQL Section--------
        #region Validate Gateregister-Tag Free Details  -- SQL
        public static int ValidateTagfreeSQL(string tagno)
        {
            int count = 0;
            try
            {
                string Query = "select count(*) from [dbo].[Tag_Register] where [Tag No]='" + tagno + "' and [TransctionStatus]='P'";
                classLog.writeLog("Message @: Gate Register-Tag Validate: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                count = int.Parse(classServerConnections.SQLCMD.ExecuteScalar().ToString());
                classServerConnections.SQLCMD.Dispose();
                classServerConnections.SQLdbCon.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Register-TagFree  : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }
            return count;
        }
        #endregion

        #region Validate Gateregister-Truck Free Details  -- SQL
        public static int ValidateTruckfreeSQL(string truckno)
        {
            int count = 0;
            try
            {
                string Query = "select count(*) from [dbo].[Tag_Register] where [Truck No]='" + truckno.ToUpper().Trim() + "' and [TransctionStatus]='P' ";
                classLog.writeLog("Message @: Gate Register-Truck Validate: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                count = int.Parse(classServerConnections.SQLCMD.ExecuteScalar().ToString());
                classServerConnections.SQLCMD.Dispose();
                classServerConnections.SQLdbCon.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Gate Register-TruckFree : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.SQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.SQLCMD.Connection.Close();
                }
            }

            return count;
        }
        #endregion

        #region Gateregister-Insert Tag Register Details  -- SQL
        public static string InsertTagRegDetailsSQL(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                DateTime dt = Convert.ToDateTime(regtime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string Query = "Insert into [dbo].[Tag_Register]([Tag No],[Truck No],[Ope Type],[Reg Time],[Handle No],[Handle IP],[T Status],[Created By],[Created DT],[LoadorEmpty],[TransctionStatus],[Remarks],[Synctime])" + "Values(" + "'" + tagno + "'" + "," + "'" + truckno + "'" + "," + "'" + optype + "'" + "," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active'," + "'" + user + "'" + "," + ndt + "," + "'" + trucktype + "'" + ",'P','" + remarks + "',GETDATE())";
                classLog.writeLog("Message @: Gate Register-Save: " + Query.ToString());
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

        #region Gateregister-Insert Tag Register Details Sync  -- SQL
        public static string InsertTagRegDetailsSyncSQL(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                DateTime dt = Convert.ToDateTime(regtime);
                string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + cdt + "'}";
                string tstatus;
                int TagFree = ValidateTagfreeSQL(tagno);
                if (TagFree == 0) { int TruckFree = ValidateTruckfreeSQL(truckno); if (TruckFree == 0) { tstatus = "P"; } else { tstatus = "D"; } } else tstatus = "D";
                string Query = "Insert into [dbo].[Tag_Register]([Tag No],[Truck No],[Ope Type],[Reg Time],[Handle No],[Handle IP],[T Status],[Created By],[Created DT],[LoadorEmpty],[TransctionStatus],[Remarks],[Synctime])" + "Values(" + "'" + tagno + "'" + "," + "'" + truckno + "'" + "," + "'" + optype + "'" + "," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active'," + "'" + user + "'" + "," + ndt + "," + "'" + trucktype + "'" + ",'P','" + remarks + "',GETDATE())";
                classLog.writeLog("Message @: Gate Register-Save Sync: " + Query.ToString());
                classServerConnections.SQLCMD.Connection = classServerConnections.SQLdbCon;
                classServerConnections.SQLdbCon.Open();
                classServerConnections.SQLCMD.CommandText = Query;
                classServerConnections.SQLCMD.ExecuteNonQuery();
                classServerConnections.SQLdbCon.Close();
                if (tstatus == "P")
                    OpStatus = "SUCCESS";
                if (tstatus == "D")
                    OpStatus = "DSUCCESS";
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
        //----End---SQL Section--------
    }
    
}
