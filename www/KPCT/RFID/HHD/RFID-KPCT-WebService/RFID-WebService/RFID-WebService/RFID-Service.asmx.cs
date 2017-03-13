/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication
Created Date:		30 June 2016 
Updated Date:		30 June 2016  
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
namespace RFID_WebService
{
    [WebService(Namespace = "http://krishnapatnamport.nlr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Service1 : System.Web.Services.WebService
    {
        #region "[ Connection String ]"
        public static string CONN_STRING = classServerConnections.OracleConnection;
        OracleConnection dbCon = new OracleConnection(CONN_STRING); OracleCommand cmd = new OracleCommand();
        #endregion

        #region Test DB Connection
        [WebMethod]
        public string TestInsertDetails(string empno, string name)
        {
            string strRtnVal = InsertUserDetails(empno,name);
            return strRtnVal;

        }

        string InsertUserDetails(string empno, string empname)
        {
            string OpStatus = "";
            try
            {
                string Query = "Insert into APPL.TESTTBL (USERNAME,EPMNO) Values('" + empname + "'," + empno + ")";
                cmd.Connection = dbCon;
                dbCon.Open();
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();
                dbCon.Close();
                OpStatus = "SUCCESS";
            }
            catch
            {
                OpStatus = "FAILED";
            }
            return OpStatus;
        }
        #endregion

        //----Start---Oracle Section--------
        #region HHD Config-Fetch Reader Configuaration Details  -- Oracle
        [WebMethod]
        public DataSet GetReaderDetailsOracle(string ReaderIP)
        {
            DataSet DSVal = classReaderConfiguration.FetchReaderDetailsOracle(ReaderIP);
            return DSVal;
        }
        #endregion

        #region HHD Master-Fetch Master Details  -- Oracle
        [WebMethod]
        public DataTable GetMasterDetailsOracle(string getDetails)
        {
            DataTable DtVal = classMasterData.getMasterDetailsOracle(getDetails);
            return DtVal;
        }
        #endregion 

        #region Validate Gateregister-Tag Free Details  -- Oracle
        [WebMethod]
        public int TagfreeOracle(string tagno)
        {
            int count = classGateRegister.ValidateTagfreeOracle(tagno);
            return count; 
        }

        #endregion

        #region Validate Gateregister-Truck Free Details  -- Oracle
        [WebMethod]
        public int TruckfreeOracle(string truckno)
        {
            int count = classGateRegister.ValidateTruckfreeOracle(truckno);
            return count;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details  -- Oracle
        [WebMethod]
        public string InsertRegDetailsOracle(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string inserstatus = classGateRegister.InsertTagRegDetailsOracle(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details Sync  -- Oracle
        [WebMethod]
        //public string InsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime,string tstaus)
        public string InsertRegDetailsSyncOracle(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            //string inserstatus = classGateRegister.InsertTagRegDetailsSync(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime, tstaus);
            string inserstatus = classGateRegister.InsertTagRegDetailsSyncOracle(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region GateUnRegister-Fetch Truck Details  -- Oracle
        [WebMethod]
        public DataSet GetTruckDetailsOracle(string tagno)
        {
            DataSet DSVal = classGateUnRegister.FetchTruckDetailsOracle(tagno);
            return DSVal;
        }
        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details  -- Oracle
        [WebMethod]
        public string InsertUnRegDetailsOracle(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user,  string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetailsOracle(regid, tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details Sync  -- Oracle
        [WebMethod]
        public string InsertUnRegDetailsSyncOracle(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetailsSyncOracle(tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region Login-Fetch DB Time Details  -- Oracle
        [WebMethod]
        public DataSet GetDBTimeOracle()
        {
            DataSet DSVal = classDBTime.FetchDBTimeOracle();
            return DSVal;
        }
        #endregion
        
        #region YardOperations-Fetch Truck Details  -- Oracle
        [WebMethod]
        public DataSet GetTruckDetailsforYardOperationsOracle(string tagno)
        {
            DataSet DSVal = classYardOperations.FetchTruckDetailsOracle(tagno);
            return DSVal;
        }
        #endregion

        #region YardOperations-Insert Yard Operation Details  -- Oracle
        [WebMethod]
        public string InsertYardOperationDetailsOracle(string tagno, string truckno, string loaderid, string readerno,  string readerip, string user, string yardoptime)
        {
            string inserstatus = classYardOperations.InsertYardOperationDetailsOracle(tagno, truckno, loaderid, readerno, readerip, user, yardoptime);
            return inserstatus;
        }
        #endregion

        #region YardOperations-Insert Yard Operation Details Sync  -- Oracle
        [WebMethod]
        public string InsertYardOperationDetailsSyncOracle(string tagno, string truckno, string loaderid, string readerno, string readerip, string user, string yardoptime)
        {
            string inserstatus = classYardOperations.InsertYardOperationDetailsOracle(tagno, truckno, loaderid, readerno, readerip, user, yardoptime);
            return inserstatus;
        }
        #endregion


        #region KPCT Operations-Fetch Truck Details  -- Oracle
        [WebMethod]
        public DataSet GetKPCTTruckDetailsOracle(string tagno,string usertype,string HHDlocation,string readerno,string user)
        {
            DataSet DSVal = classSDSOperations.FetchTruckDetailsOracle(tagno, usertype, HHDlocation, readerno,user);
            return DSVal;
        }
        #endregion

        #region KPCT Operations-Insert KPCT Operation Details  -- Oracle
        [WebMethod]
        public string InsertKPCTOperationDetailsOracle(string tagno, string readerno, string saveoperation, string savetime, string user, string usertype, string HHDlocation, string Qty, string OPLOC, string CargoCondition, string WeatherCondition, string OpHandledBy, string OpHandledType, string TallySheetType,string ContainerNo)
        {
            string inserstatus = classSDSOperations.InsertKPCTOperationDetailsOracle(tagno, saveoperation, readerno, savetime, user, usertype, HHDlocation, Qty, OPLOC, CargoCondition, WeatherCondition, OpHandledBy, OpHandledType, TallySheetType, ContainerNo);
            return inserstatus;
        }
        #endregion

        #region KPCT Operations-Fetch Truck TallySheet Details  -- Oracle
        [WebMethod]
        public DataSet GetKPCTTruckTallySheetDetailsOracle(string tagno)
        {
            DataSet DSVal = classSDSOperations.FetchTruckTallySheetDetailsOracle(tagno);
            return DSVal;
        }
        #endregion

        #region KPCT Operations-Fetch Truck TallySheet Details New  -- Oracle
        [WebMethod]
        public DataSet GetKPCTTruckTallySheetDetailsOracleNew(string tagno)
        {
            DataSet DSVal = classSDSOperations.FetchTruckTallySheetDetailsOracleNew(tagno);
            return DSVal;
        }
        #endregion
        
        #region KPCT Operations-Fetch Truck TallySheet Validation Details  -- Oracle
        [WebMethod]
        public string  GetKPCTTruckTallySheetValidationDetailsOracle(string tagno)
        {
            string DSVal = classSDSOperations.isTallySheetMandatory(tagno);
            return DSVal;
        }
        #endregion

        #region KPCT Operations-Fetch Truck TallySheet Location Details  -- Oracle
        [WebMethod]
        public DataSet GetKPCTTruckTallySheetLocationDetailsOracle(string tagno)
        {
            DataSet DSVal = classSDSOperations.FetchTruckTallySheetLocationDetailsOracle(tagno);
            return DSVal;
        }
        #endregion

        #region KPCT Operations-Save Truck TallySheet Details  -- Oracle
        [WebMethod]
        public string InsertKPCTTruckTallySheetDetailsOracle(string Val1, string Val2, string Val3, string Val4, string Val5, string Val6, string Val7, string Val8, string Val9,string tagno,string user,string savetime)
        {
            
            string inserstatus = classSDSOperations.InsertTruckTallySheetDetailsOracle(tagno, Val1, Val2, Val3, Val4, Val5, Val6, Val7, Val8, Val9, user, savetime);
            return inserstatus;
        }
        #endregion

        #region KPCT Operations-Save Truck TallySheet Details New  -- Oracle
        [WebMethod]
        public string InsertKPCTTruckTallySheetDetailsOracleNew(string tagno, string width, string hight, string ctotal, string user, string savetime)
        {

            string inserstatus = classSDSOperations.InsertTruckTallySheetDetailsOracleNew(tagno, width, hight, ctotal, user, savetime);
            return inserstatus;
        }
        #endregion

        #region KPCT Operations-Fetch Truck Reason Details Required or Not -- Oracle
        [WebMethod]
        public string GetKPCTTruckReasonDetailsRequirementOracle(string tagno)
        {
            string ReasonStatus = classSDSOperations.ResonDetailsNeededorNot(tagno);
            return ReasonStatus;
        }
        #endregion

        #region KPCT Operations-Insert Truck Reason Details -- Oracle
        [WebMethod]
        public string InsertReasonDetailsOracle(string tagno, string readerno, string saveoperation, string savetime, string user, string usertype, string HHDlocation, string Qty, string OPLOC, string reasonid, string reasondesc)
        {
            string ReasonStatus = classSDSOperations.SaveResonDetails(tagno, readerno, saveoperation, savetime, user, usertype, HHDlocation, Qty, OPLOC, reasonid, reasondesc);
            return ReasonStatus;
        }
        #endregion
        //----END---Oracle Section--------


        //----Start---CFS SQL Section--------
        #region HHD Master-Fetch Location Master Details  -- CFS SQL DB
        [WebMethod]
        public DataTable GetLocationMasterDetailsCFSSQL(string getDetails)
        {
            DataTable DtVal = classReaderConfiguration.getLocationMasterDetailsCFSSQL(getDetails);
            return DtVal;
        }
        #endregion 

        #region HHD Master-Fetch Location Details  -- CFS SQL DB
        [WebMethod]
        public String  GetLocationDetailsCFSSQL(string LocationId)
        {
            String  Loc = classSDSOperations.getLocationDetailsCFSSQL(LocationId);
            return Loc;
        }
        #endregion 
        //----End---CFS SQL Section--------



        //----Start---SQL Section--------
        #region Login-Fetch DB Time Details  -- SQL
        [WebMethod]
        public DataSet GetDBTimeSQL()
        {
            DataSet DSVal = classDBTime.FetchDBTimeSQL();
            return DSVal;
        }
        #endregion

        #region HHD Config-Fetch Reader Configuaration Details  -- SQL
        [WebMethod]
        public DataSet GetReaderDetailsSQL(string ReaderIP)
        {
            DataSet DSVal = classReaderConfiguration.FetchReaderDetailsSQL(ReaderIP);
            return DSVal;
        }
        #endregion

        #region HHD Master-Fetch Master Details  -- SQL
        [WebMethod]
        public DataTable GetMasterDetailsSQL(string getDetails)
        {
            DataTable DtVal = classMasterData.getMasterDetailsSQL(getDetails);
            return DtVal;
        }
        #endregion

        #region Validate Gateregister-Tag Free Details  -- SQL
        [WebMethod]
        public int TagfreeSQL(string tagno)
        {
            int count = classGateRegister.ValidateTagfreeSQL(tagno);
            return count;
        }

        #endregion

        #region Validate Gateregister-Truck Free Details  -- SQL
        [WebMethod]
        public int TruckfreeSQL(string truckno)
        {
            int count = classGateRegister.ValidateTruckfreeSQL(truckno);
            return count;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details  -- SQL
        [WebMethod]
        public string InsertRegDetailsSQL(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string inserstatus = classGateRegister.InsertTagRegDetailsSQL(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details Sync  -- SQL
        [WebMethod]
        //public string InsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime,string tstaus)
        public string InsertRegDetailsSyncSQL(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            //string inserstatus = classGateRegister.InsertTagRegDetailsSync(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime, tstaus);
            string inserstatus = classGateRegister.InsertTagRegDetailsSyncSQL(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region GateUnRegister-Fetch Truck Details  -- SQL
        [WebMethod]
        public DataSet GetTruckDetailsSQL(string tagno)
        {
            DataSet DSVal = classGateUnRegister.FetchTruckDetailsSQL(tagno);
            return DSVal;
        }
        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details  -- SQL
        [WebMethod]
        public string InsertUnRegDetailsSQL(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetailsSQL(regid, tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details Sync  -- SQL
        [WebMethod]
        public string InsertUnRegDetailsSyncSQL(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetailsSyncSQL(tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region YardOperations-Fetch Truck Details  -- SQL
        [WebMethod]
        public DataSet GetTruckDetailsforYardOperationsSQL(string tagno)
        {
            DataSet DSVal = classYardOperations.FetchTruckDetailsSQL(tagno);
            return DSVal;
        }
        #endregion

        #region YardOperations-Insert Yard Operation Details  -- SQL
        [WebMethod]
        public string InsertYardOperationDetailsSQL(string tagno, string truckno, string loaderid, string readerno, string readerip, string user, string yardoptime)
        {
            string inserstatus = classYardOperations.InsertYardOperationDetailsSQL(tagno, truckno, loaderid, readerno, readerip, user, yardoptime);
            return inserstatus;
        }
        #endregion

        #region YardOperations-Insert Yard Operation Details Sync  -- SQL
        [WebMethod]
        public string InsertYardOperationDetailsSyncSQL(string tagno, string truckno, string loaderid, string readerno, string readerip, string user, string yardoptime)
        {
            string inserstatus = classYardOperations.InsertYardOperationDetailsSQL(tagno, truckno, loaderid, readerno, readerip, user, yardoptime);
            return inserstatus;
        }
        #endregion
        //----End---SQL Section--------

    }
}
