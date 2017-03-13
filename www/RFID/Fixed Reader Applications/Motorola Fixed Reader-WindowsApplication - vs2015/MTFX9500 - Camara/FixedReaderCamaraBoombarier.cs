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
using System.Linq;
//using WinSCP;
namespace CS_RFID3_Host_Sample1
{
    public partial class FixedReaderCamaraBoombarier : Form
    {
        #region "Variable Decleration"
        SqlConnection Sqlcon; SqlCommand cmd; static string Sqlstr = Class_ProperityLayer.SqlConn_String;//Connection details

        string strReaderIPaddress = classGlobalVariables.Reader_IP;
        string strReaderPort = classGlobalVariables.ReaderPort;
        string strReaderAntennaSet1 = classGlobalVariables.AntenaSet1Loc;
        string strReaderAntennaSet2 = classGlobalVariables.AntenaSet2Loc;

        RFIDReader Parking_readerAPI;

        int fixant = 0; int PFlag = 0, TruckFlag = 0;

        bool blnIsSuccess_Parking = false;

        int errorflag = 0;

        string strErrorMessage_Parking = "";
        #endregion
        #region "Network Connection"
        Ping ping = new Ping();
        PingOptions options = new PingOptions { DontFragment = true };

        //just need some data. this sends 10 bytes.
        byte[] buffer = Encoding.ASCII.GetBytes(new string('z', 10));
        string host;
        int nwflag = 0;
        int ConnectErrLog = 0;
        int DisConnectErrLog = 0;
        #endregion
        public FixedReaderCamaraBoombarier()
        {
            classLog.writeLog("Message @: Screen Load Started. ");
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
            lblLoc1.Text = classGlobalVariables.AntenaSet1Loc;
            lblLoc2.Text = classGlobalVariables.AntenaSet2Loc;
            this.Text = classGlobalVariables.AntenaSet1Loc + " and " + classGlobalVariables.AntenaSet2Loc;
        }
        #endregion
        #region "Reader Connection"
        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            //classFTPOperstaions.FileDelete("ftp://172.168.8.60/Entry Lane - 01/image16-11-11_10-49-06-23.jpg");
            //classFTPOperstaions.FileDelete("ftp://172.168.8.60/Entry Lane - 01/image16-11-11_10-49-04-79.jpg");
            //classFTPOperstaions.FileDelete("ftp://172.168.8.60/Entry Lane - 01/image16-11-11_10-49-03-31.jpg");
            //classFTPOperstaions.FileDelete("ftp://172.168.8.60/Entry Lane - 01/image16-11-11_10-49-00-35.jpg");
            //classFTPOperstaions.FileDelete("ftp://172.168.8.60/Entry Lane - 01/image16-11-11_10-49-01-87.jpg");
            nwflag = 1;
            //tmrConnection.Enabled = true;
            btnDisconnect.Enabled = true;
            btnConnect.Enabled = false;
            if (timerReader.Enabled == false)
            {
                FunctionDisconnectReader();
                if (FunctionRFIDConnection_Parking())
                {
                    lblReaderStatusText.Text = "# Connected";
                    }
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
                {
                    
                    Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                    Parking_readerAPI.Disconnect();                    
                }
               

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
                lblErrorMessageText.Text = "Exception : Connection Already Exists...";
                classLog.writeLog("Error @: Connection Already Exists with the reader. Please check.");
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
                Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
                Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
              
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
                        if (this.dataGridViewTagDetails.Rows.Count > 3) this.dataGridViewTagDetails.Rows.Clear();
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
                            //Class_ProperityLayer.AntNo1 = tag.AntennaID;
                            string tagID = tag.TagID; // getting tagid here 113370000000000000000000
                            string tagno = tagData[nIndex].TagID;
                            string RSSI = tag.PeakRSSI.ToString();
                            string Tagno = tagno.Replace(" ", "").Substring(0, 5);
                            
                            //Class_ProperityLayer.DupTagNo1 = Tagno;//get the tagnum to restreict duplicate values
                            
                            //Class_ProperityLayer.PTAGNO1 = Tagno;
                            string Ant = tag.AntennaID.ToString();
                            //string LOC = ConfigurationManager.AppSettings["loc"];
                            //string Reader = ConfigurationManager.AppSettings["fixreaderip"];
                            //string Reader = MyDataCollection.ReaderIP;
                            CatchData(string.Format("{0},{1},{2},{3},{4}", "DATA LOG", Tagno, Ant, RSSI, DateTime.Now));

                            if (int.Parse(Ant) >= 1 && int.Parse(Ant) <= 2)
                            {
                               // classGlobalVariables.AntenaSet1OldTag = "00";
                                if (classGlobalVariables.AntenaSet1OldTag != Tagno)
                                {
                                    //fixant = 1;
                                    //WBNO = ConfigurationManager.AppSettings["WBIN"];
                                    //Class_ProperityLayer.PWBNO1 = WBNO;
                                    //PFlag = 1;
                                    string newImagePath = classFTPOperstaions.GetDirectoryListing(classGlobalVariables.AntenaSet1Loc_CamaraFTP_Path, classGlobalVariables.AntenaSet1Loc_SaveImage_Path);
                                    pictureBoxImgatLoc1.ImageLocation = newImagePath;
                                    pictureBoxImgatLoc1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    string MapingId = classValidations.getMappingId("1");
                                    if(MapingId!="0")
                                    {
                                        string AccessStatus = classValidations.ValidateGateAcces(Tagno, MapingId,newImagePath);
                                        if (AccessStatus == "Valid")
                                        {
                                            //--Camar Pulse Start---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_Camara_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
                                            Thread.Sleep(1000);
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_Camara_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Camar Pulse Stop---
                                            //--Red Light Pulse Stop---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Green Light Pulse Start---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
                                            //Thread.Sleep(1000);
                                            //Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Green Light Pulse Stop---
                                            this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Tag captured successfully- Access Granted"); 

                                        }
                                        else this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Tag captured successfully-Access Denied");
                                    }
                                        string SaveTransactionStatus = classSaveTransactionDetails.SavePhotoLog(Tagno, Ant, classGlobalVariables.AntenaSet1Loc, classGlobalVariables.RaederIP, newImagePath);
                                        classGlobalVariables.AntenaSet1OldTag = Tagno;                                                                   
                                    
                                }
                                else this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Duplicate Tag found.");
                                
                            }
                            else if (int.Parse(Ant) >= 3 && int.Parse(Ant) <= 4)
                            {
                                if (classGlobalVariables.AntenaSet2OldTag != Tagno)
                                {
                                    //fixant = 2;
                                    //WBNO = ConfigurationManager.AppSettings["WBOUT"];
                                    //Class_ProperityLayer.PWBNO1 = WBNO;
                                    //PFlag = 2;
                                    string newImagePath = classFTPOperstaions.GetDirectoryListing(classGlobalVariables.AntenaSet2Loc_CamaraFTP_Path, classGlobalVariables.AntenaSet2Loc_SaveImage_Path);
                                    pictureBoxImgatLoc2.ImageLocation = newImagePath;
                                    pictureBoxImgatLoc2.SizeMode = PictureBoxSizeMode.StretchImage;
                                    string MapingId = classValidations.getMappingId("2");
                                    if (MapingId != "0")
                                    {
                                        string AccessStatus = classValidations.ValidateGateAcces(Tagno, MapingId,newImagePath);
                                        if (AccessStatus == "Valid")
                                        {
                                            //--Camar Pulse Start---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_Camara_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
                                            Thread.Sleep(1000);
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_Camara_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Camar Pulse Stop---
                                            //--Red Light Pulse Stop---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_RedLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Green Light Pulse Start---
                                            Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet2Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.TRUE;
                                            //Thread.Sleep(1000);
                                            //Parking_readerAPI.Config.GPO[classGlobalVariables.AntenaSet1Loc_GreenLight_GPIO_PortNo].PortState = GPOs.GPO_PORT_STATE.FALSE;
                                            //--Green Light Pulse Stop---
                                            this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Tag captured successfully- Access Granted");
                                        }
                                        else this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Tag captured successfully- Access Denied");
                                    }
                                    classGlobalVariables.AntenaSet2OldTag = Tagno;
                                    string SaveTransactionStatus = classSaveTransactionDetails.SaveTransaction(Tagno, Ant, classGlobalVariables.AntenaSet2Loc, classGlobalVariables.RaederIP, newImagePath);
                                    
                                }
                                else this.dataGridViewTagDetails.Rows.Add(tag.AntennaID, tag.TagID.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Tagno + " Duplicate Tag found.");
                            }

                        }
                    }
                 }
            }
            catch (Exception ex)
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
