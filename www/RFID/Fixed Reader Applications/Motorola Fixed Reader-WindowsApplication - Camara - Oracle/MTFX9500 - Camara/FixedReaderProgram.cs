﻿using System;
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
    public partial class FixedReaderProgram : Form
    {
        #region "Variable Decleration"
        SqlConnection Sqlcon; SqlCommand cmd; static string Sqlstr = Class_ProperityLayer.SqlConn_String;//Connection details

        string strReaderIPaddress = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderIPAddress").ToString();
        string strReaderPort = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_ReaderPort").ToString();
        string strReaderAntennaSet1 = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaIn").ToString();
        string strReaderAntennaSet2 = System.Configuration.ConfigurationSettings.AppSettings.Get("Parking_AntennaOut").ToString();

        RFIDReader Parking_readerAPI;

        int fixant = 0; int PFlag = 0, TruckFlag = 0;

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
        public FixedReaderProgram()
        {
            InitializeComponent();
            timerNetworkConnection.Enabled = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        #region "Form Load"
        private void FixedReaderCamara_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);
            lblReaderIPText.Text = strReaderIPaddress;
            MyDataCollection.ReaderIP = strReaderIPaddress;
            lblReaderPortText.Text = strReaderPort;
            //lblAntennaSet1Value.Text = strReaderAntennaSet1;
            //lblAntennaSet2Value.Text = strReaderAntennaSet2;
            //lblWBOneValue.Text = ConfigurationManager.AppSettings["WBIN"];
            //lblWBTwoValue.Text = ConfigurationManager.AppSettings["WBOUT"];
            this.Text = ConfigurationManager.AppSettings["WBIN"] + " and " + ConfigurationManager.AppSettings["WBOUT"];
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
                    lblReaderStatusText.Text = "# Connected";
                else
                    lblReaderStatusText.Text = "# Not Connected";
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
                lblErrorMessageText.Text = "Exception : Network Problem or Connection Already Exists...";
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
            lblReaderStatusText.Text = "# Not Connected";
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
                    lblReaderStatusText.Text = "# Connected"; lblErrorMessageText.Text = "-";
                //else
                //lblReaderStatus_Parking_Text.Text = "# Not Connected";
            }
            catch (Exception ex)
            {
                lblReaderStatusText.Text = "# Not Connected";
                lblErrorMessageText.Text = ex.ToString();
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
                        if (this.dataGridViewTagDetails.Rows.Count > 13) this.dataGridViewTagDetails.Rows.Clear();
                        Symbol.RFID3.TagData tag = tagData[nIndex];
                        if (Class_ProperityLayer.DupTagNo1 == tagData[nIndex].TagID.Replace(" ", "").Substring(0, 5))
                        {
                            this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Duplicate Tag Found.");
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
                            this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Tag captured successfully");
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
                            else if (int.Parse(Ant) >= 3 && int.Parse(Ant) <= 4)
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
        #endregion

        #region "Network Connectivity Check"
        //timer check only connection
        private void timerNetworkConnection_Tick(object sender, EventArgs e)
        {
            try
            {
                host = strReaderIPaddress;
                var reply = ping.Send(host, 999, buffer, options);
                if (reply == null)
                {
                    //MessageBox.Show("Reply was null");
                    lblNetworkStatusText.Text = "Reply was null";
                    return;
                }

                if (reply.Status == IPStatus.Success)
                {
                    lblErrorMessageText.Text = "";
                    lblNetworkStatusText.Text = "Network Connected!";
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
                    lblNetworkStatusText.Text = "Network Disconnected!";
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
                lblNetworkStatusText.Text = ex.Message;
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
            lblReaderStatusText.Text = "# Not Connected";
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
