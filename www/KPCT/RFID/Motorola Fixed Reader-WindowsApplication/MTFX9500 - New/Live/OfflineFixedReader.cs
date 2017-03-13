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
using System.Data.OleDb;

namespace CS_RFID3_Host_Sample1.Live
{
    public partial class OfflineFixedReader : Form
    {
        OleDbConnection Localcon = new OleDbConnection(Class_ProperityLayer.OLEDBLocalCON_STRING);
        OleDbConnection Sharecon = new OleDbConnection(Class_ProperityLayer.OLEDBShareDBCON_STRING);
        string OfflineType = ConfigurationSettings.AppSettings["OfflineType"];
        int PFlag = 0, TruckFlag = 0;
        SqlCommand cmd;
        #region " Parking Area Variable Decleration "

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

        #endregion
        public OfflineFixedReader()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            InitializeComponent();
        }

        private void OfflineFixedReader_Load(object sender, EventArgs e)
        {
           // Class_ProperityLayer.PropertyConnectionString = Localcon;//System.Configuration.ConfigurationSettings.AppSettings.Get("OledbConnectionInfo").ToString();

            lblReaderIP_Parking_Text.Text = strParkingIPaddress;
            MyDataCollection.ReaderIP = strParkingIPaddress;
            lblReaderPort_Parking_Text.Text = strParkingPort;

            lblAntenna_ParkingIn_Text.Text = strParkingAntennaIn;
            lblAntenna_ParkingOut_Text.Text = strParkingAntennaOut;
        }

        #region Parking
        //Reader Connection
        private void btnParkingAreaReaderConnect_Click(object sender, EventArgs e)
        {

            if (tmrParkingArea.Enabled == false)
            {
                FunctionDisconnectReaderAtParking();
                if (FunctionRFIDConnection_Parking())
                {

                    btnParkingAreaReaderDisconnect.Enabled = true;
                    btnParkingAreaReaderConnect.Enabled = false;
                    lblReaderStatus_Parking_Text.Text = "# Connected";
                }
                else
                    lblReaderStatus_Parking_Text.Text = "# Not Connected";
            }
        }
        //Reader Disconnect
        private void btnParkingAreaReaderDisconnect_Click(object sender, EventArgs e)
        {
            tmrParkingArea.Enabled = false;
            Thread.Sleep(1000);
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
                MessageBox.Show("Exception : Connection Already Exists...");
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

                tmrParkingArea.Enabled = true;

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
                else
                    lblReaderStatus_Parking_Text.Text = "# Not Connected";
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
                           
                        }
                    }
                }
             }
            catch (Exception)
            { }
        }
        //is the tag read in WBIN
        private bool FunctionParkingIn(string tagIDParking, string tagAntennaID)
        {
            Localcon.Close();
            Sharecon.Close();
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_ParkingIn_Text.Text = "";
            ////Application.DoEvents();

            int execquery = 0;
            int Shexecquery = 0;
            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "IN", out strErrorMessage_Parking, out blnIsValid_Parking);
            if (blnIsSuccess_Parking)
            {
                if (blnIsValid_Parking)
                {
                    if (OfflineType == "D")
                    {
                        OleDbCommand Localcmd = Localcon.CreateCommand();
                        OleDbCommand Sharecmd = Sharecon.CreateCommand();
                        if (Localcon.State == ConnectionState.Closed || Localcon.State == ConnectionState.Broken)
                        {
                            Localcon.Open();
                        }
                        if (Sharecon.State == ConnectionState.Closed || Sharecon.State == ConnectionState.Broken)
                        {
                            Sharecon.Open();
                        }
                        if (TruckFlag == 1)
                        {
                            string strNonQuery_Parking = "Insert into [MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",'" + System.DateTime.Now.ToString() + "','" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                            Localcmd = new OleDbCommand(strNonQuery_Parking, Localcon);
                            Sharecmd = new OleDbCommand(strNonQuery_Parking, Sharecon);
                        }
                        execquery = Localcmd.ExecuteNonQuery();
                        Shexecquery = Sharecmd.ExecuteNonQuery();
                    }
                    else if (OfflineType == "L")
                    {
                        OleDbCommand Localcmd = Localcon.CreateCommand();
                        //OleDbCommand Sharecmd = Sharecon.CreateCommand();
                        if (Localcon.State == ConnectionState.Closed || Localcon.State == ConnectionState.Broken)
                        {
                            Localcon.Open();
                        }
                        if (TruckFlag == 1)
                        {
                            string strNonQuery_Parking = "Insert into [MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",'" + System.DateTime.Now.ToString() + "','" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                            Localcmd = new OleDbCommand(strNonQuery_Parking, Localcon);
                        }
                        execquery = Localcmd.ExecuteNonQuery();
                    }

                    if (Shexecquery >= 1 || execquery >= 1)
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
                        Localcon.Close();
                        Sharecon.Close();
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                        //Application.DoEvents();

                    }
                    return true;
                }
                else
                {
                    lblAntennaStatus_ParkingIn_Text.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();

                }
                return false;
            }
            else
            {
                lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                //Application.DoEvents();

            }
            Localcon.Close();
            Sharecon.Close();
            return false;

        }
        //is the tag read in WBOUT
        private bool FunctionParkingOut(string tagIDParking, string tagAntennaID)
        {
            Localcon.Close();
            Sharecon.Close();
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_ParkingOut_Text.Text = "";
            //Application.DoEvents();

            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "OUT", out strErrorMessage_Parking, out blnIsValid_Parking);
            lblAntennaStatus_ParkingIn_Text.Text = "";
            int execquery = 0;
             int Shexecquery =0;
            if (blnIsSuccess_Parking)
            {
                if (blnIsValid_Parking)
                {

                    if (OfflineType == "D")
                    {
                        OleDbCommand Localcmd = Localcon.CreateCommand();
                        OleDbCommand Sharecmd = Sharecon.CreateCommand();
                        if (Localcon.State == ConnectionState.Closed || Localcon.State == ConnectionState.Broken)
                        {
                            Localcon.Open();
                        }
                        if (Sharecon.State == ConnectionState.Closed || Sharecon.State == ConnectionState.Broken)
                        {
                            Sharecon.Open();
                        }
                        if (TruckFlag == 1)
                        {
                            string strNonQuery_Parking = "Insert into [MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",'" + System.DateTime.Now.ToString() + "','" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                            Localcmd = new OleDbCommand(strNonQuery_Parking, Localcon);
                            Sharecmd = new OleDbCommand(strNonQuery_Parking, Sharecon);
                        }
                        execquery = Localcmd.ExecuteNonQuery();
                        Shexecquery = Sharecmd.ExecuteNonQuery();
                    }
                    else if (OfflineType == "L")
                    {
                        OleDbCommand Localcmd = Localcon.CreateCommand();
                        //OleDbCommand Sharecmd = Sharecon.CreateCommand();
                        if (Localcon.State == ConnectionState.Closed || Localcon.State == ConnectionState.Broken)
                        {
                            Localcon.Open();
                        }
                        if (TruckFlag == 1)
                        {
                            string strNonQuery_Parking = "Insert into [MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",'" + System.DateTime.Now.ToString() + "','" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strParkingIPaddress + "')";
                            Localcmd = new OleDbCommand(strNonQuery_Parking, Localcon);
                        }
                        execquery = Localcmd.ExecuteNonQuery();
                    }
                   
                    if (Shexecquery >= 1 || execquery >= 1)
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
                        Localcon.Close();
                        Sharecon.Close();
                      
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                        //Application.DoEvents();

                    }
                    return true;
                }
                else
                {
                    lblAntennaStatus_ParkingIn_Text.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();

                }
                return false;
            }
            else
            {
                lblAntennaStatus_ParkingIn_Text.Text = strErrorMessage_Parking;
                //Application.DoEvents();

            }
            Localcon.Close();
            Sharecon.Close();
            return false;
        }
        //Check the Tag is either in Internal fleet or By Road 
        private bool FunctionBlnValidTag_Parking(string inParamStrTag, string inParamStrInOrOut, out string outParamErrorMessage, out bool outParamBlnIsValid)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            string newtag;
            outParamBlnIsValid = false;
            outParamErrorMessage = "";
            try
            {
                OleDbConnection connectionstring = new OleDbConnection();
                OleDbCommand Localcmd = Localcon.CreateCommand();
                OleDbCommand Sharecmd = Sharecon.CreateCommand();
                if (OfflineType == "L")
                {
                    connectionstring = Localcon;
                }
                else if (OfflineType == "D")
                {
                    connectionstring = Sharecon;
                }
                if (connectionstring.State == ConnectionState.Closed || connectionstring.State == ConnectionState.Broken)
                {
                    connectionstring.Open();
                } 
                string com1 = "SELECT [FleetNo]  FROM [tmst_FleetMaster]a, [tmst_TagMaster] b where a.TagId = b.TagId and b.TagNo =" + Class_ProperityLayer.PTAGNO1;
                OleDbDataAdapter da1 = new OleDbDataAdapter(com1, connectionstring);
                DataSet ds1 = new DataSet();
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [MTFX9500InternalFleet] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
                //string command = "select  [tagno],[Ant],[wbno],CONVERT(VARCHAR,CONVERT(DATETIME,[readdt],103),103) + ' ' + CONVERT(VARCHAR,CONVERT(DATETIME,[readdt],108),108) [readdt] from MTFX9500on where  [wbno] ='" + txtWbno.Text + "' and [tagno]='" + Tagno + "' and [readdt]=(SELECT MAX ([readdt]) FROM MTFX9500on WHERE [wbno] ='" + txtWbno.Text + "' and [tagno]='" + Tagno + "')  order by readdt desc";
                OleDbDataAdapter da = new OleDbDataAdapter(command, connectionstring);
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
                    lblmsg.Text = "Truck was not Found..";
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                outParamErrorMessage = ex.Message.ToString();
                return false;
            }
        }

        #endregion
    }
}

