////Not in Use 
//PortEntry and for General
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Data.SqlClient;
namespace CS_RFID3_Host_Sample1
{
    public partial class PortEntryandGeneralReader : Form
    {
        int PFlag = 0, TruckFlag = 0;
        string TruckType = "IN";
        SqlCommand Sqlcmd;
        SqlConnection Sqlcon;
        SqlConnection SqlconInternal;
        static string Sqlstr = Class_ProperityLayer.SqlConn_String;
        static string Sqlstr1 = Class_ProperityLayer.SqlConn_String_Internal;
        string SysIP = ConfigurationManager.AppSettings["systemip"];
        #region " PortEntry Variable Decleration "

        string strSelectQuery_PortEntry = "";
        string strNonQuery_PortEntry = "";
        string strTableName_PortEntry = "";
        string strScalarResult_PortEntry = "";
        string strErrorMessage_PortEntry = "";

        bool blnIsSuccess_PortEntry = false;
        bool blnIsExist_PortEntry = false;
        int fixant = 0;
        int errorflag = 0;

        //RFID-Reader at PortEntry
        RFIDReader PortEntry_readerAPI;

        string strPortEntryIPaddress = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderIPAddress").ToString();
        string strPortEntryPort = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderPort").ToString();

        string strPortEntryAntennaIn = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaIn").ToString();
        string strPortEntryAntennaOut = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaOut").ToString();

        private delegate void UpdateUI_PortEntry(string stringPortEntryData);
        private UpdateUI_PortEntry PortEntry_UpdateUIHandler = null;
        private TagData PortEntry_ReadTag = null;

        #endregion

        #region Network Connection
        Ping ping = new Ping();
        PingOptions options = new PingOptions { DontFragment = true };

        //just need some data. this sends 10 bytes.
        byte[] buffer = Encoding.ASCII.GetBytes(new string('z', 10));
        string host;
        int nwflag = 0;
        int ConnectErrLog = 0;
        int DisConnectErrLog = 0;
        #endregion
        public PortEntryandGeneralReader()
        {
            InitializeComponent();
            lblVersion.Text = Class_ProperityLayer.Version;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            host = strPortEntryIPaddress;
            tnrNetworkConnection.Enabled = true;
        }

        private void PortEntryandGeneralReader_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);
            SqlconInternal = new SqlConnection(Sqlstr1);
            lblReaderIP_PortEntry_Text.Text = strPortEntryIPaddress;
            MyDataCollection.ReaderIP = strPortEntryIPaddress;
            lblReaderPort_PortEntry_Text.Text = strPortEntryPort;

            lblAntenna_PortEntryIn_Text.Text = strPortEntryAntennaIn;
            lblAntenna_PortEntryOut_Text.Text = strPortEntryAntennaOut;

            lblWBIN.Text = ConfigurationManager.AppSettings["WBIN"];
            lblWBOUT.Text = ConfigurationManager.AppSettings["WBOUT"];

            Class_ProperityLayer.Ant1and21 = ConfigurationManager.AppSettings["ant1and2"];
            Class_ProperityLayer.Ant3and41 = ConfigurationManager.AppSettings["ant3and4"];

            this.Text = lblWBIN.Text + " and " + lblWBOUT.Text;
        }
        #region PortEntry
        //Reader Connection
        private void btnPortEntryAreaReaderConnect_Click(object sender, EventArgs e)
        {
            nwflag = 1;
            //tmrConnection.Enabled = true;
            if (tmrPortEntryArea.Enabled == false)
            {
                FunctionDisconnectReaderAtPortEntry();
                if (FunctionRFIDConnection_PortEntry())
                {

                    btnPortEntryAreaReaderDisconnect.Enabled = true;
                    btnPortEntryAreaReaderConnect.Enabled = false;
                    lblReaderStatus_PortEntry_Text.Text = "# Connected";
                }
                else
                    lblReaderStatus_PortEntry_Text.Text = "# Not Connected";
            }
        }
        //Reader Disconnect
        private void btnPortEntryAreaReaderDisconnect_Click(object sender, EventArgs e)
        {
            tmrPortEntryArea.Enabled = false;
            //tnrNetworkConnection.Enabled = false;
            //nwflag = 0;
            Thread.Sleep(1000);
            FunctionDisconnectReaderAtPortEntry();
            lblReaderStatus_PortEntry_Text.Text = "# Not Connected";
            btnPortEntryAreaReaderDisconnect.Enabled = false;
            btnPortEntryAreaReaderConnect.Enabled = true;
        }
        //check the connection status
        private void FunctionDisconnectReaderAtPortEntry()
        {
            try
            {
                if (PortEntry_readerAPI.IsConnected)
                    PortEntry_readerAPI.Disconnect();

            }
            catch (Exception)
            { }
        }
        //get the connection details
        public bool FunctionRFIDConnection_PortEntry()
        {
            try
            {
                PortEntry_readerAPI = new RFIDReader(strPortEntryIPaddress, Convert.ToUInt32(strPortEntryPort), 0);
                PortEntry_readerAPI.Connect();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Exception : Network Problem or Connection Already Exists...";
                btnPortEntryAreaReaderDisconnect.Enabled = false;
                btnPortEntryAreaReaderConnect.Enabled = true;
                return false;
            }
            try
            {
                if (PortEntry_readerAPI.IsConnected)
                {

                    PortEntry_readerAPI.Actions.TagAccess.OperationSequence.DeleteAll();

                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;

                    op.ReadAccessParams.ByteCount = 0;

                    PortEntry_readerAPI.Actions.TagAccess.OperationSequence.Add(op);
                    PortEntry_readerAPI.Actions.TagAccess.OperationSequence.PerformSequence();
                }

                tmrPortEntryArea.Enabled = true;
                nwflag = 0;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message.ToString());
                return false;
            }
        }
        //Timer run to get the tag data(Antenna no, tagid,....)
        private void tmrPortEntryArea_Tick(object sender, EventArgs e)
        {
            string WBNO;
            try
            {
                if (PortEntry_readerAPI.IsConnected)
                    lblReaderStatus_PortEntry_Text.Text = "# Connected";
                //else
                //lblReaderStatus_PortEntry_Text.Text = "# Not Connected";
            }
            catch (Exception)
            {
                lblReaderStatus_PortEntry_Text.Text = "# Not Connected";

            }

            try
            {
                Symbol.RFID3.TagData[] tagData = PortEntry_readerAPI.Actions.GetReadTags(1);

                if (tagData != null)
                {
                    string newtag, dstr;
                    AutoResetEvent AccessComplete;
                    AccessComplete = new AutoResetEvent(false);
                    int nIndex = 0;

                    //for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                    //{
                    if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                        (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                        tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
                    {
                        Symbol.RFID3.TagData tag = tagData[nIndex];
                        if (Class_ProperityLayer.DupTagNo1 == tagData[nIndex].TagID.Replace(" ", "").Substring(0, 5))
                        {
                            PortEntry_readerAPI.Actions.PurgeTags();
                            return;
                        }
                        else
                        {
                            string tagIDPortEntry = tag.TagID;
                            string tagAntennaID = tag.AntennaID.ToString();
                            Class_ProperityLayer.AntNo1 = tag.AntennaID;
                            string tagID = tag.TagID; // getting tagid here 113370000000000000000000
                            string tagno = tagData[nIndex].TagID;
                            string RSSI = tag.PeakRSSI.ToString();
                            string Tagno = tagno.Replace(" ", "").Substring(0, 5);

                            

                            Class_ProperityLayer.PTAGNO1 = Tagno;
                            string Ant = tag.AntennaID.ToString();
                            string LOC = ConfigurationManager.AppSettings["loc"];
                            //string Reader = ConfigurationManager.AppSettings["fixreaderip"];
                            string Reader = MyDataCollection.ReaderIP;
                            CatchData(string.Format("{0},{1},{2},{3},{4}", "DATA LOG", Tagno, Ant, RSSI, DateTime.Now));
                            int ActualRSSI=Convert.ToInt32(RSSI.ToString());
                            int AllowedRSSI=Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings.Get("RSSI").ToString());
                            if (ActualRSSI >= AllowedRSSI)
                            {
                                if (int.Parse(Ant) >= 1 && int.Parse(Ant) <= 2)
                                {
                                    //tagdate = DateTime.Now;
                                    fixant = 1;
                                    WBNO = ConfigurationManager.AppSettings["WBIN"];
                                    Class_ProperityLayer.PWBNO1 = WBNO;
                                    PFlag = 1;
                                    FunctionPortEntryIn(tagIDPortEntry, tagAntennaID);
                                    Class_ProperityLayer.DupTagNo1 = Tagno;//get the tagnum to restreict duplicate values
                                }
                                else
                                {
                                    fixant = 2;
                                    WBNO = ConfigurationManager.AppSettings["WBOUT"];
                                    Class_ProperityLayer.PWBNO1 = WBNO;
                                    PFlag = 2;
                                    FunctionPortEntryOut(tagIDPortEntry, tagAntennaID);
                                    Class_ProperityLayer.DupTagNo1 = Tagno;//get the tagnum to restreict duplicate values
                                }
                            }
                        }
                    }
                    //}
                }
            }
            catch (Exception)
            {
                //btnPortEntryAreaReaderDisconnect_Click(sender, e);
                //tmrPortEntryArea.Enabled = false;

            }
        }
        private static void CatchData(string msg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "DataLog.txt");
            strW.WriteLine(msg);
            strW.Close();
        }
        //is the tag read in WBIN
        private void FunctionPortEntryIn(string tagIDPortEntry, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_PortEntryIn_Text.Text = "";

            bool blnIsValid_PortEntry = false;
            if (Class_ProperityLayer.Ant1and21 == "PortEntry")
            {
                TruckType = "PE";
            }
            else if (Class_ProperityLayer.Ant1and21 == "General")
            {
                TruckType = "GNL";
            }
            blnIsSuccess_PortEntry = FunctionBlnValidTag_PortEntry(tagIDPortEntry, TruckType, out strErrorMessage_PortEntry, out blnIsValid_PortEntry);
            if (blnIsSuccess_PortEntry)
            {
                if (blnIsValid_PortEntry)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (SqlconInternal.State == ConnectionState.Closed || SqlconInternal.State == ConnectionState.Broken)
                    {
                        SqlconInternal.Open();
                    }
                    if (TruckType == "PE")
                    {
                        if (TruckFlag == 1)
                        {

                            string strNonQuery_PortEntry = "Insert into  [dbo].PORTENTRYTAGLOG (tagno,Ant,wbno,readdt,Reader,SysIP) Values ('" + Class_ProperityLayer.PTAGNO1 + "','" + tagAntennaID + "','" + Class_ProperityLayer.PWBNO1 + "',GETDATE(),'" + strPortEntryIPaddress + "','" + SysIP + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                        }
                    }
                    else if (TruckType == "GNL")
                    {
                        if (TruckFlag == 1)
                        {
                            //string strNonQuery_PortEntry = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, SqlconInternal);
                        }
                        else if (TruckFlag == 2)
                        {
                            string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                        }
                    }
                    int execquery = Sqlcmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntennaStatus_PortEntryIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and PortEntry-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_PortEntryIn_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_PortEntryIn_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        Thread.Sleep(1000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        lblAntenna_PortEntryIn_Text.Text = tagAntennaID;
                        Sqlcon.Close();
                        PortEntry_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_PortEntryIn_Text.Text = strErrorMessage_PortEntry;
                        //Application.DoEvents();
                        lblRFIDTagValue_PortEntryIn_Text.Text = string.Empty;
                        lblAntenna_PortEntryIn_Text.Text = string.Empty;
                        lblAntenna_PortEntryIn_Time_Text.Text = string.Empty;
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        PortEntry_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(1000);
                    }
                    //return true;
                }
                else
                {
                    lblAntennaStatus_PortEntryIn_Text.Text = "Invlid Tag ID '" + tagIDPortEntry + "'";
                    //Application.DoEvents();
                    lblRFIDTagValue_PortEntryIn_Text.Text = string.Empty;
                    lblAntenna_PortEntryIn_Text.Text = string.Empty;
                    lblAntenna_PortEntryIn_Time_Text.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDPortEntry, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    PortEntry_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(1000);
                }
                //return false;
            }
            else
            {
                lblAntennaStatus_PortEntryIn_Text.Text = strErrorMessage_PortEntry;
                //Application.DoEvents();
                lblRFIDTagValue_PortEntryIn_Text.Text = string.Empty;
                lblAntenna_PortEntryIn_Text.Text = string.Empty;
                lblAntenna_PortEntryIn_Time_Text.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDPortEntry, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                PortEntry_readerAPI.Actions.PurgeTags();
                Thread.Sleep(1000);
            }
        }
        //is the tag read in WBOUT
        private void FunctionPortEntryOut(string tagIDPortEntry, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_PortEntryOut_Text.Text = "";
            //Application.DoEvents();

            bool blnIsValid_PortEntry = false;
            if (Class_ProperityLayer.Ant3and41 == "PortEntry")
            {
                TruckType = "PE";
            }
            else if (Class_ProperityLayer.Ant3and41 == "General")
            {
                TruckType = "GNL";
            }
            blnIsSuccess_PortEntry = FunctionBlnValidTag_PortEntry(tagIDPortEntry, TruckType, out strErrorMessage_PortEntry, out blnIsValid_PortEntry);
            lblAntennaStatus_PortEntryIn_Text.Text = "";

            if (blnIsSuccess_PortEntry)
            {
                if (blnIsValid_PortEntry)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (SqlconInternal.State == ConnectionState.Closed || SqlconInternal.State == ConnectionState.Broken)
                    {
                        SqlconInternal.Open();
                    }
                    if (TruckType == "PE")
                    {
                        if (TruckFlag == 1)
                        {

                            string strNonQuery_PortEntry = "Insert into  [dbo].PORTENTRYTAGLOG (tagno,Ant,wbno,readdt,Reader,SysIP) Values ('" + Class_ProperityLayer.PTAGNO1 + "','" + tagAntennaID + "','" + Class_ProperityLayer.PWBNO1 + "',GETDATE(),'" + strPortEntryIPaddress + "','" + SysIP + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                        }
                    }
                    else if (TruckType == "GNL")
                    {
                        if (TruckFlag == 1)
                        {
                            //string strNonQuery_PortEntry = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, SqlconInternal);
                        }
                        else if (TruckFlag == 2)
                        {
                            string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader]) Values (" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strPortEntryIPaddress + "')";
                            Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                        }
                    }
                    int execquery = Sqlcmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntenna_PortEntryOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and PortEntry-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_PortEntryOut_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_PortEntryOut_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        lblAntenna_PortEntryOut_Text.Text = tagAntennaID;
                        Thread.Sleep(1000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Sqlcon.Close();
                        PortEntry_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_PortEntryIn_Text.Text = strErrorMessage_PortEntry;
                        lblRFIDTagValue_PortEntryOut_Text.Text = string.Empty;
                        lblAntenna_PortEntryOut_Text.Text = string.Empty;
                        lblAntenna_PortEntryOut_Time_Text.Text = string.Empty;
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        PortEntry_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    lblAntennaStatus_PortEntryIn_Text.Text = "Invlid Tag ID '" + tagIDPortEntry + "'";
                    //Application.DoEvents();
                    lblRFIDTagValue_PortEntryOut_Text.Text = string.Empty;
                    lblAntenna_PortEntryOut_Text.Text = string.Empty;
                    lblAntenna_PortEntryOut_Time_Text.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDPortEntry, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    PortEntry_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(1000);
                }
                //return false;
            }
            else
            {
                lblAntennaStatus_PortEntryIn_Text.Text = strErrorMessage_PortEntry;
                //Application.DoEvents();
                lblRFIDTagValue_PortEntryOut_Text.Text = string.Empty;
                lblAntenna_PortEntryOut_Text.Text = string.Empty;
                lblAntenna_PortEntryOut_Time_Text.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDPortEntry, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                PortEntry_readerAPI.Actions.PurgeTags();
                Thread.Sleep(1000);
            }
            //return false;
        }
        private void SaveJunkData(string tagnum, string wbnum)
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + Class_ProperityLayer.AntNo1 + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + strPortEntryIPaddress + "')";
                Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
        }

        //Check the Tag is either in Internal fleet or By Road 
        private bool FunctionBlnValidTag_PortEntry(string inParamStrTag, string inParamStrInOrOut, out string outParamErrorMessage, out bool outParamBlnIsValid)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            string newtag, newtag1;
            outParamBlnIsValid = false;
            outParamErrorMessage = "";
            TruckType = inParamStrInOrOut;
            try
            {
                if (TruckType == "PE")
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (SqlconInternal.State == ConnectionState.Closed || SqlconInternal.State == ConnectionState.Broken)
                    {
                        SqlconInternal.Open();
                    }
                    // string command = "Select TAGNO From (Select * from CLMS.CT_PORTENTRYTAGLOG  where WBNO='" + Class_ProperityLayer.PWBNO1 + "' order by READDT desc) where rownum=1";
                    string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from  [dbo].PORTENTRYTAGLOG  where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
                    SqlDataAdapter da = new SqlDataAdapter(command, SqlconInternal);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                    else
                    {
                        newtag = "0";
                    }

                    if (newtag == Class_ProperityLayer.PTAGNO1)// && PTruckNo == Class_ProperityLayer.ParkingFleetNo1
                    {
                        if (PFlag == 1)
                        {
                            lblAntennaStatus_PortEntryIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        Thread.Sleep(1000);
                        errorflag = 0;
                        outParamBlnIsValid = false;
                        PortEntry_readerAPI.Actions.PurgeTags();
                        return false;
                    }
                    else
                    {
                        errorflag = 1;
                        outParamBlnIsValid = true;
                        TruckFlag = 1;
                        return true;
                    }

                }
                else if (TruckType == "GNL")
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (SqlconInternal.State == ConnectionState.Closed || SqlconInternal.State == ConnectionState.Broken)
                    {
                        SqlconInternal.Open();
                    }
                    //string com1 = "SELECT [FleetNo]  FROM  [dbo].[tmst_FleetMaster]a,  [dbo].[tmst_TagMaster] b where a.TagId = b.TagId and b.TagNo ='" + Class_ProperityLayer.PTAGNO1 + "'";
                    string com1 = "select [TruckNo] from [dbo].[Tag_TruckAllocation] A,  [dbo].[TruckMaster] B,  [dbo].[TagMaster] C where A.Tagid=C.Tagid and A.Tkid=B.Tkid and A.[RStatus]='Active' and C.TagNo='" + Class_ProperityLayer.PTAGNO1 + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(com1, SqlconInternal);
                    DataSet ds1 = new DataSet();
                    string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from  [dbo].[MTFX9500InternalFleet] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
                    SqlDataAdapter da = new SqlDataAdapter(command, SqlconInternal);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                    else
                    {
                        newtag = "0";
                    }

                    string command1 = "select  TOP 1 [tagno],[Ant],[wbno],[readdt] from  [dbo].[MTFX9500BYROAD] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
                    SqlDataAdapter dar = new SqlDataAdapter(command1, Sqlcon);
                    DataSet dsr = new DataSet();
                    dar.Fill(dsr);

                    if (dsr.Tables[0].Rows.Count > 0)
                    {
                        newtag1 = dsr.Tables[0].Rows[0].ItemArray[0].ToString();
                        //dstr = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    }
                    else
                    {
                        newtag1 = "0";
                    }
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {

                        Class_ProperityLayer.ParkingFleetNo1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (newtag == Class_ProperityLayer.PTAGNO1)// && PTruckNo == Class_ProperityLayer.ParkingFleetNo1
                        {
                            if (PFlag == 1)
                            {
                                lblAntennaStatus_PortEntryIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                            }
                            else if (PFlag == 2)
                            {
                                lblAntennaStatus_PortEntryOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                            }
                            Thread.Sleep(1000);
                            errorflag = 0;
                            outParamBlnIsValid = false;
                            PortEntry_readerAPI.Actions.PurgeTags();
                            return false;

                        }
                        else
                        {
                            outParamBlnIsValid = true;
                            TruckFlag = 1;
                            return true;
                        }
                    }
                    else
                    {
                        if (newtag1 == Class_ProperityLayer.PTAGNO1)
                        {
                            if (PFlag == 1)
                            {
                                lblAntennaStatus_PortEntryIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                            }
                            else if (PFlag == 2)
                            {
                                lblAntennaStatus_PortEntryOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                            }
                            Thread.Sleep(1000);
                            outParamBlnIsValid = false;
                            PortEntry_readerAPI.Actions.PurgeTags();
                            errorflag = 0;
                            return false;
                        }
                        else
                        {
                            //inventoryList.Items.Clear();

                            string com2 = "SELECT [Truck No]  FROM  [dbo].[Tag_Register] where [Tag No] ='" + Class_ProperityLayer.PTAGNO1 + "' and [TransctionStatus] = 'P' order by [Tag ID] desc";
                            SqlDataAdapter da2 = new SqlDataAdapter(com2, Sqlcon);
                            DataSet ds2 = new DataSet();
                            da2.Fill(ds2);
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                Class_ProperityLayer.ParkingFleetNo1 = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                                outParamBlnIsValid = true;
                                TruckFlag = 2;
                                errorflag = 0;
                            }
                            else
                            {
                                lblAntennaStatus_PortEntryIn_Text.Text = "Tag/Truck is Not Registered";
                                errorflag = 1;
                            }
                            return true;
                        }
                        //}
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorflag = 1;
                outParamErrorMessage = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        //timer check only connection
        private void tnrNetworkConnection_Tick(object sender, EventArgs e)
        {
            try
            {
                var reply = ping.Send(host, 60, buffer, options);
                if (reply == null)
                {
                    //MessageBox.Show("Reply was null");
                    lblmsg.Text = "Reply was null";
                    return;
                }
                if (reply.Status == IPStatus.Success)
                {
                    lblErrorMessage.Text = "";
                    lblmsg.Text = "Network Connected!";
                    if (nwflag == 1)
                    {
                        if (ConnectErrLog == 1)
                        {
                            CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Reconnect", strPortEntryIPaddress, strPortEntryPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                            ConnectErrLog = 0;
                            DisConnectErrLog = 0;
                        }
                        //CatchException(string.Format("{0} - {1} - {2} - {3} - {4} - {5}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, hdnGPID.Text, GlobalVariables.versionLog));
                        btnPortEntryAreaReaderConnect_Click(sender, e);
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    lblmsg.Text = "Network Disconnected!";
                    if (DisConnectErrLog == 0)
                    {
                        CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Disconnect", strPortEntryIPaddress, strPortEntryPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                        DisConnectErrLog = 1;
                    }
                    btnPortEntryAreaReaderDisconnect_Click(sender, e);
                    ConnectErrLog = 1;
                    nwflag = 1;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblmsg.Text = ex.Message;
            }
        }
        private static void CatchException(string excepmsg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "ErrorLog.txt");
            strW.WriteLine(excepmsg);
            strW.Close();
        }

        private void btnPortEntryAreaReaderDisconnect_MouseClick(object sender, MouseEventArgs e)
        {
            nwflag = 0;
        }

        private void FixedReader_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}