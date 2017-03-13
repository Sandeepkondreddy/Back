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
using ComponentFactory.Krypton.Toolkit;

namespace CS_RFID3_Host_Sample1
{
    public partial class formTest : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        #region "Variable Decleration"
        SqlConnection Sqlcon; SqlCommand cmd; static string Sqlstr = Class_ProperityLayer.SqlConn_String;//Connection details

        string strReaderIPaddress = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderIPAddress").ToString();
        string strReaderPort = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderPort").ToString();
        string strReaderAntennaSet1 = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaIn").ToString();
        string strReaderAntennaSet2 = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaOut").ToString();

        RFIDReader Parking_readerAPI;

        int fixant = 0; int PFlag = 0,TruckFlag = 0;

        bool blnIsSuccess_Parking = false;

        int errorflag = 0;

        string strErrorMessage_Parking = "";
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

        public formTest()
        {
            InitializeComponent(); timerNetworkConnection.Enabled = true;
            //lblVersion.Text = Class_ProperityLayer.Version;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            host = strReaderIPaddress;
        }
        #region "Form Load"
        private void formTest_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);
            txtReaderIP.Text = strReaderIPaddress;
            MyDataCollection.ReaderIP = strReaderIPaddress;
            txtReaderPort.Text = strReaderPort;
            lblAntennaSet1Value.Text = strReaderAntennaSet1;
            lblAntennaSet2Value.Text = strReaderAntennaSet2;
            lblWBOneValue.Text = ConfigurationManager.AppSettings["WBIN"];
            lblWBTwoValue.Text = ConfigurationManager.AppSettings["WBOUT"];
            this.Text = lblWBOneValue.Text + " and " + lblWBTwoValue.Text;
        }
        #endregion

        #region "Reader Connection"
        private void btnConnect_Click(object sender, EventArgs e)
        {
            nwflag = 1;
            //tmrConnection.Enabled = true;
            btnDisconnect.Enabled = true;
            btnConnect.Enabled = false;
            if (timerReader.Enabled == false)
            {
                FunctionDisconnectReader();
                if (FunctionRFIDConnection_Parking())
                    lblReaderStatusValue.Text = "# Connected";
                else
                    lblReaderStatusValue.Text = "# Not Connected";
            }
        }
        //check the connection status
        private void FunctionDisconnectReader()
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
                Parking_readerAPI = new RFIDReader(strReaderIPaddress, Convert.ToUInt32(strReaderPort), 0);
                Parking_readerAPI.Connect();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception : Network Problem or Connection Already Exists...");
                lblErrorValue.Text = "Exception : Network Problem or Connection Already Exists...";
                //if (ex.Message == "Connect")
                //{
                //    lblErrorMessage.Text = "Exception : Connection Already Exists...";
                //    tnrNetworkConnection.Enabled = false;
                //}
                //else
                //{
                //    lblErrorMessage.Text = "Exception : Network Problem...";
                //}
                btnDisconnect.Enabled = false;
                btnConnect.Enabled = true;

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

                timerReader.Enabled = true;
                nwflag = 0;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message.ToString());
                return false;
            }
        }
        #endregion


        #region "Reader DisConnection"
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            timerReader.Enabled = false;
            //tnrNetworkConnection.Enabled = false;
            //nwflag = 0;
            Thread.Sleep(1000);
            FunctionDisconnectReader();
            lblReaderStatusValue.Text = "# Not Connected";
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
        }
        #endregion

        #region "Timer to Get Tag details from Reader"
        //Timer run to get the tag data(Antenna no, tagid,....)
        private void timerReader_Tick(object sender, EventArgs e)
        {
            string WBNO;
            try
            {
                if (Parking_readerAPI.IsConnected)
                    lblReaderStatusValue.Text = "# Connected"; lblErrorValue.Text = "-";
                //else
                //lblReaderStatus_Parking_Text.Text = "# Not Connected";
            }
            catch (Exception ex)
            {
                lblReaderStatusValue.Text = "# Not Connected";
                lblErrorValue.Text = ex.ToString();
            }

            try
            {
                Symbol.RFID3.TagData[] tagData = Parking_readerAPI.Actions.GetReadTags(1);

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
                            Parking_readerAPI.Actions.PurgeTags();
                            return;
                        }
                        else
                        {
                            string tagIDParking = tag.TagID;
                            string tagAntennaID = tag.AntennaID.ToString();
                            Class_ProperityLayer.AntNo1 = tag.AntennaID;
                            string tagID = tag.TagID; // getting tagid here 113370000000000000000000
                            string tagno = tagData[nIndex].TagID;
                            string RSSI = tag.PeakRSSI.ToString();
                            string Tagno = tagno.Replace(" ", "").Substring(0, 5);

                            Class_ProperityLayer.DupTagNo1 = Tagno;//get the tagnum to restreict duplicate values

                            Class_ProperityLayer.PTAGNO1 = Tagno;
                            string Ant = tag.AntennaID.ToString();
                            string LOC = ConfigurationManager.AppSettings["loc"];
                            //string Reader = ConfigurationManager.AppSettings["fixreaderip"];
                            string Reader = MyDataCollection.ReaderIP;
                            CatchData(string.Format("{0},{1},{2},{3},{4}", "DATA LOG", Tagno, Ant, RSSI, DateTime.Now));

                            if (int.Parse(Ant) >= 1 && int.Parse(Ant) <= 2)
                            {
                                //tagdate = DateTime.Now;
                                fixant = 1;
                                WBNO = ConfigurationManager.AppSettings["WBIN"];
                                Class_ProperityLayer.PWBNO1 = WBNO;
                                PFlag = 1;
                                Parking_readerAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.TRUE;
                                //FunctiongetAntennaSet1Details(tagIDParking, tagAntennaID);
                                Parking_readerAPI.Config.GPO[1].PortState = GPOs.GPO_PORT_STATE.FALSE;
                            }
                            else
                            {
                                fixant = 2;
                                WBNO = ConfigurationManager.AppSettings["WBOUT"];
                                Class_ProperityLayer.PWBNO1 = WBNO;
                                PFlag = 2;
                                //FunctiongetAntennaSet2Details(tagIDParking, tagAntennaID);
                            }

                        }
                    }
                    //}
                }
            }
            catch (Exception)
            {
                //btnParkingAreaReaderDisconnect_Click(sender, e);
                //tmrParkingArea.Enabled = false;

            }
        }
        private static void CatchData(string msg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "DataLog.txt");
            strW.WriteLine(msg);
            strW.Close();
        }
        //is the tag read in WBIN
        private void FunctiongetAntennaSet1Details(string tagIDParking, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaSet1StatusValue.Text = "";
            ////Application.DoEvents();

            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "IN", out strErrorMessage_Parking, out blnIsValid_Parking);
            if (blnIsSuccess_Parking)
            {
                if (blnIsValid_Parking)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }

                    if (TruckFlag == 1)
                    {
                        string strNonQuery_Parking = "Insert into [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strReaderIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strReaderIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    int execquery = cmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntennaSet1StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " has read and Parking-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        AntennaSet1TagValue.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntennaSet1TimeValue.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(1000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        //lblRFIDTagValue_ParkingIn.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntennaSet1Value.Text = tagAntennaID;
                        Sqlcon.Close();
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaSet1StatusValue.Text = strErrorMessage_Parking;
                        //Application.DoEvents();
                        AntennaSet1TagValue.Text = string.Empty;
                        lblAntennaSet1Value.Text = string.Empty;
                        lblAntennaSet1TimeValue.Text = string.Empty;
                        //SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Parking_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(1000);
                    }
                    //return true;
                }
                else
                {
                    lblAntennaSet1StatusValue.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();
                    AntennaSet1TagValue.Text = string.Empty;
                    lblAntennaSet1Value.Text = string.Empty;
                    lblAntennaSet1TimeValue.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    Parking_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(1000);
                }
                //return false;
            }
            else
            {
                lblAntennaSet1StatusValue.Text = strErrorMessage_Parking;
                //Application.DoEvents();
                AntennaSet1TagValue.Text = string.Empty;
                lblAntennaSet1Value.Text = string.Empty;
                lblAntennaSet1TimeValue.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                Parking_readerAPI.Actions.PurgeTags();
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
                string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + Class_ProperityLayer.AntNo1 + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + strReaderIPaddress + "')";
                cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblErrorValue.Text = ex.Message;
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
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from [IFMS150311].[dbo].[MTFX9500InternalFleet] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
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
                            lblAntennaSet1StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        else if (PFlag == 2)
                        {
                            lblAntennaSet2StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        Thread.Sleep(1000);
                        errorflag = 0;
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
                            lblAntennaSet1StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                        }
                        else if (PFlag == 2)
                        {
                            lblAntennaSet2StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " has read Same Tag Found..";
                        }
                        Thread.Sleep(1000);
                        outParamBlnIsValid = false;
                        Parking_readerAPI.Actions.PurgeTags();
                        errorflag = 0;
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
                            errorflag = 0;
                        }
                        else
                        {
                            lblAntennaSet1StatusValue.Text = "Tag/Truck is Not Registered";
                            errorflag = 1;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorflag = 1;
                outParamErrorMessage = ex.Message.ToString();
                return false;
            }
        }
        //is the tag read in WBOUT
        private void FunctiongetAntennaSet2Details(string tagIDParking, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaSet2StatusValue.Text = "";
            //Application.DoEvents();

            bool blnIsValid_Parking = false;
            blnIsSuccess_Parking = FunctionBlnValidTag_Parking(tagIDParking, "OUT", out strErrorMessage_Parking, out blnIsValid_Parking);
            lblAntennaSet2StatusValue.Text = "";

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
                        string strNonQuery_Parking = "Insert into [IFMS150311].[dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strReaderIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_Parking = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strReaderIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_Parking, Sqlcon);
                    }
                    int execquery = cmd.ExecuteNonQuery();
                    if (execquery >= 1)
                    {
                        lblAntennaSet2StatusValue.Text = Class_ProperityLayer.PTAGNO1 + " has read and Parking-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblAntennaSet2TagValue.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntennaSet2TimeValue.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //lblRFIDTagValue_ParkingOut.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntennaSet2Value.Text = tagAntennaID;
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(1000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Sqlcon.Close();
                        //if (!FunctionParkingIn(tagIDParking, tagAntennaID))
                        //    FunctionParkingOut(tagIDParking, tagAntennaID);
                        //Application.DoEvents();
                        Parking_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaSet2StatusValue.Text = strErrorMessage_Parking;
                        //Application.DoEvents();
                        lblAntennaSet2TagValue.Text = string.Empty;
                        lblAntennaSet2Value.Text = string.Empty;
                        lblAntennaSet2TimeValue.Text = string.Empty;
                        //SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Parking_readerAPI.Actions.PurgeTags();
                        Thread.Sleep(1000);
                    }
                    //return true;
                }
                else
                {
                    lblAntennaSet2StatusValue.Text = "Invlid Tag ID '" + tagIDParking + "'";
                    //Application.DoEvents();
                    lblAntennaSet2TagValue.Text = string.Empty;
                    lblAntennaSet2Value.Text = string.Empty;
                    lblAntennaSet2TimeValue.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    Parking_readerAPI.Actions.PurgeTags();
                    Thread.Sleep(1000);
                }
                //return false;
            }
            else
            {
                lblAntennaSet2StatusValue.Text = strErrorMessage_Parking;
                //Application.DoEvents();
                lblAntennaSet2TagValue.Text = string.Empty;
                lblAntennaSet2Value.Text = string.Empty;
                lblAntennaSet2TimeValue.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDParking, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                Parking_readerAPI.Actions.PurgeTags();
                Thread.Sleep(1000);
            }
            //return false;
        }
        #endregion
        #region "Network Connectivity Check"
        //timer check only connection
        private void timerNetworkConnection_Tick(object sender, EventArgs e)
        {
            try
            {

                var reply = ping.Send(host, 999, buffer, options);
                if (reply == null)
                {
                    //MessageBox.Show("Reply was null");
                    lblNetworkStatusValue.Text = "Reply was null";
                    return;
                }

                if (reply.Status == IPStatus.Success)
                {
                    lblErrorValue.Text = "";
                    lblNetworkStatusValue.Text = "Network Connected!";
                    if (nwflag == 1)
                    {
                        if (ConnectErrLog == 1)
                        {
                            CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Reconnect", strReaderIPaddress, strReaderPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                            ConnectErrLog = 0;
                            DisConnectErrLog = 0;
                        }
                        //CatchException(string.Format("{0} - {1} - {2} - {3} - {4} - {5}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, hdnGPID.Text, GlobalVariables.versionLog));
                        btnConnect_Click(sender, e);
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    lblNetworkStatusValue.Text = "Network Disconnected!";
                    if (DisConnectErrLog == 0)
                    {
                        CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Disconnect", strReaderIPaddress, strReaderPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                        DisConnectErrLog = 1;
                    }
                    btnParkingAreaReaderDisconnect_Click(sender, e);
                    ConnectErrLog = 1;
                    nwflag = 1;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblNetworkStatusValue.Text = ex.Message;
            }
        }
        //Reader Disconnect
        private void btnParkingAreaReaderDisconnect_Click(object sender, EventArgs e)
        {
            timerReader.Enabled = false;
            //tnrNetworkConnection.Enabled = false;
            //nwflag = 0;
            Thread.Sleep(1000);
            FunctionDisconnectReader();
            lblReaderStatusValue.Text = "# Not Connected";
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
        }
        private static void CatchException(string excepmsg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "ErrorLog.txt");
            strW.WriteLine(excepmsg);
            strW.Close();
        }
        #endregion
    }
}