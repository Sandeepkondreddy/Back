/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication-Gate UnRegistration
Created Date:		10 Sep 2015 
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
    public class classGateUnRegister
    {
        public static DataSet FetchTruckDetails(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TRUCKNO,REGID from RFID.RT_TAG_REGISTER where TAGNO='" + tagno + "' and TRANSCTIONSTATUS='P' order by REGID desc";
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

        public static string InsertTagUnRegDetails(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into RFID.RT_TAG_UNREGISTER(UNREGID,REGID,TAGNO,TRUCKNO,LOADOREMPTY,UNREGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,SYNCTIME)" + "Values(RFID.RT_TAG_UNREGISTER_SEQ.NEXTVAL,'" + regid + "','" + tagno + "','" + truckno + "','" + trucktype + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: Gate UnRegister-Save: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                
                OpStatus = "SUCCESS";

               string x= Regdtlstatuschange(tagno,truckno,user,unregtime);
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
   
        public  static string Regdtlstatuschange(string tagno, string truckno, string user, string unregtime)
        {
            string OpStatus = "";
            try
            {
                string Query = "UPDATE RFID.RT_TAG_REGISTER SET TRANSCTIONSTATUS='C',MODIFIEDBY='" + user + "',MODIFIEDTIME=TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS') where TAGNO='" + tagno + "' and TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' and TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS')>=REGTIME";
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

        public static string InsertTagUnRegDetailsSync(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string regid = GetTagRegIdatSync(tagno, truckno, unregtime);

            string OpStatus = "";
            try
            {
                string Query = "Insert into RFID.RT_TAG_UNREGISTER(UNREGID,REGID,TAGNO,TRUCKNO,LOADOREMPTY,UNREGTIME,HANDLENO,HANDLEIP,TSTATUS,CREATEDBY,CREATEDTIME,SYNCTIME)" + "Values(RFID.RT_TAG_UNREGISTER_SEQ.NEXTVAL,'" + regid + "','" + tagno + "','" + truckno + "','" + trucktype + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),'" + readerno + "','" + readerip + "','Active','" + user + "',TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: Gate UnRegister-Save-Sync: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";

                string x = Regdtlstatuschange(tagno, truckno, user, unregtime);
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

        public static string GetTagRegIdatSync(string tagno, string truckno, string unregtime)
        {
            string tagregid = "";
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select REGID from RFID.RT_TAG_REGISTER where TAGNO='" + tagno + "' and TRUCKNO='" + truckno + "' and TRANSCTIONSTATUS='P' and TO_DATE ('" + unregtime + "','DD-Mon-YYYY HH24:MI:SS')>=REGTIME order by REGID desc";
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
    }
}
