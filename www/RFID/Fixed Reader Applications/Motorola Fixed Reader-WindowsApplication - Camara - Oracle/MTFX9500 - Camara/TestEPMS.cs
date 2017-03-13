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
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;


namespace CS_RFID3_Host_Sample1
{
    public partial class TestEPMS : Form
    {
        int PFlag = 0, TruckFlag = 0;
        SqlCommand cmd;
        #region " Parking Area Variable Decleration "
        SqlConnection Sqlcon;
        string strSelectQuery_Parking = "";
        string strNonQuery_Parking = "";
        string strTableName_Parking = "";
        string strScalarResult_Parking = "";
        string strErrorMessage_Parking = "";

        bool blnIsSuccess_Parking = false;
        bool blnIsExist_Parking = false;

        //RFID-Reader at Parking
        RFIDReader Parking_readerAPI;

        string strParkingIPaddress = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderIPAddress").ToString();
        string strParkingPort = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderPort").ToString();

        string strParkingAntennaIn = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaIn").ToString();
        string strParkingAntennaOut = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaOut").ToString();

        private delegate void UpdateUI_Parking(string stringParkingData);
        private UpdateUI_Parking Parking_UpdateUIHandler = null;
        private TagData Parking_ReadTag = null;
        static string Sqlstr = Class_ProperityLayer.SqlConn_String;
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
        public TestEPMS()
        {
            InitializeComponent();
            lblVersion.Text = Class_ProperityLayer.Version;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            host = strParkingIPaddress;
            //tnrNetworkConnection.Enabled = true;
        }
        #region Parking
        //Reader Connection
        private void btnParkingAreaReaderConnect_Click(object sender, EventArgs e)
        {
            nwflag = 1;
            //tmrConnection.Enabled = true;
            btnParkingAreaReaderDisconnect.Enabled = true;
            btnParkingAreaReaderConnect.Enabled = false;
            if (tmrParkingArea.Enabled == false)
            {
                FunctionDisconnectReaderAtParking();
                if (FunctionRFIDConnection_Parking())
                    lblReaderStatus_Parking_Text.Text = "# Connected";
                else
                    lblReaderStatus_Parking_Text.Text = "# Not Connected";
            }
        }
        //Reader Disconnect
        private void btnParkingAreaReaderDisconnect_Click(object sender, EventArgs e)
        {
            tmrParkingArea.Enabled = false;
            //tnrNetworkConnection.Enabled = false;
            //nwflag = 0;
            Thread.Sleep(3000);
            FunctionDisconnectReaderAtParking();
            lblReaderStatus_Parking_Text.Text = "# Not Connected";
            btnParkingAreaReaderDisconnect.Enabled = false;
            btnParkingAreaReaderConnect.Enabled = true;
        }
        //check the connection status
        private void FunctionDisconnectReaderAtParking()
        {
            try
            {
                if (Parking_readerAPI.IsConnected)
                    Parking_readerAPI.Disconnect();

            }
            catch (Exception)
            { }
        }
        //get the connection details
        public bool FunctionRFIDConnection_Parking()
        {
            try
            {
                Parking_readerAPI = new RFIDReader(strParkingIPaddress, Convert.ToUInt32(strParkingPort), 0);
                Parking_readerAPI.Connect();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception : Network Problem or Connection Already Exists...");
                lblErrorMessage.Text = "Exception : Network Problem or Connection Already Exists...";
                //if (ex.Message == "Connect")
                //{
                //    lblErrorMessage.Text = "Exception : Connection Already Exists...";
                //    tnrNetworkConnection.Enabled = false;
                //}
                //else
                //{
                //    lblErrorMessage.Text = "Exception : Network Problem...";
                //}
                btnParkingAreaReaderDisconnect.Enabled = false;
                btnParkingAreaReaderConnect.Enabled = true;

                return false;
            }
            try
            {
                if (Parking_readerAPI.IsConnected)
                {

                    Parking_readerAPI.Actions.TagAccess.OperationSequence.DeleteAll();

                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;

                    op.ReadAccessParams.ByteCount = 0;

                    Parking_readerAPI.Actions.TagAccess.OperationSequence.Add(op);
                    Parking_readerAPI.Actions.TagAccess.OperationSequence.PerformSequence();
                }

                //Parking_readerAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.TRUE;
                //Parking_readerAPI.Config.GPO[2].PortState = GPOs.GPO_PORT_STATE.TRUE;
                //Parking_readerAPI.Config.GPO[3].PortState = GPOs.GPO_PORT_STATE.TRUE;

                //Thread.Sleep(500);

                //Parking_readerAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.FALSE;
                //Parking_readerAPI.Config.GPO[2].PortState = GPOs.GPO_PORT_STATE.FALSE;
                //Parking_readerAPI.Config.GPO[3].PortState = GPOs.GPO_PORT_STATE.FALSE;

                tmrParkingArea.Enabled = true;
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
        private void tmrParkingArea_Tick(object sender, EventArgs e)
        {
            string WBNO;
            try
            {
                if (Parking_readerAPI.IsConnected)
                    lblReaderStatus_Parking_Text.Text = "# Connected";
                //else
                //lblReaderStatus_Parking_Text.Text = "# Not Connected";
            }
            catch (Exception)
            {
                lblReaderStatus_Parking_Text.Text = "# Not Connected";

            }

            try
            {
                Symbol.RFID3.TagData[] tagData = Parking_readerAPI.Actions.GetReadTags(1);
                if (tagData != null)
                {
                    string newtag, dstr;
                    AutoResetEvent AccessComplete;
                    AccessComplete = new AutoResetEvent(false);
                    for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                    {
                        if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                            (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                            tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
                        {
                            Symbol.RFID3.TagData tag = tagData[nIndex];
                            string tagIDParking = tag.TagID;
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
                            if (int.Parse(Ant) >= 1 && int.Parse(Ant) <= 2)
                            {

                                WBNO = ConfigurationManager.AppSettings["WBIN"];
                                Class_ProperityLayer.PWBNO1 = WBNO;
                                PFlag = 1;
                                FunctionParkingIn(tagIDParking, tagAntennaID);
                            }
                            else
                            {
                                WBNO = ConfigurationManager.AppSettings["WBOUT"];
                                Class_ProperityLayer.PWBNO1 = WBNO;
                                PFlag = 2;
                                FunctionParkingOut(tagIDParking, tagAntennaID);
                            }
                            //if (lblAntenna_ParkingIn_Text.Text == tagAntennaID)
                            //    FunctionParkingIn(tagIDParking, tagAntennaID);
                            //else if (lblAntenna_ParkingOut_Text.Text == tagAntennaID)
                            //    FunctionParkingOut(tagIDParking, tagAntennaID);

                            //lblRFIDTagValue_ParkingIn_Text.Text = tagIDParking;
                            //lblAntenna_ParkingIn_Text.Text = tagAntennaID;
                            //if (!FunctionParkingIn(tagIDParking, tagAntennaID))
                            //    FunctionParkingOut(tagIDParking, tagAntennaID);

                        }
                    }
                }
            }
            catch (Exception)
            {
                //btnParkingAreaReaderDisconnect_Click(sender, e);
                //tmrParkingArea.Enabled = false;

            }
        }
        //is the tag read in WBIN
        private void FunctionParkingIn(string tagIDParking, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_ParkingIn_Text.Text = "";
            ////Application.DoEvents();

            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "IN", out strErrorMessage_Parking, out blnIsValid_Parking);
            if (blnIsSuccess_Parking)
            {
                if (blnIsValid_Parking)
                {
                    //string Conn = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ConnectionString;
                    //SqlConnection sqlCon = new SqlConnection(Conn);
                    //sqlCon.Open();
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    //strNonQuery_Parking = "UPDATE XXX_GATEHOUSE_ENTRY_DETAILS SET DATETIME_PARKING_IN=GETDATE() WHERE RFID_TAG_NO='" + tagIDParking + "' AND ORGN_CODE='" + Class_ProperityLayer.PropertyOrganizationCode + "' AND RFID_TAG_STATUS='N'";
                    if (TruckFlag == 1)
                    {
                        string strNonQuery_Parking = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleetOld24] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    int execquery = cmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntennaStatus_ParkingIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and Parking-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_ParkingIn_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_ParkingIn_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(3000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        //lblRFIDTagValue_ParkingIn.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_ParkingIn_Text.Text = tagAntennaID;
                        Sqlcon.Close();
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                        //Application.DoEvents();
                        lblRFIDTagValue_ParkingIn_Text.Text = string.Empty;
                        lblAntenna_ParkingIn_Text.Text = string.Empty;
                        lblAntenna_ParkingIn_Time_Text.Text = string.Empty;
                        //SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Parking_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(3000);
                    }
                    //return true;
                }
                else
                {
                    lblAntennaStatus_ParkingIn_Text.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();
                    lblRFIDTagValue_ParkingIn_Text.Text = string.Empty;
                    lblAntenna_ParkingIn_Text.Text = string.Empty;
                    lblAntenna_ParkingIn_Time_Text.Text = string.Empty;
                    SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    Parking_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(3000);
                }
                //return false;
            }
            else
            {
                lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                //Application.DoEvents();
                lblRFIDTagValue_ParkingIn_Text.Text = string.Empty;
                lblAntenna_ParkingIn_Text.Text = string.Empty;
                lblAntenna_ParkingIn_Time_Text.Text = string.Empty;
                SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                Parking_readerAPI.Actions.PurgeTags();
                Thread.Sleep(3000);
            }
            //return false;
        }
        //is the tag read in WBOUT
        private void FunctionParkingOut(string tagIDParking, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_ParkingOut_Text.Text = "";
            //Application.DoEvents();

            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "OUT", out strErrorMessage_Parking, out blnIsValid_Parking);
            lblAntennaStatus_ParkingIn_Text.Text = "";

            if (blnIsSuccess_Parking)
            {
                if (blnIsValid_Parking)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    //string strNonQuery_Parking = "Insert into MTFX9500on ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                    //SqlCommand cmd = new SqlCommand(strNonQuery_Parking, sqlCon);
                    if (TruckFlag == 1)
                    {
                        string strNonQuery_Parking = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleetOld24] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    int execquery = cmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntennaStatus_ParkingOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and Parking-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_ParkingOut_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_ParkingOut_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //lblRFIDTagValue_ParkingOut.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_ParkingOut_Text.Text = tagAntennaID;
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(3000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Sqlcon.Close();
                        //if (!FunctionParkingIn(tagIDParking, tagAntennaID))
                        //    FunctionParkingOut(tagIDParking, tagAntennaID);
                        //Application.DoEvents();
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                        //Application.DoEvents();
                        lblRFIDTagValue_ParkingOut_Text.Text = string.Empty;
                        lblAntenna_ParkingOut_Text.Text = string.Empty;
                        lblAntenna_ParkingOut_Time_Text.Text = string.Empty;
                        //SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Parking_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(3000);
                    }
                    //return true;
                }
                else
                {
                    lblAntennaStatus_ParkingIn_Text.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();
                    lblRFIDTagValue_ParkingOut_Text.Text = string.Empty;
                    lblAntenna_ParkingOut_Text.Text = string.Empty;
                    lblAntenna_ParkingOut_Time_Text.Text = string.Empty;
                    SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    Parking_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(3000);
                }
                //return false;
            }
            else
            {
                lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                //Application.DoEvents();
                lblRFIDTagValue_ParkingOut_Text.Text = string.Empty;
                lblAntenna_ParkingOut_Text.Text = string.Empty;
                lblAntenna_ParkingOut_Time_Text.Text = string.Empty;
                SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                Parking_readerAPI.Actions.PurgeTags();
                Thread.Sleep(3000);
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
                string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500JunkData27022015] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + Class_ProperityLayer.AntNo1 + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + strParkingIPaddress + "')";
                cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }
        }
        //Check the Tag is either in Internal fleet or By Road 
        private bool FunctionBlnValidTag_Parking(string inParamStrTag, string inParamStrInOrOut, out string outParamErrorMessage, out bool outParamBlnIsValid)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            string newtag, newtag1;
            outParamBlnIsValid = false;
            outParamErrorMessage = "";
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                //string sqlConn1 = ConfigurationManager.ConnectionStrings["sqlconnectionstring"].ConnectionString;
                //SqlConnection dbCon1 = new SqlConnection(sqlConn1);
                string com1 = "SELECT [FleetNo]  FROM [IFMS150311].[dbo].[tmst_FleetMaster]a, [IFMS150311].[dbo].[tmst_TagMaster] b where a.TagId = b.TagId and b.TagNo ='" + Class_ProperityLayer.PTAGNO1 + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, Sqlcon);
                DataSet ds1 = new DataSet();
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [IFMS150311].[dbo].[MTFX9500InternalFleetOld24] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
                //string command = "select  [tagno],[Ant],[wbno],CONVERT(VARCHAR,CONVERT(DATETIME,[readdt],103),103) + ' ' + CONVERT(VARCHAR,CONVERT(DATETIME,[readdt],108),108) [readdt] from MTFX9500on where  [wbno] ='" + txtWbno.Text + "' and [tagno]='" + Tagno + "' and [readdt]=(SELECT MAX ([readdt]) FROM MTFX9500on WHERE [wbno] ='" + txtWbno.Text + "' and [tagno]='" + Tagno + "')  order by readdt desc";
                SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    newtag = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    //dstr = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                    //PTruckNo = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                }
                else
                {
                    newtag = "0";
                    //dstr = DateTime.Now.AddMinutes(-6).ToString("dd-MM-yyyy HH:mm:ss");
                }

                string command1 = "select  TOP 1 [tagno],[Ant],[wbno],[readdt] from  [dbo].[MTFX9500] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
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
                    //dstr = DateTime.Now.AddMinutes(-6).ToString("dd-MM-yyyy HH:mm:ss");
                }
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    Class_ProperityLayer.ParkingFleetNo1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                    if (newtag == Class_ProperityLayer.PTAGNO1)// && PTruckNo == Class_ProperityLayer.ParkingFleetNo1
                    {
                        //lblmsg.Text = "same Tag found ";
                        //m_ReaderAPI.Actions.Inventory.Stop();
                        //AccessComplete.WaitOne(3000, false);
                        if (PFlag == 1)
                        {
                            lblAntennaStatus_ParkingIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        else if (PFlag == 2)
                        {
                            lblAntennaStatus_ParkingOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        Thread.Sleep(3000);
                        outParamBlnIsValid = false;
                        Parking_readerAPI.Actions.PurgeTags();
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
                        //lblmsg.Text = "same Tag found ";
                        //m_ReaderAPI.Actions.Inventory.Stop();
                        //AccessComplete.WaitOne(3000, false);
                        if (PFlag == 1)
                        {
                            lblAntennaStatus_ParkingIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                        }
                        else if (PFlag == 2)
                        {
                            lblAntennaStatus_ParkingOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                        }
                        Thread.Sleep(3000);
                        outParamBlnIsValid = false;
                        Parking_readerAPI.Actions.PurgeTags();
                        return false;
                    }
                    else
                    {
                        //inventoryList.Items.Clear();
                        //string Conn1 = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
                        //SqlConnection sqlCon1 = new SqlConnection(Conn1);
                        if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                        {
                            Sqlcon.Open();
                        }
                        string com2 = "SELECT [Truck No]  FROM  [dbo].[Tag_Register] where [Tag No] ='" + Class_ProperityLayer.PTAGNO1 + "' and [TransctionStatus] = 'P' order by [Tag ID] desc";
                        SqlDataAdapter da2 = new SqlDataAdapter(com2, Sqlcon);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            Class_ProperityLayer.ParkingFleetNo1 = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
                            outParamBlnIsValid = true;
                            TruckFlag = 2;
                        }
                        else
                        {
                            lblAntennaStatus_ParkingIn_Text.Text = "Tag/Truck is Not Registered";
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                outParamErrorMessage = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        //timer check only connection
        private void tnrNetworkConnection_Tick(object sender, EventArgs e)
        {

        }
        private static void CatchException(string excepmsg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "ErrorLog.txt");
            strW.WriteLine(excepmsg);
            strW.Close();
        }

        private void btnParkingAreaReaderDisconnect_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void FixedReader_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void TestEPMS_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);

            //Class_ProperityLayer.PropertyConnectionString = System.Configuration.ConfigurationSettings.AppSettings.Get("sqlconn").ToString();

            lblReaderIP_Parking_Text.Text = strParkingIPaddress;
            MyDataCollection.ReaderIP = strParkingIPaddress;
            lblReaderPort_Parking_Text.Text = strParkingPort;

            lblAntenna_ParkingIn_Text.Text = strParkingAntennaIn;
            lblAntenna_ParkingOut_Text.Text = strParkingAntennaOut;

            lblWBIN.Text = ConfigurationManager.AppSettings["WBIN"];
            lblWBOUT.Text = ConfigurationManager.AppSettings["WBOUT"];
        }
    }
}
