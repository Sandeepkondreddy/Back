/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Comminication-KPCT Operations
Created Date:		04 July 2016
Updated Date:		09 Aug 2016
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
    public class classSDSOperations
    {

        //----Start---Oracle Section--------
        #region SDS Operations-Fetch Truck Details  -- Oracle
        public static DataSet FetchTruckDetailsOracle(string tagno,string usertype,string HHDlocation,string readerno,string user)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TRUCKNO,PARTY,CHANAME,CARGO,LOCID,GIN_QTY,STATUS,NEXTSTATUS,TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                classLog.writeLog("Message @: KPCT Operations-Get Truck Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                da.Fill(ds);
                ds.Tables[0].Columns.Add(new DataColumn("ButtonState"));
                ds.Tables[0].Columns.Add(new DataColumn("ActivityEndDetailsState"));
                string qtydetails = "Disable";//Activity End details capturing
                string btnstate = "Disable";
                string Location = getLocationDetailsCFSSQL(ds.Tables[0].Rows[0].ItemArray[4].ToString());
                //ds.Tables[0].Columns.Add(new DataColumn("ActivityEndDetailsState"));
                //ds.Tables[0].Rows[0].ItemArray[4] = Location.ToString();
                string ReaderLoc=HHDlocation.ToString();
                if (usertype == "user")
                {
                    if (HHDlocation == "CT-Parking")
                    {
                        
                        if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "PARKING IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                         else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "PARKING OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                         else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "OUT PARK-IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                         else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "OUT PARK-OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "KPCT OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else { btnstate = "Disable"; qtydetails = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc, user); }
                         
                    }
                    else if (HHDlocation == "CT-Yard"||HHDlocation=="CT-Warehouse")
                    {
                        if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                        //else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD/WH OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "KPCT OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else { btnstate = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc, user); }
                        qtydetails = "Disable";
                    }
                }
                else if (usertype == "supervisor")
                {
                    if (HHDlocation == "CT-Parking")
                    {
                        btnstate = "Disable"; qtydetails = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc,  user);
                    }
                    else if (HHDlocation == "CT-Yard" || HHDlocation == "CT-Warehouse")
                    {
                       // if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD/WH IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                        if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-ACTIVITY START") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "WH-ACTIVITY START") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "ACTIVITY END") { btnstate = "Enable"; qtydetails = "Enable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD/WH OUT") { btnstate = "Enable"; qtydetails = "Enable"; }
                        // else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "KPCT OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else { btnstate = "Disable"; qtydetails = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc,  user); }
                    }
                }
                else if (usertype == "admin")
                {
                    if (HHDlocation == "CT-Parking")
                    {
                        if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "PARKING IN") btnstate = "Enable";
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "PARKING OUT") btnstate = "Enable";
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "OUT PARK-IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "OUT PARK-OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "KPCT OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else { btnstate = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc,  user); }
                        qtydetails = "Disable";
                    }
                    else if (HHDlocation == "CT-Yard" || HHDlocation == "CT-Warehouse")
                    {
                        if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD IN") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-YARD/WH OUT") { btnstate = "Enable"; qtydetails = "Enable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "CT-ACTIVITY START") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "WH-ACTIVITY START") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "ACTIVITY END") { btnstate = "Enable"; qtydetails = "Enable"; }
                        else if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == "KPCT OUT") { btnstate = "Enable"; qtydetails = "Disable"; }
                        else { btnstate = "Disable"; qtydetails = "Disable"; string sts = InsertKPCTOperationDeniedLogDetailsOracle(ds.Tables[0].Rows[0].ItemArray[8].ToString(), ds.Tables[0].Rows[0].ItemArray[6].ToString(), readerno, ReaderLoc,  user); }
                    }
                }
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    ds.Tables[0].Rows[i]["ButtonState"] = btnstate.ToString(); ds.Tables[0].Rows[i]["ActivityEndDetailsState"] = qtydetails.ToString();
                    ds.Tables[0].Rows[i]["LOCID"] = Location.ToString();
                   }
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: KPCT Operations-Get Truck Details : " + ex.ToString());
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

        #region Get Location Type Details  -- Oracle
        public static string GetLocationTypeDetails(string truckid)
        {
            string Locationtype = "Yard";
            try
            {
                string Query = "Select OP_LOCTYPE_WHCTYARD from KPCT.KT_SDS_PRKINGDTLS where TRUCKID=" + truckid + "";
                classLog.writeLog("Message @: KPCT Operations-Location Type Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                DataSet ds = new DataSet();
                da.Fill(ds);
                string Locid=ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (Locid == "1") Locationtype = "Warehouse";
                else if (Locid == "2") Locationtype = "Yard";
                else Locationtype = "NA";
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: KPCT Operations-Get Location Type Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return Locationtype;
        }
        #endregion

        #region SDS Operations-Insert Truck Details  -- Oracle
        public static string InsertKPCTOperationDetailsOracle(string tagno, string saveoperation, string readerno, string savetime, string user, string usertype, string HHDlocation, string Qty, string OPLOC, string CargoCondition, string WeatherCondition, string OpHandledBy, string OpHandledType, string TallySheetType, string ContainerNo)
        {
            string OpStatus = "FAILED";
            DataSet ds = new DataSet();
            ds = FetchTruckDetailsOracle(tagno, usertype, HHDlocation,readerno,user);
            string truckno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            string truckid = ds.Tables[0].Rows[0].ItemArray[8].ToString();
            if (ds.Tables[0].Rows[0].ItemArray[7].ToString() == saveoperation.ToString())
            {
                string LogStatus = InsertKPCTOperationLogDetailsOracle(tagno, truckno, saveoperation, readerno, savetime, user, HHDlocation);
                string LOCTYPE = "";
                if (saveoperation != "PARKING IN") LOCTYPE = GetLocationTypeDetails(truckid);
                string OPLOCTYPE = "";

                if (HHDlocation == "CT-Yard") OPLOCTYPE = "2";
                else if (HHDlocation == "CT-Warehouse") OPLOCTYPE = "1";
                else OPLOCTYPE = "0";

                if (LogStatus == "SUCCESS")
                {
                    if (saveoperation == "PARKING IN")
                    {
                        string statusid = "3";
                        OpStatus = SaveParkingIndetails(truckid, statusid, user, savetime);
                    }
                    else if (saveoperation == "PARKING OUT")
                    {
                        string statusid = "4";
                        OpStatus = SaveParkingOutdetails(truckid, statusid, user, savetime);
                    }
                    else if (saveoperation == "CT-YARD IN")
                    {
                        string statusid = "5";
                        OpStatus = SaveYardIndetails(truckid, statusid, user, savetime);
                    }
                    else if (saveoperation == "WH-ACTIVITY START")
                    {
                        string statusid = "5";
                        OpStatus = SaveWhIndetails(truckid, statusid, user, savetime);
                    }
                    else if (saveoperation == "CT-ACTIVITY START")
                    {
                        OpStatus = SaveActivityStartdetails(truckid, user, savetime);
                    }
                    else if (saveoperation == "ACTIVITY END")
                    {
                        if (LOCTYPE == "Yard")
                            OpStatus = SaveActivityEnddetails(truckid, Qty, OPLOC, OPLOCTYPE, user, savetime, CargoCondition, WeatherCondition, OpHandledBy, OpHandledType, TallySheetType,ContainerNo);
                        else if (LOCTYPE == "Warehouse")
                            OpStatus = SaveWhOutdetails(truckid, Qty, OPLOC, OPLOCTYPE, user, savetime, CargoCondition, WeatherCondition, OpHandledBy, OpHandledType, TallySheetType, ContainerNo);
                        else OpStatus = SaveActivityEnddetails(truckid, Qty, OPLOC, OPLOCTYPE, user, savetime, CargoCondition, WeatherCondition, OpHandledBy, OpHandledType, TallySheetType, ContainerNo);
                    }
                    else if (saveoperation == "CT-YARD/WH OUT")
                    {
                        OpStatus = SaveYardOutdetails(truckid, user, savetime);
                    }
                    else if (saveoperation == "OUT PARK-IN")
                    {
                        OpStatus = SaveOutParkingIndetails(truckid, user, savetime);
                    }
                    else if (saveoperation == "OUT PARK-OUT")
                    {
                        OpStatus = SaveOutParkingOutdetails(truckid, user, savetime);
                    }
                    else if (saveoperation == "KPCT OUT")
                    {
                        OpStatus = SaveKPCTOutdetails(truckid, user, savetime);
                    }
                }
            }
            else OpStatus = "Stage Modified to " + ds.Tables[0].Rows[0].ItemArray[7].ToString()+"Please Check.";
            return OpStatus;
        }

        //Insert Reader Log Details --Start--
        public static string InsertKPCTOperationLogDetailsOracle(string tagno, string truckno, string saveoperation, string readerno, string savetime, string user, string HHDlocation)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into KPCT.KT_HHDLOG(ID,READERID,TAGNO,TRUCKNO,OPERATION,READERLOCATION,CRETADEBY,CRATEDTIME,SYNCTIME)" + "Values(KPCT.KT_HHDLOG_SEQ.NEXTVAL," + readerno + ",'" + tagno + "','" + truckno + "','" + saveoperation + "','" + HHDlocation + "','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),SYSDATE)";
                classLog.writeLog("Message @: KPCT Operations-Save HHD Log: " + Query.ToString());
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
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Insert Reader Log Details --End--

        //Save Parking In Details --Start--
        public static string SaveParkingIndetails(string truckid, string statusid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                //string Query = "Update KPCT.KT_CFMS_JO_REGDTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_SDS_PRKINGDTLS(TRUCKID,PARKING_INDATETIME,CREATEDBY,CREATEDDATE)" + "Values('" + truckid + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",2,'Truck From 1st Transit to Parking','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query3 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=2";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + "," + statusid + ",TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveParkingIndetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingIndetails-Insert KT_SDS_PRKINGDTLS: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                if (isReason != "Yes")
                {
                    classServerConnections.OracleCMD.CommandText = Query2;
                    classLog.writeLog("Message @: KPCT Operations-SaveParkingIndetails-Insert KT_TRUCKTRACKER: " + Query2.ToString());
                    classServerConnections.OracleCMD.ExecuteNonQuery();
                }
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingIndetails-Update KT_STAGETRACKER: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingIndetails-Insert KT_STAGETRACKER: " + Query4.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Parking In Details --End--

        //Save Parking Out Details --Start--
        public static string SaveParkingOutdetails(string truckid, string statusid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                //string Query = "Update KPCT.KT_CFMS_JO_REGDTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query1 = "Update KPCT.KT_SDS_PRKINGDTLS set PARKING_OUTDATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                string Query2 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",3,'Truck From Parking to 2nd Transit','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query3 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=3";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + ","+statusid+",TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveParkingOutdetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingOutdetails-Update KT_SDS_PRKINGDTLS: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                if (isReason != "Yes")
                {
                    classServerConnections.OracleCMD.CommandText = Query2;
                    classLog.writeLog("Message @: KPCT Operations-SaveParkingOutdetails-Insert KT_TRUCKTRACKER: " + Query2.ToString());
                    classServerConnections.OracleCMD.ExecuteNonQuery();
                }
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingOutdetails-Update KT_STAGETRACKER: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveParkingOutdetails-Insert KT_STAGETRACKER: " + Query4.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Parking Out Details --End--

        //Save CT Yard In Details --Start--
        public static string SaveYardIndetails(string truckid, string statusid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                //string Query = "Update KPCT.KT_CFMS_JO_REGDTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_CTYRDWH_OPDTLS(TRUCKID,CTYRDWH_IN_DATETIME,CREATEDBY,CREATEDDATE)" + "Values('" + truckid + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",4,'Truck From 2nd Transit to Yard In','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query3 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=4";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + "," + statusid + ",TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhIndetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveYardIndetails-Insert KT_CTYRDWH_OPDTLS: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                if (isReason != "Yes")
                {
                    classServerConnections.OracleCMD.CommandText = Query2;
                    classLog.writeLog("Message @: KPCT Operations-SaveYardIndetails-Insert KT_TRUCKTRACKER: " + Query2.ToString());
                    classServerConnections.OracleCMD.ExecuteNonQuery();
                }
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveYardIndetails-Update KT_STAGETRACKER: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveYardIndetails-Insert KT_STAGETRACKER: " + Query4.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save CT Yard In Details --End--

        //Save Activity Start Details --Start--
        public static string SaveActivityStartdetails(string truckid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Update KPCT.KT_CTYRDWH_OPDTLS set OP_STARTDATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid +"";
                classLog.writeLog("Message @: KPCT Operations-SaveActivityStartdetails-Update KT_CTYRDWH_OPDTLS: " + Query.ToString());
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
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Activity Start Details --End--

        //Save Activity End Details --Start--
        public static string SaveActivityEnddetails(string truckid, string OpQty, string OPLOC, string OPLOCTYPE, string user, string savetime, string CargoCondition, string WeatherCondition, string OpHandledBy, string OpHandledType, string TallySheetType, string ContainerNo)
        {
            string OpStatus = "";
            try
            {
                string Query = "Update KPCT.KT_CTYRDWH_OPDTLS set OP_ENDDATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),LOC_CTWH_QTY=" + OpQty + ",CTYRDWH_LOCID=" + OPLOC + ",OP_LOCTYPE_WHCTYARD=" + OPLOCTYPE + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),CARGO_CONID=" + CargoCondition + ",WEATHER_CONID=" + WeatherCondition + ",HANDLDBY_CMPNYID=" + OpHandledBy + ",HANDLINGTYPE='" + OpHandledType + "',TALLYSHEET_TYPE='" + TallySheetType + "',CONTAINER_NO='" + ContainerNo + "' where TRUCKID=" + truckid + "";
                //if (OpQty.ToString() == "") Query = "Update KPCT.KT_CTYRDWH_OPDTLS set OP_ENDDATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),CTYRDWH_LOCID=" + OPLOC + ",OP_LOCTYPE_WHCTYARD=" + OPLOCTYPE + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                classLog.writeLog("Message @: KPCT Operations-SaveActivityEnddetails-Update KT_CTYRDWH_OPDTLS: " + Query.ToString());
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
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Activity End Details --End--

        //Save CT Yard Out Details --Start--
        public static string SaveYardOutdetails(string truckid,string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Update KPCT.KT_CTYRDWH_OPDTLS set CTYRDWH_OUT_DATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",5,'Truck From Yard to SDS Out','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=5";
                string Query3 = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=6,MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + ",6,TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Update KT_CTYRDWH_OPDTLS: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Insert KT_TRUCKTRACKER: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query2;
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Update KT_STAGETRACKER: " + Query2.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveYardWhOutdetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveYardWhOutdetails-Insert KT_STAGETRACKER: " + Query.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();

                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save CT Yard Out Details --End--

        //Save Out Parking In Details --Start--
        public static string SaveOutParkingIndetails(string truckid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {

                string Query = "Insert into KPCT.KT_SDSOUT_PRKINGDTLS(TRUCKID,PARKING_INDATETIME,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + ",TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query1 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",6,'Truck From CT Yard/WH Out to Out Parking In','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Insert KT_SDSOUT_PRKINGDTLS: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Out Parking In Details --End--

        //Save Out Parking Out Details --Start--
        public static string SaveOutParkingOutdetails(string truckid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Update KPCT.KT_SDSOUT_PRKINGDTLS set PARKING_OUTDATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",6,'Truck From SDS Out to Out Parking Out/KPCT Out','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Update KT_SDSOUT_PRKINGDTLS: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = SaveKPCTOutdetails(truckid, user, savetime);
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save Out Parking Out Details --End--


        //Save CT WH In Details --Start--
        public static string SaveWhIndetails(string truckid, string statusid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                //string Query = "Update KPCT.KT_CFMS_JO_REGDTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=" + statusid + ",MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_CTYRDWH_OPDTLS(TRUCKID,CTYRDWH_IN_DATETIME,CREATEDBY,CREATEDDATE)" + "Values('" + truckid + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",4,'Truck From 2nd Transit to WH In','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query3 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=4";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + "," + statusid + ",TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveYardorWhIndetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveWhIndetails-Insert KT_CTYRDWH_OPDTLS: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                if (isReason != "Yes")
                {
                    classServerConnections.OracleCMD.CommandText = Query2;
                    classLog.writeLog("Message @: KPCT Operations-SaveWhIndetails-Insert KT_TRUCKTRACKER: " + Query2.ToString());
                    classServerConnections.OracleCMD.ExecuteNonQuery();
                }
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveWhIndetails-Update KT_STAGETRACKER: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveWhIndetails-Insert KT_STAGETRACKER: " + Query4.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = SaveActivityStartdetails(truckid, user, savetime);
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save CT WH In Details --End--

        //Save CT WH Out Details --Start--
        public static string SaveWhOutdetails(string truckid, string OpQty, string OPLOC, string OPLOCTYPE, string user, string savetime, string CargoCondition, string WeatherCondition, string OpHandledBy, string OpHandledType, string TallySheetType,string ContainerNo )
        {
            string OpStatus = "";
            string sts = SaveActivityEnddetails(truckid, OpQty, OPLOC, OPLOCTYPE, user, savetime, CargoCondition, WeatherCondition, OpHandledBy, OpHandledType, TallySheetType,ContainerNo);
            try
            {
                string Query = "Update KPCT.KT_CTYRDWH_OPDTLS set CTYRDWH_OUT_DATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",5,'Truck From WH to SDS Out','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=5";
                string Query3 = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=6,MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + ",6,TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                //classLog.writeLog("Message @: KPCT Operations-SaveYardorWhOutdetails-Update KT_CTYRDWH_OPDTLS: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                //classServerConnections.OracleCMD.CommandText = Query;
                //classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveWhOutdetails-Insert KT_TRUCKTRACKER: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query2;
                classLog.writeLog("Message @: KPCT Operations-SaveWhIndetails-Update KT_STAGETRACKER: " + Query2.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveWhOutdetails-Update KT_CFMS_JO_DTLS_MST: " + Query.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveWhOutdetails-Insert KT_STAGETRACKER: " + Query.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save CT WH Out Details --End--

        //Save KPCT Out Details --Start--
        public static string SaveKPCTOutdetails(string truckid, string user, string savetime)
        {
            string OpStatus = "";
            try
            {
                string Query = "Update KPCT.KT_SDS_PRKINGDTLS set KPCT_OUT_FROM='KPCT ',KPCT_OUT_DATETIME=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + "";
                string Query1 = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + ",7,'Truck from KPCT Out to Final Transit','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                string Query2 = "Update KPCT.KT_STAGETRACKER set OUT_DATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where TRUCKID=" + truckid + " and STAGEID=7";
                string Query3 = "Update KPCT.KT_CFMS_JO_DTLS_MST set STATUSID=8,MODIFIEDBY='" + user + "',MODIFIEDDATE=TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS') where ID=" + truckid + "";
                string Query4 = "Insert into KPCT.KT_STAGETRACKER(TRUCKID,STAGEID,IN_DATE,CREATEDBY,CREATEDDATE)" + "Values(" + truckid + ",8,TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'),'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classLog.writeLog("Message @: KPCT Operations-SaveKPCTOutdetails-Update KT_CTYRDWH_OPDTLS: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveKPCTOutdetails-Insert KT_TRUCKTRACKER: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query2;
                classLog.writeLog("Message @: KPCT Operations-SaveKPCTOutdetails-Update KT_STAGETRACKER: " + Query2.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();                
                classServerConnections.OracleCMD.CommandText = Query3;
                classLog.writeLog("Message @: KPCT Operations-SaveKPCTOutdetails-Update KT_CFMS_JO_DTLS_MST: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracleCMD.CommandText = Query4;
                classLog.writeLog("Message @: KPCT Operations-SaveKPCTOutdetails-Insert KT_STAGETRACKER: " + Query3.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
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
        //Save KPCT Out Details --End--
        #endregion

        #region Operations-Fetch Truck TallySheet   --  Oracle
        public static DataSet FetchTruckTallySheetDetailsOracle(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                DataSet ds1 = new DataSet();
                string Query = "Select TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                
                classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                da1.Fill(ds1);
                string TruckId=ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                string Query1 = " select * from KPCT.VW_TALLYSHEETDTLS where TRUCKID=" + TruckId + " ";
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-Get Truck TallySheet Details: " + Query1.ToString());
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                da.Fill(ds);
                classServerConnections.OracledbCon.Close();
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
        public static DataSet FetchTruckTallySheetDetailsOracleNew(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                DataSet ds1 = new DataSet();
                string Query = "Select TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";

                classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                da1.Fill(ds1);
                string TruckId = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                string Query1 = " select TRUCKID,WIDTH,HEIGHT,COLTOTAL from KPCT.KT_SRVR_TALLYSHEET where TRUCKID=" + TruckId + " ";
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-Get Truck TallySheet Details: " + Query1.ToString());
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                da.Fill(ds);
                classServerConnections.OracledbCon.Close();
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
        public static DataSet FetchTruckTallySheetLocationDetailsOracle(string tagno)
        {
            DataSet ds = new DataSet();
            try
            {
                DataSet ds1 = new DataSet();
                string Query = "Select TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                da1.Fill(ds1);
                string TruckId = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                string Query1 = " select CTYRDWH_LOCID,CARGO_CONID,WEATHER_CONID,HANDLDBY_CMPNYID,HANDLINGTYPE,TALLYSHEET_TYPE,CONTAINER_NO from KPCT.KT_CTYRDWH_OPDTLS where TRUCKID=" + TruckId + " ";
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-Get Truck TallySheet Location Details: " + Query1.ToString());
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                da.Fill(ds);
                classServerConnections.OracledbCon.Close();
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

        #region Operations-Insert Truck TallySheet   --  Oracle
        public static string InsertTruckTallySheetDetailsOracle(string tagno, string Val1, string Val2, string Val3, string Val4, string Val5, string Val6, string Val7, string Val8, string Val9, string user, string savetime)
        {
            classLog.writeLog("Message @: KPCT Operations-TallySheet Details: " + tagno.ToString() + Val1.ToString() + Val2.ToString() + Val3.ToString() + Val4.ToString() + Val5.ToString() + Val6.ToString() + Val7.ToString() + Val8.ToString() + Val9.ToString()+user.ToString()+savetime.ToString());
            string OpStatus = "";
            try
            {
                DataSet ds1 = new DataSet();
                string Query = "Select TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                da1.Fill(ds1);
                string TruckId = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                int coltotal = Int32.Parse(Val1) + Int32.Parse(Val2) + Int32.Parse(Val3) + Int32.Parse(Val4) + Int32.Parse(Val5) + Int32.Parse(Val6) + Int32.Parse(Val7) + Int32.Parse(Val8) + Int32.Parse(Val9);
                string Query1 = "Insert into KPCT.KT_TALLYSHEETDTLS(ID,TRUCKID,COL1,COL2,COL3,COL4,COL5,COL6,COL7,COL8,COL9,COLTOTAL,CREATEDBY,CREATEDDATE)" + "Values(KPCT.KT_TALLYSHEETDTLS_SEQ.NEXTVAL," + TruckId + "," + Val1 + "," + Val2 + "," + Val3 + "," + Val4 + "," + Val5 + "," + Val6 + "," + Val7 + "," + Val8 + "," + Val9 + "," + coltotal + ",'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveTruckId for TallySheet Details-Insert KT_TALLYSHEETDTLS: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();
                
                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: Insert Truck TallySheet Details : " + ex.ToString());
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
        public static string InsertTruckTallySheetDetailsOracleNew(string tagno, string width, string hight, string ctotal, string user, string savetime)
        {
            classLog.writeLog("Message @: KPCT Operations-TallySheet Details: " + tagno.ToString() + width.ToString() + hight.ToString() + ctotal.ToString()  + user.ToString() + savetime.ToString());
            string OpStatus = "";
            try
            {
                DataSet ds1 = new DataSet();
                string Query = "Select TRUCKID from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                da1.Fill(ds1);
                string TruckId = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                //int coltotal = Int32.Parse(Val1) + Int32.Parse(Val2) + Int32.Parse(Val3) + Int32.Parse(Val4) + Int32.Parse(Val5) + Int32.Parse(Val6) + Int32.Parse(Val7) + Int32.Parse(Val8) + Int32.Parse(Val9);
                string Query1 = "Insert into KPCT.KT_SRVR_TALLYSHEET(ID,TRUCKID,WIDTH,HEIGHT,COLTOTAL,CREATEDBY,CREATEDDATE)" + "Values(KPCT.KT_SRVR_TALLYSHEET_SEQ.NEXTVAL," + TruckId + "," + width + "," + hight + "," + ctotal + ",'" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                classServerConnections.OracleCMD.CommandText = Query1;
                classLog.writeLog("Message @: KPCT Operations-SaveTruckId for TallySheet Details-Insert KT_SRVR_TALLYSHEET: " + Query1.ToString());
                classServerConnections.OracleCMD.ExecuteNonQuery();

                classServerConnections.OracledbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch (Exception ex)
            {
                OpStatus = "SAVEFAILED";
                classLog.writeLog("Error @: Insert Truck TallySheet Details : " + ex.ToString());
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

        #region Operations-Fetch TallySheetMandatory Details -- Oracle
        public static string isTallySheetMandatory(string tagno)
        {
            string isRequired = "";
             try
            {
            DataSet ds = new DataSet();
            string Query = "Select TRUCKID,NEXTSTATUS from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
            classLog.writeLog("Message @: KPCT Operations-Get TruckId for TallySheet Details: " + Query.ToString());
            classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
            classServerConnections.OracledbCon.Open();
            classServerConnections.OracleCMD.CommandText = Query;
            OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
            da.Fill(ds);
            string TruckId = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            string Operation = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            string Query1 = "Select OP_LOCTYPE_WHCTYARD from KPCT.KT_SDS_PRKINGDTLS where TRUCKID=" + TruckId + "";
            classLog.writeLog("Message @: KPCT Operations-Get Location Type for TallySheet Validation: " + Query1.ToString());
            classServerConnections.OracleCMD.CommandText = Query1;
            OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            string LocType = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
            if (Operation == "ACTIVITY END" && LocType == "1") isRequired = "Yes";//Warehouse
            else if (Operation == "ACTIVITY END" && LocType == "2") isRequired = "No";//Yard
            else if (Operation == "CT-YARD/WH OUT") isRequired = "Yes"; else isRequired = "No";
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
            return isRequired;
        }
        #endregion

        #region Operations-Resons capturing     --  Oracle
        public static string ResonDetailsNeededorNot(string tagno)
        {
            string RemarksNeedStatus = "";
            try
            {
                string Query = "Select TRUCKNO,NEXTSTATUS,TRUCKID,FRSTTRANSIT_FLAG,DBPARKING_FLAG,SCNDTRANSIT_FLAG,ACTIVITYLOC_FLAG,SDSOUT_FLAG,KPCTOUT_FLAG,FINALTRANSIT_FLAG from KPCT.VW_TRUCKDTLS where TAGNO='" + tagno + "'";
                classLog.writeLog("Message @: KPCT Operations-Get Truck Details: " + Query.ToString());
                classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                classServerConnections.OracledbCon.Open();
                classServerConnections.OracleCMD.CommandText = Query;
                OracleDataAdapter da = new OracleDataAdapter(classServerConnections.OracleCMD);
                classServerConnections.OracledbCon.Close();
                DataSet ds = new DataSet();
                da.Fill(ds);
                string nextstatus = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                string truckid = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                if (nextstatus == "PARKING IN")
                {
                    if (ds.Tables[0].Rows[0].ItemArray[3].ToString() == "C" || ds.Tables[0].Rows[0].ItemArray[3].ToString() == "W")
                    {
                        RemarksNeedStatus = "Needed";
                    }
                    else RemarksNeedStatus = "NotNeeded";
                }
                else if (nextstatus == "PARKING OUT")
                {
                    if (ds.Tables[0].Rows[0].ItemArray[4].ToString() == "C" || ds.Tables[0].Rows[0].ItemArray[4].ToString() == "W")
                    {
                        //string Query1 = "Select REASONID from KPCT.KT_TRUCKTRACKER where TRUCKID=" + truckid + " and STATUSID=3";
                        //classLog.writeLog("Message @: KPCT Operations-Get Truck Remarks Details: " + Query1.ToString());
                        //classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                        //classServerConnections.OracledbCon.Open();
                        //classServerConnections.OracleCMD.CommandText = Query1;
                        //OracleDataAdapter da1 = new OracleDataAdapter(classServerConnections.OracleCMD);
                        //classServerConnections.OracledbCon.Close();
                        //DataSet ds1 = new DataSet();
                        //da1.Fill(ds1);
                        //if (ds1.Tables[0].Rows[0].ItemArray[0].ToString().Trim() == "") RemarksNeedStatus = "Needed";
                        //else RemarksNeedStatus = "NotNeeded";
                        RemarksNeedStatus = "Needed";
                    }
                    else RemarksNeedStatus = "NotNeeded";
                }
                else if (nextstatus == "CT-YARD IN" || nextstatus == "WH-ACTIVITY START")
                {
                    if (ds.Tables[0].Rows[0].ItemArray[5].ToString() == "C" || ds.Tables[0].Rows[0].ItemArray[5].ToString() == "W")
                    {
                        RemarksNeedStatus = "Needed";
                    }
                    else RemarksNeedStatus = "NotNeeded";
                }
                else if (nextstatus == "CT-ACTIVITY START")
                {
                    if (ds.Tables[0].Rows[0].ItemArray[6].ToString() == "C" || ds.Tables[0].Rows[0].ItemArray[6].ToString() == "W")
                    {
                        RemarksNeedStatus = "Needed";
                    }
                    else RemarksNeedStatus = "NotNeeded";
                }
                else if (nextstatus == "KPCT OUT")
                {
                    if (ds.Tables[0].Rows[0].ItemArray[8].ToString() == "C" || ds.Tables[0].Rows[0].ItemArray[8].ToString() == "W")
                    {
                        RemarksNeedStatus = "Needed";
                    }
                    else RemarksNeedStatus = "NotNeeded";
                }
                else RemarksNeedStatus = "NotNeeded";
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: KPCT Operations-Get Truck Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }
            return RemarksNeedStatus;
        }
        public static string isReason = "";
        public static string SaveResonDetails(string tagno, string readerno, string saveoperation, string savetime, string user, string usertype, string HHDlocation, string Qty, string OPLOC, string reasonid, string reasondesc)
        {       isReason="Yes";
            string OpStatus = "FAILED";
            try
            {
                OpStatus = InsertKPCTOperationDetailsOracle(tagno, saveoperation, readerno, savetime, user, usertype, HHDlocation, Qty, OPLOC,"","","","","","");
                if (OpStatus == "SUCCESS")
                {
                    DataSet ds = new DataSet();
                    ds = FetchTruckDetailsOracle(tagno, usertype, HHDlocation,readerno,user);
                    string truckno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string truckid = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                    string statusid = "0"; string Description = ""; classLog.writeLog("Message @: KPCT Operations-Save Operation: " + saveoperation.ToString());
                    if (saveoperation == "PARKING IN") { statusid = "2"; Description = "Truck From 1st Transit to Parking"; }
                    else if (saveoperation == "PARKING OUT") { statusid = "3"; Description = "Truck From Parking to 2nd Transit"; }
                    else if (saveoperation == "CT-YARD IN" || saveoperation == "WH-ACTIVITY START") { statusid = "4"; Description = "Truck From 2nd Transit to CTYARD/WH"; }
                    else if (saveoperation == "CT-ACTIVITY START") { statusid = "5"; Description = "Truck Activity Start"; }
                    else if (saveoperation == "KPCT OUT") { statusid = "7"; Description = "Truck From SDS Out to KPCT Out"; }
                    string Query = "Insert into KPCT.KT_TRUCKTRACKER(TRUCKID,STATUSID,REASONID,REASONDESC,DESCRIPTION,PROCESSEDBY,PROCESSEDDATE)" + "Values(" + truckid + "," + statusid + "," + reasonid + ",'" + reasondesc + "','" + Description + "','" + user + "',TO_DATE ('" + savetime + "','DD-Mon-YYYY HH24:MI:SS'))";
                    classLog.writeLog("Message @: KPCT Operations-Save Truck Tracker Reason Log: " + Query.ToString());
                    classServerConnections.OracleCMD.Connection = classServerConnections.OracledbCon;
                    classServerConnections.OracledbCon.Open();
                    classServerConnections.OracleCMD.CommandText = Query;
                    classServerConnections.OracleCMD.ExecuteNonQuery();
                    classServerConnections.OracledbCon.Close();

                    OpStatus = "SUCCESS";
                }
                
            }
            catch (Exception ex)
            {
                OpStatus = "FAILED";
                classLog.writeLog("Error @: KPCT Operations-Save HHDLog: " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.OracleCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.OracleCMD.Connection.Close();
                }
            }

            isReason = "No";
            return OpStatus;
        }
        #endregion
        //----END---Oracle Section--------

        #region Get Location Name details from -- CFMS SQL DB
        public static string getLocationDetailsCFSSQL(string LocationId)
        {
            string Location = "";
            try
            {
                string Query = "";
                Query = "Select [Id],[Code],[Name] from [dbo].[TBL_GRID_LOCATION_MASTER] where [Id]=" + LocationId + " ";
                classLog.writeLog("Message @: Location Master Details : " + Query.ToString());
                classServerConnections.CFSSQLCMD.Connection = classServerConnections.CFSSQLdbCon;
                classServerConnections.CFSSQLdbCon.Open();
                classServerConnections.CFSSQLCMD.CommandText = Query;
                SqlDataAdapter da = new SqlDataAdapter(classServerConnections.CFSSQLCMD);
                classServerConnections.CFSSQLdbCon.Close();
                DataSet ds = new DataSet();
                da.Fill(ds);
                //Location = ds.Tables[0].Rows[0].ItemArray[1].ToString() + "-" + ds.Tables[0].Rows[0].ItemArray[2].ToString();
                Location =  ds.Tables[0].Rows[0].ItemArray[2].ToString();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @: Get Location Details : " + ex.ToString());
            }
            finally
            {
                if (classServerConnections.CFSSQLCMD.Connection.State != ConnectionState.Closed)
                {
                    classServerConnections.CFSSQLCMD.Connection.Close();
                }
            }
            return Location;
        }
        #endregion

        //Insert Transaction Denied Reader Log Details --Start--
        public static string InsertKPCTOperationDeniedLogDetailsOracle(string truckid, string currentstage, string readerno, string Location, string user)
        {
            string Desc = "";
            string OpStatus = "";
            try
            {
                string Query = "Insert into KPCT.KT_TRUCK_READLOG(TRUCKID,CURRENT_STAGE,DEVICEID,READ_LOC,DESCRIPTION,CREATEDBY,CREATEDDATE)" + "Values(" + readerno + ",'" + currentstage + "','" + readerno + "','" + Location + "','" + Desc + "','" + user + "',SYSDATE)";
                classLog.writeLog("Message @: KPCT Operations-Save Transaction Denied Log: " + Query.ToString());
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
                classLog.writeLog("Error @: KPCT Operations-Save Transaction Denied Log: " + ex.ToString());
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
        //Insert Transaction Denied Reader Log Details --End--
    }
}
