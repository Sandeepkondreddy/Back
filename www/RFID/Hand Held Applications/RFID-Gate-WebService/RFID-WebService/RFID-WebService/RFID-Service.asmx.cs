/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-WebService
Purpose:	        RFID-HHD Oracle Comminication
Created Date:		19 Aug 2015 
Updated Date:		14 Dec 2015  
******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Oracle.DataAccess.Client;
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
        
        #region HHD Config-Fetch Reader Configuaration Details
        [WebMethod]
        public DataSet GetReaderDetails(string ReaderIP)
        {
            DataSet DSVal = classReaderConfiguration.FetchReaderDetails(ReaderIP);
            return DSVal;
        }
        #endregion

        #region Validate Gateregister-Tag Free Details
        [WebMethod]
        public int Tagfree(string tagno)
        {
            int count = classGateRegister.ValidateTagfree(tagno);
            return count; 
        }

        #endregion

        #region Validate Gateregister-Truck Free Details
        [WebMethod]
        public int Truckfree(string truckno)
        {
            int count = classGateRegister.ValidateTruckfree(truckno);
            return count;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details
        [WebMethod]
        public string InsertRegDetails(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            string inserstatus = classGateRegister.InsertTagRegDetails(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region Gateregister-Insert Tag Register Details Sync
        [WebMethod]
        //public string InsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime,string tstaus)
        public string InsertRegDetailsSync(string tagno, string truckno, string optype, string readerno, string readerip, string user, string trucktype, string remarks, string regtime)
        {
            //string inserstatus = classGateRegister.InsertTagRegDetailsSync(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime, tstaus);
            string inserstatus = classGateRegister.InsertTagRegDetailsSync(tagno, truckno, optype, readerno, readerip, user, trucktype, remarks, regtime);
            return inserstatus;
        }

        #endregion

        #region GateUnRegister-Fetch Truck Details
        [WebMethod]
        public DataSet GetTruckDetails(string tagno)
        {
            DataSet DSVal = classGateUnRegister.FetchTruckDetails(tagno);
            return DSVal;
        }


        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details
        [WebMethod]
        public string InsertUnRegDetails(string regid, string tagno, string truckno, string trucktype, string readerno, string readerip, string user,  string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetails(regid, tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region GateUnRegister-Insert Tag UnRegister Details Sync
        [WebMethod]
        public string InsertUnRegDetailsSync(string tagno, string truckno, string trucktype, string readerno, string readerip, string user, string unregtime)
        {
            string inserstatus = classGateUnRegister.InsertTagUnRegDetailsSync(tagno, truckno, trucktype, readerno, readerip, user, unregtime);
            return inserstatus;
        }
        #endregion

        #region Login-Fetch DB Time Details
        [WebMethod]
        public DataSet GetDBTime()
        {
            DataSet DSVal = classDBTime.FetchDBTime();
            return DSVal;
        }
        #endregion

        #region HHD Master-Fetch Master Details
        [WebMethod]
        public DataTable GetMasterDetails(string getDetails)
        {
            DataTable DtVal = classMasterData.getMasterDetails(getDetails);
            return DtVal;
        }
        #endregion
    }
}
