//Not in Use 
//For Internal Fleet based on task
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
using System.IO;
using System.Net.NetworkInformation;

namespace CS_RFID3_Host_Sample1
{
    public partial class RFIDInternalFleet : Form
    {
        int PFlag = 0, TruckFlag = 0;
        SqlCommand cmd;
        SqlConnection Sqlcon;
        static string Sqlstr = Class_ProperityLayer.SqlConn_String;
        int fixant = 0;
        int errorflag = 0;
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
        #region " WB Area Variable Decleration "

        string dtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string strSelectQuery_WB = "";
        string strNonQuery_WB = "";
        string strTableName_WB = "";
        string strScalarResult_WB = "";
        string strErrorMessage_WB = "";
        int execquery;
        bool blnIsSuccess_WB = false;
        bool blnIsExist_WB = false;

        string tagno;
        //RFID-Reader at WB
        RFIDReader WB_readerAPI;

        string strWBIPaddress = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderIPAddress").ToString();
        string strWBPort = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderPort").ToString();

        string strWBAntennaIn = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaIn").ToString();
        string strWBAntennaOut = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaOut").ToString();

        private delegate void UpdateUI_WB(string stringPortEntryData);
        private UpdateUI_WB WB_UpdateUIHandler = null;
        private TagData WB_ReadTag = null;

        #endregion
        public RFIDInternalFleet()
        {
            InitializeComponent();
            lblVersion.Text = Class_ProperityLayer.Version;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            host = strWBIPaddress;
            tnrNetworkConnection.Enabled = true;
        }

        private void RFIDInternalFleet_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);

            lblReaderIP_WB_Text.Text = strWBIPaddress;
            MyDataCollection.ReaderIP = strWBIPaddress;
            lblReaderPort_WB_Text.Text = strWBPort;

            lblAntenna_WBIn_Text.Text = strWBAntennaIn;
            lblAntenna_WBOut_Text.Text = strWBAntennaOut;
            
            lblWBIN.Text = ConfigurationManager.AppSettings["WBIN"];
            lblWBOUT.Text = ConfigurationManager.AppSettings["WBOUT"];

            Class_ProperityLayer.Ant1and21 = ConfigurationManager.AppSettings["ant1and2"];
            Class_ProperityLayer.Ant3and41 = ConfigurationManager.AppSettings["ant3and4"];
        }
        #region WB
        //Reader Connection
        private void btnWBAreaReaderConnect_Click(object sender, EventArgs e)
        {
            nwflag = 1;
            //tmrConnection.Enabled = true;
        
            if (tmrWBArea.Enabled == false)
            {
                FunctionDisconnectReaderAtWB();
                if (FunctionRFIDConnection_WB())
                {
                    btnWBAreaReaderDisconnect.Enabled = true;
                    btnWBAreaReaderConnect.Enabled = false;
                    lblReaderStatus_WB_Text.Text = "# Connected";
                }
                else
                    lblReaderStatus_WB_Text.Text = "# Not Connected";
            }
        }
        //Reader Disconnect
        private void btnWBAreaReaderDisconnect_Click(object sender, EventArgs e)
        {
          
            tmrWBArea.Enabled = false;
            //tnrNetworkConnection.Enabled = false;
            //nwflag = 0;
            Thread.Sleep(1000);
            FunctionDisconnectReaderAtWB();
            lblReaderStatus_WB_Text.Text = "# Not Connected";
            btnWBAreaReaderDisconnect.Enabled = false;
            btnWBAreaReaderConnect.Enabled = true;
        }
        //check the connection status
        private void FunctionDisconnectReaderAtWB()
        {
            try
            {
                if (WB_readerAPI.IsConnected)
                    WB_readerAPI.Disconnect();

            }
            catch (Exception)
            { }
        }
        //get the connection details
        public bool FunctionRFIDConnection_WB()
        {
            try
            {

                WB_readerAPI = new RFIDReader(strWBIPaddress, Convert.ToUInt32(strWBPort), 0);
                WB_readerAPI.Connect();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Exception : Network Problem or Connection Already Exists...";
                btnWBAreaReaderDisconnect.Enabled = false;
                btnWBAreaReaderConnect.Enabled = true;
                return false;
            }
            try
            {
                if (WB_readerAPI.IsConnected)
                {

                    WB_readerAPI.Actions.TagAccess.OperationSequence.DeleteAll();

                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;

                    op.ReadAccessParams.ByteCount = 0;

                    WB_readerAPI.Actions.TagAccess.OperationSequence.Add(op);
                    WB_readerAPI.Actions.TagAccess.OperationSequence.PerformSequence();
                }

                tmrWBArea.Enabled = true;
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
        private void tmrWBArea_Tick(object sender, EventArgs e)
        {
            string WBNO;
            try
            {
                if (WB_readerAPI.IsConnected)
                    lblReaderStatus_WB_Text.Text = "# Connected";
                else
                    lblReaderStatus_WB_Text.Text = "# Not Connected";
            }
            catch (Exception)
            {
                lblReaderStatus_WB_Text.Text = "# Not Connected";

            }

            try
            {
                Symbol.RFID3.TagData[] tagData = WB_readerAPI.Actions.GetReadTags(1);
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
                            if (Class_ProperityLayer.DupTagNo1 == tagData[nIndex].TagID.Replace(" ", "").Substring(0, 5))
                            {
                                WB_readerAPI.Actions.PurgeTags();
                                return;
                            }
                            else
                            {
                                //Symbol.RFID3.TagData tag = tagData[nIndex];
                                string tagIDWB = tag.TagID;
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

                                Class_ProperityLayer.DupTagNo1 = Tagno;//get the tagnum to restreict duplicate values

                                CatchData(string.Format("{0},{1},{2},{3},{4}", "DATA LOG", Tagno, Ant, RSSI, DateTime.Now));

                                if (int.Parse(Ant) >= 1 && int.Parse(Ant) <= 2)
                                {
                                    fixant = 1;
                                    WBNO = ConfigurationManager.AppSettings["WBIN"];
                                    Class_ProperityLayer.PWBNO1 = WBNO;
                                    PFlag = 1;
                                    FunctionWBIn(tagIDWB, tagAntennaID);
                                }
                                else
                                {
                                    fixant = 2;
                                    WBNO = ConfigurationManager.AppSettings["WBOUT"];
                                    Class_ProperityLayer.PWBNO1 = WBNO;
                                    PFlag = 2;
                                    FunctionWBOut(tagIDWB, tagAntennaID);
                                }   
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
        private static void CatchData(string msg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "DataLog.txt");
            strW.WriteLine(msg);
            strW.Close();
        }
        //is the tag read in WBIN
        private bool FunctionWBIn(string tagIDWB, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_WBIn_Text.Text = "";
            ////Application.DoEvents();

            bool blnIsValid_WB = false;
            blnIsSuccess_WB = FunctionBlnValidTag_WB(tagIDWB, "IN", out strErrorMessage_WB, out blnIsValid_WB);
            if (blnIsSuccess_WB)
            {
                if (blnIsValid_WB)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (TruckFlag == 1)
                    {
                        string strNonQuery_WB = "Insert into  [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strWBIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_WB, Sqlcon);
                        if (Class_ProperityLayer.OperType1 == "Load")
                        {
                            InsertLoadData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "UnLoad")
                        {
                            InsertUnLoadData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "Verification")
                        {
                            InsertVerificationData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "Dust")
                        {
                            InsertDustData();
                        }
                        execquery = cmd.ExecuteNonQuery();
                    }
                    else if (TruckFlag == 2)
                    {
                        string strNonQuery_WB = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strWBIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_WB, Sqlcon);
                        execquery = cmd.ExecuteNonQuery();
                    }
                    else if (TruckFlag == 3)
                    {
                        InsertjunkData();
                    }
                    if (execquery >= 1)
                    {
                        lblAntennaStatus_WBIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and WB-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_WBIn_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_WBIn_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(2000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        //lblRFIDTagValue_WBIn.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_WBIn_Text.Text = tagAntennaID;
                        Sqlcon.Close();
                        //if (!FunctionWBIn(tagIDWB, tagAntennaID))
                        //    FunctionWBOut(tagIDWB, tagAntennaID);
                        //Application.DoEvents();
                        ResetAll();
                        WB_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_WBIn_Text.Text = strErrorMessage_WB;
                        lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                        lblAntenna_WBOut_Text.Text = string.Empty;
                        lblAntenna_WBOut_Time_Text.Text = string.Empty;
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        WB_readerAPI.Actions.PurgeTags();
                        ResetAll();
                        Thread.Sleep(2000);

                    }
                    return true;
                }
                else
                {
                    lblAntennaStatus_WBIn_Text.Text = "Invlid Tag ID '" + tagIDWB + "'";
                    lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                    lblAntenna_WBOut_Text.Text = string.Empty;
                    lblAntenna_WBOut_Time_Text.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDWB, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    WB_readerAPI.Actions.PurgeTags();
                    ResetAll();
                    Thread.Sleep(2000);

                }
                return false;
            }
            else
            {
                lblAntennaStatus_WBIn_Text.Text = strErrorMessage_WB;
                lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                lblAntenna_WBOut_Text.Text = string.Empty;
                lblAntenna_WBOut_Time_Text.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDWB, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                WB_readerAPI.Actions.PurgeTags();
                ResetAll();
                Thread.Sleep(2000);

            }
            return false;
        }
        //is the tag read in WBOUT
        private bool FunctionWBOut(string tagIDWB, string tagAntennaID)
        {
            AutoResetEvent AccessComplete;
            AccessComplete = new AutoResetEvent(false);
            lblAntennaStatus_WBOut_Text.Text = "";
            //Application.DoEvents();

            bool blnIsValid_WB = false;
            blnIsSuccess_WB = FunctionBlnValidTag_WB(tagIDWB, "OUT", out strErrorMessage_WB, out blnIsValid_WB);
            lblAntennaStatus_WBIn_Text.Text = "";

            if (blnIsSuccess_WB)
            {
                if (blnIsValid_WB)
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }
                    if (TruckFlag == 1)
                    {
                        string strNonQuery_WB = "Insert into  [dbo].[MTFX9500InternalFleet] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strWBIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_WB, Sqlcon);
                        if (Class_ProperityLayer.OperType1 == "Load")
                        {
                            InsertLoadData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "UnLoad")
                        {
                            InsertUnLoadData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "Verification")
                        {
                            InsertVerificationData();
                        }
                        else if (Class_ProperityLayer.OperType1 == "Dust")
                        {
                            InsertDustData();
                        }

                        execquery = cmd.ExecuteNonQuery();
                        ResetAll();
                    }
                    else if (TruckFlag == 2)
                    {//MTFX9500BYROAD
                        string strNonQuery_WB = "Insert into  [dbo].[MTFX9500BYROAD] ([tagno],[Ant],[wbno],[readdt],[truckno],[Reader])" + "Values(" + "'" + Class_ProperityLayer.PTAGNO1 + "'" + "," + "'" + tagAntennaID + "'" + "," + "'" + Class_ProperityLayer.PWBNO1 + "'" + ",GETDATE(),'" + Class_ProperityLayer.ParkingFleetNo1 + "','" + strWBIPaddress + "')";
                        cmd = new SqlCommand(strNonQuery_WB, Sqlcon);

                        execquery = cmd.ExecuteNonQuery();
                        ResetAll();
                    }
                    else if (TruckFlag == 3)
                    {
                        InsertjunkData();
                        ResetAll();
                    }
                    if (execquery >= 1)
                    {
                        lblAntennaStatus_WBOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " has read and WB-In Time captured successfully";
                        Class_ProperityLayer.PWBNO1 = string.Empty;
                        lblRFIDTagValue_WBOut_Text.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_WBOut_Time_Text.Text = "Antenna No. " + tagAntennaID + " at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                        //lblRFIDTagValue_WBOut.Text = Class_ProperityLayer.PTAGNO1;
                        lblAntenna_WBOut_Text.Text = tagAntennaID;
                        //AccessComplete.WaitOne(3000, false);
                        Thread.Sleep(2000);
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        Sqlcon.Close();
                        ResetAll();
                        WB_readerAPI.Actions.PurgeTags();
                    }
                    else
                    {
                        lblAntennaStatus_WBIn_Text.Text = strErrorMessage_WB;
                        lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                        lblAntenna_WBOut_Text.Text = string.Empty;
                        lblAntenna_WBOut_Time_Text.Text = string.Empty;
                        Class_ProperityLayer.PTAGNO1 = string.Empty;
                        WB_readerAPI.Actions.PurgeTags();
                        ResetAll();
                        Thread.Sleep(2000);

                    }
                    return true;
                }
                else
                {
                    lblAntennaStatus_WBIn_Text.Text = "Invlid Tag ID '" + tagIDWB + "'";
                    lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                    lblAntenna_WBOut_Text.Text = string.Empty;
                    lblAntenna_WBOut_Time_Text.Text = string.Empty;
                    if (errorflag == 1)
                    {
                        SaveJunkData(tagIDWB, Class_ProperityLayer.PWBNO1);
                    }
                    Class_ProperityLayer.PTAGNO1 = string.Empty;
                    WB_readerAPI.Actions.PurgeTags();
                    ResetAll();
                    Thread.Sleep(2000);

                }
                return false;
            }
            else
            {
                lblAntennaStatus_WBIn_Text.Text = strErrorMessage_WB;
                lblAntennaStatus_WBIn_Text.Text = "Invlid Tag ID '" + tagIDWB + "'";
                lblRFIDTagValue_WBOut_Text.Text = string.Empty;
                lblAntenna_WBOut_Text.Text = string.Empty;
                lblAntenna_WBOut_Time_Text.Text = string.Empty;
                if (errorflag == 1)
                {
                    SaveJunkData(tagIDWB, Class_ProperityLayer.PWBNO1);
                }
                Class_ProperityLayer.PTAGNO1 = string.Empty;
                WB_readerAPI.Actions.PurgeTags();
                ResetAll();
                Thread.Sleep(2000);

            }
            return false;
        }
        public void ResetAll()
        {
            Class_ProperityLayer.OperType1 = string.Empty;
            Class_ProperityLayer.TaskCode1 = string.Empty;
            Class_ProperityLayer.SubTaskCode1 = string.Empty;
            Class_ProperityLayer.ReaderID1 = string.Empty;
            Class_ProperityLayer.ReaderIP1 = string.Empty;
            Class_ProperityLayer.Location1 = string.Empty;
        }
        private void SaveJunkData(string tagnum, string wbnum)
        {
            ResetAll();
            //try
            //{
            //    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            //    {
            //        Sqlcon.Open();
            //    }
            //    string strNonQuery_PortEntry = "Insert into  [dbo].[MTFX9500JunkData] ([tagno],[Ant],[wbno],[readdt],[Reader])" + "Values(" + "'" + tagnum + "'" + "," + "'" + Class_ProperityLayer.AntNo1 + "'" + "," + "'" + wbnum + "'" + ",GETDATE(),'" + strWBIPaddress + "')";
            //    SqlCommand Sqlcmd = new SqlCommand(strNonQuery_PortEntry, Sqlcon);
            //    Sqlcmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    lblErrorMessage.Text = ex.Message;
            //}
        }
        //Check the Tag is either in Internal fleet or By Road 
        private bool FunctionBlnValidTag_WB(string inParamStrTag, string inParamStrInOrOut, out string outParamErrorMessage, out bool outParamBlnIsValid)
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
             
                string com1 = "SELECT c.TruckNo,a.TaskCode,a.SubTaskCode  FROM  [dbo].[Tag_TruckAllocation]a,  [dbo].[TagMaster] b, [dbo].[TruckMaster] c where a.TagId = b.TagId and a.Tkid=c.Tkid and b.TagNo ='" + Class_ProperityLayer.PTAGNO1 + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, Sqlcon);
                DataSet ds1 = new DataSet();
                string command = "select  TOP 1 [tagno],[Ant],[wbno],[readdt],[truckno] from  [dbo].[MTFX9500InternalFleet] where  [wbno] ='" + Class_ProperityLayer.PWBNO1 + "'  order by readdt desc";
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
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
            
                da1.Fill(ds1);//tmst_FleetMaster
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    Class_ProperityLayer.ParkingFleetNo1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                    Class_ProperityLayer.TaskCode1 = ds1.Tables[0].Rows[0].ItemArray[1].ToString();
                    Class_ProperityLayer.SubTaskCode1 = ds1.Tables[0].Rows[0].ItemArray[2].ToString();
                    //Class_ProperityLayer.AllocFlag1 = ds1.Tables[0].Rows[0].ItemArray[3].ToString();
                    if (newtag == Class_ProperityLayer.PTAGNO1)// && PTruckNo == Class_ProperityLayer.WBFleetNo1
                    {
                        //lblmsg.Text = "same Tag found ";
                        //m_ReaderAPI.Actions.Inventory.Stop();
                        //AccessComplete.WaitOne(3000, false);
                        if (PFlag == 1)
                        {
                            lblAntennaStatus_WBIn_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        else if (PFlag == 2)
                        {
                            lblAntennaStatus_WBOut_Text.Text = Class_ProperityLayer.PTAGNO1 + " & " + Class_ProperityLayer.ParkingFleetNo1 + " has read Same Tag with Same Truck Found..";
                        }
                        errorflag = 0;
                        Thread.Sleep(2000);
                        outParamBlnIsValid = false;
                        WB_readerAPI.Actions.PurgeTags();
                        return false;

                    }
                    else
                    {
                        errorflag = 0;
                        outParamBlnIsValid = true;
                        TruckFlag = 1;
                        GetTaskData();
                        return true;
                    }
                }
                else
                {
                    errorflag = 0;
                    outParamBlnIsValid = true;
                    TruckFlag = 3;

                    return true;
                }

            }
            catch (Exception ex)
            {
                errorflag = 1;
                outParamErrorMessage = ex.Message.ToString();
                return false;
            }
        }

        #endregion

        #region gettaskdata
        private void GetTaskData()
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string com1 = "SELECT ReaderId from  [dbo].[ReaderMaster] where ReaderIP='" + strWBIPaddress + "' and RStatus='A'";
                SqlDataAdapter da1 = new SqlDataAdapter(com1, Sqlcon);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Class_ProperityLayer.ReaderID1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                    Class_ProperityLayer.ReaderIP1 = strWBIPaddress;
                    Class_ProperityLayer.Location1 = "0";
                }
                if (Class_ProperityLayer.ReaderID1 != "")
                {
                    if (Class_ProperityLayer.TaskCode1 == "")
                    {
                        Class_ProperityLayer.OperType1 = "Dust";
                        Class_ProperityLayer.TaskCode1 = "0";
                        Class_ProperityLayer.SubTaskCode1 = "0";
                        string emptycom1 = "SELECT ReaderId from  [dbo].[ReaderMaster] where ReaderIP='" + strWBIPaddress + "' and RStatus='A'";
                        SqlDataAdapter emptyda1 = new SqlDataAdapter(emptycom1, Sqlcon);
                        DataSet emptyds1 = new DataSet();
                        emptyda1.Fill(emptyds1);
                        if (emptyds1.Tables[0].Rows.Count > 0)
                        {
                            Class_ProperityLayer.ReaderID1 = emptyds1.Tables[0].Rows[0].ItemArray[0].ToString();
                            Class_ProperityLayer.ReaderIP1 = strWBIPaddress;
                            Class_ProperityLayer.Location1 = "0";
                        }
                    }
                    else
                    {

                        string taskcom1 = "SELECT d.OperationType,d.ReaderId,e.ReaderIP,d.LocationId from  [dbo].[Task_Tbl] a INNER JOIN  [dbo].[Fleet_Allocation_Tbl] b ON a.TaskCode=b.TaskCode Inner join  [dbo].[Fleet_Allocation_Dtl_Tbl] c on b.AllocationCode=c.AllocationCode Inner join  [dbo].[Reader_Allocation_Dtl_Tbl] d on b.AllocationCode=d.AllocationCode Inner join  [dbo].[ReaderMaster] e on d.ReaderId=e.Readerid where a.TaskCode=" + Class_ProperityLayer.TaskCode1 + " and c.FleetNo='" + Class_ProperityLayer.ParkingFleetNo1 + "' and d.ReaderId=" + Class_ProperityLayer.ReaderID1 + " and d.RAStatus='Allocated'";
                        SqlDataAdapter taskda1 = new SqlDataAdapter(taskcom1, Sqlcon);
                        DataSet taskds1 = new DataSet();
                        taskda1.Fill(taskds1);
                        if (taskds1.Tables[0].Rows.Count > 0)
                        {
                            Class_ProperityLayer.OperType1 = taskds1.Tables[0].Rows[0].ItemArray[0].ToString();
                            Class_ProperityLayer.ReaderID1 = taskds1.Tables[0].Rows[0].ItemArray[1].ToString();
                            Class_ProperityLayer.ReaderIP1 = taskds1.Tables[0].Rows[0].ItemArray[2].ToString();
                            Class_ProperityLayer.Location1 = taskds1.Tables[0].Rows[0].ItemArray[3].ToString();
                        }
                        else
                        {
                            Class_ProperityLayer.OperType1 = "Dust";
                            Class_ProperityLayer.TaskCode1 = "0";
                            Class_ProperityLayer.SubTaskCode1 = "0";
                            Class_ProperityLayer.Location1 = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Load
        private void InsertLoadData()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string command = "select  TOP 1 [TagNo],[TruckNo] from  [dbo].[InternalFleet_Load] where ReaderIP='" + Class_ProperityLayer.ReaderIP1 + "' order by ReadTime desc";
            SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tagno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (tagno == Class_ProperityLayer.PTAGNO1)
                {

                }
                else
                {
                    string sql = "Insert into  [dbo].[InternalFleet_Load] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                    SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into  [dbo].[InternalFleet_Load] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region UnLoad
        private void InsertUnLoadData()
        {

            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string command = "select  TOP 1 [TagNo],[TruckNo] from  [dbo].[InternalFleet_UnLoad] where ReaderIP='" + Class_ProperityLayer.ReaderIP1 + "'  order by ReadTime desc";
            SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tagno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (tagno == Class_ProperityLayer.PTAGNO1)
                {

                }
                else
                {
                    string sql = "Insert into  [dbo].[InternalFleet_UnLoad] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                    SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into  [dbo].[InternalFleet_UnLoad] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Verification
        private void InsertVerificationData()
        {

            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string command = "select  TOP 1 [TagNo],[TruckNo] from  [dbo].[InternalFleet_Verification] where ReaderIP='" + Class_ProperityLayer.ReaderIP1 + "' order by ReadTime desc";
            SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tagno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (tagno == Class_ProperityLayer.PTAGNO1)
                {

                }
                else
                {
                    string sql = "Insert into  [dbo].[InternalFleet_Verification] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                    SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into  [dbo].[InternalFleet_Verification] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Dust
        private void InsertDustData()
        {

            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string command = "select  TOP 1 [TagNo],[TruckNo] from  [dbo].[InternalFleet_Dust] where ReaderIP='" + Class_ProperityLayer.ReaderIP1 + "' order by ReadTime desc";
            SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tagno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (tagno == Class_ProperityLayer.PTAGNO1)
                {

                }
                else
                {
                    string sql = "Insert into  [dbo].[InternalFleet_Dust] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                    SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into  [dbo].[InternalFleet_Dust] (TagNo,[TruckNo],[TaskCode],SubTaskCode,ReaderOperation,ReaderNo,ReaderIP,Location,ReadTime,SyncTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "','" + Class_ProperityLayer.ParkingFleetNo1 + "'," + Class_ProperityLayer.TaskCode1 + "," + Class_ProperityLayer.SubTaskCode1 + ",'" + Class_ProperityLayer.OperType1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "'," + Class_ProperityLayer.Location1 + ",'" + dtime + "','" + dtime + "','Active','Admin','" + dtime + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region junk Data
        private void InsertjunkData()
        {

            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string com1 = "SELECT ReaderId from  [dbo].[ReaderMaster] where ReaderIP='" + strWBIPaddress + "' and RStatus='A'";
            SqlDataAdapter da1 = new SqlDataAdapter(com1, Sqlcon);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Class_ProperityLayer.ReaderID1 = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                Class_ProperityLayer.ReaderIP1 = strWBIPaddress;
            }
            string command = "select  TOP 1 [TagNo] from  [dbo].[FR_JunkData] where ReaderIP='" + Class_ProperityLayer.ReaderIP1 + "' order by ReadTime desc";
            SqlDataAdapter da = new SqlDataAdapter(command, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tagno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (tagno == Class_ProperityLayer.PTAGNO1)
                {

                }
                else
                {
                    string sql = "Insert into  [dbo].[FR_Junkdata] (TagNo,Readerid,ReaderIP,ReadTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "','" + dtime + "','Active','Admin','" + dtime + "')";
                    SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = "Insert into  [dbo].[FR_Junkdata] (TagNo,Readerid,ReaderIP,ReadTime,[TStatus],[CreatedBy],[CreatedDate]) values ('" + Class_ProperityLayer.PTAGNO1 + "'," + Class_ProperityLayer.ReaderID1 + ",'" + Class_ProperityLayer.ReaderIP1 + "','" + dtime + "','Active','Admin','" + dtime + "')";
                SqlCommand cmd = new SqlCommand(sql, Sqlcon);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

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
                            CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Reconnect", strWBIPaddress, strWBPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                            ConnectErrLog = 0;
                            DisConnectErrLog = 0;
                        }
                        //CatchException(string.Format("{0} - {1} - {2} - {3} - {4} - {5}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, hdnGPID.Text, GlobalVariables.versionLog));
                        btnWBAreaReaderConnect_Click(sender, e);
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    lblmsg.Text = "Network Disconnected!";
                    if (DisConnectErrLog == 0)
                    {
                        CatchException(string.Format("{0},{1},{2},{3},{4}", "Network Disconnect", strWBIPaddress, strWBPort, ConfigurationManager.AppSettings["WBIN"], DateTime.Now));
                        DisConnectErrLog = 1;
                    }
                    btnWBAreaReaderDisconnect_Click(sender, e);
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

    }

}
