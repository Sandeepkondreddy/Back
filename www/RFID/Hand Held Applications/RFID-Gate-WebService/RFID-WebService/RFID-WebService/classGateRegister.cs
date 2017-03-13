/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication-Gate Registration
Created Date:		09 Sep 2015 
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
    public class classGateRegister
    {
        public static int ValidateTagfree(string tagno)
        {
            int count =0;
            try{
            string Query = "select count(*) from RFID.RT_TAG_REGISTER where TAGNO='" + tagno + "' and TRANSCTIONSTATUS='P' ";
            classLog.writeLog("Message @: Gate Register-Save: " + Query.ToString());
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


        public static int ValidateTruckfree(string truckno)
        {
            int count = 0;
            try{
            string Query = "select count(*) from RFID.RT_TAG_REGISTER where TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' ";
            classLog.writeLog("Message @: Gate Register-Save: " + Query.ToString());
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


        public static string InsertTagRegDetails(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into RFID.RT_TAG_REGISTER(REGID,TAGNO,TRUCKNO,OPETYPE,REGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,LOADOREMPTY,TRANSCTIONSTATUS,REMARKS,SYNCTIME)" + "Values(RFID.RT_TAG_REGISTER_SEQ.NEXTVAL,'" + tagno + "','" + truckno + "','" + optype + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS')," + "'" + trucktype + "'" + ",'P','" + remarks + "',SYSDATE)";
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


        //public static string InsertTagRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime,string tstatus)
        //{
        //    string OpStatus = "";
        //    try
        //    {
        //        string Query = "Insert into RFID.RT_TAG_REGISTER(REGID,TAGNO,TRUCKNO,OPETYPE,REGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,LOADOREMPTY,TRANSCTIONSTATUS,REMARKS,SYNCTIME)" + "Values(RFID.RT_TAG_REGISTER_SEQ.NEXTVAL,'" + tagno + "','" + truckno + "','" + optype + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS')," + "'" + trucktype + "'" + ",'" + tstatus + "','" + remarks + "',SYSDATE)";
        //        classLog.writeLog("Message @: Gate Register-Save Sync: " + Query.ToString());
        //        classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
        //        classServerConnections.OracledbCon.Open();
        //        classServerConnections.OracleCMD.CommandText = Query;
        //        classServerConnections.OracleCMD.ExecuteNonQuery();
        //        classServerConnections.OracledbCon.Close();
        //        OpStatus = "SUCCESS";
        //    }
        //    catch (Exception ex)
        //    {
        //        OpStatus = "FAILED";
        //        classLog.writeLog("Error @: Gate Register-Save : " + ex.ToString());
        //    }
        //    finally
        //    {
        //        if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
        //        {
        //            classServerConnections.OracleCMD.Connection.Close();
        //        }
        //    }
        //    return OpStatus;
        //}


        public static string InsertTagRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string OpStatus = "";
            try
            {
                string tstatus;
                int TagFree=ValidateTagfree(tagno);
                if (TagFree == 0) { int TruckFree = ValidateTruckfree(truckno); if (TruckFree == 0) { tstatus = "P"; } else { tstatus = "D"; } } else tstatus = "D";
                string Query = "Insert into RFID.RT_TAG_REGISTER(REGID,TAGNO,TRUCKNO,OPETYPE,REGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,LOADOREMPTY,TRANSCTIONSTATUS,REMARKS,SYNCTIME)" + "Values(RFID.RT_TAG_REGISTER_SEQ.NEXTVAL,'" + tagno + "','" + truckno + "','" + optype + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + regtime + "','DD-Mon-YYYY HH24:MI:SS')," + "'" + trucktype + "'" + ",'" + tstatus + "','" + remarks + "',SYSDATE)";
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

    }


}
