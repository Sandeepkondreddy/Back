/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-KPCL
Purpose:	        Internal Fleet
Created Date:		23 May 2014 
Updated Date:		01 July 2015 
******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using com.caen.RFIDLibrary;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using PsionTeklogix.RFID;
using System.IO;
using System.Net;
using WAP3.RFIDWEB;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WAP3
{
    public partial class formInternal : Form
    {
        #region Properties
        int ComPort;
        bool bOpen = false;
        // Read ID Options  (default)
        PTEMEAMS.Designs.ReadIDOptionsPanel.ReadIDOptions opReadIDOptions;

        // Read ID: Chrono options
        private bool bActiveChrono = false;
        private int iNbTagsToRead = 10;
        private int StartTime = 0;

        // Read ID: Display
        private bool bASCIIDisplay = false;
        private bool bHexadecimalDisplay = true;

        // Read ID: Session
        private bool bAlternate = false;
        private bool bReset = false;

        // Read ID: EPC Filter Options
        private bool bActiveEPCFilter = false;
        private byte[] Mask = new byte[] { 0x00 };
        private short MaskLenght = 8;
        private short Position = 0;

        // Read / Write Options  (default)
        PTEMEAMS.Designs.ReadWriteOptionsPanel.ReadWriteOptions opReadWriteOptions;

        // R/W: Bank Memory
        enum BankMemory
        {
            Reserved,
            EPC,
            TID,
            USER
        }
        BankMemory CurrentBankMemory = BankMemory.EPC;


        // CAEN READER
        com.caen.RFIDLibrary.CAENRFIDReader Reader = null;
        CAENRFIDLogicalSource m_Source0 = null;
        CAENRFIDTag[] m_RFIDTags;

        // Tags collection
        ArrayList ListTags = new ArrayList();

        // Read/Write option depending the kind of tags (by default C1G2 values)
        private int addressEPC = 0x04;
        private int nByteEPC = 0x0c;

        // Read TID (32bits or 64 bits)
        private int addressTID = 0x00;
        private int nByteTID = 0x04;

        // Read USER (96bits or 512 bits)
        private int addressUSER = 0x00;
        private int nByteUSER = 0x0c;

        // Sound
        private PTEMEAMS.Multimedia.SoundWrapper Sound;

        // boolean to know in we are currently in the load
        private bool bLoad = true;

        // FW version reference
        Version versionRef = new Version(2, 1, 2);

        // modules
        ArrayList Modules;

        // get wakeup event to reinitialize the CAEN reader after Resume
        PTEMEAMS.Windows.WakeUp WakeUp = new PTEMEAMS.Windows.WakeUp();
        #endregion

        #region Signal Strenth
        public void signal()
        {
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                strength = connection.SignalStrength;
            }
            catch (Exception ex)
            {
                _lbReadInfoReadWrite.Text = "Wifi Not Available."; 
                strength = -99;
            }
            //strength = -30;
            //_lbReadInfoReadWrite.Text = connection.SignalStrength.ToString();
        }
        #endregion
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string gprsSqlConnection = classServerDetails.GPRSSQLConnection;
        string User = classLogin.User;//"Sandeep";
        string ConnType = classLogin.ConnType;
        string readerip = classLogin.ReaderIP;
        string readerno;
        int strength;
        int count;
        SqlCeConnection dbCon;
        int TruckTurnAroundTimeDiff=2;
        int DataSyncTimeDiff;
        string ReaderName;
        SqlConnection dbCon1;
        public formInternal(int ComPort)
        {
            InitializeComponent();
            InitGraphics();
            
            this.ComPort = ComPort;
            // Definition of the options 
            opReadIDOptions = readIDOptionsPanel1.Options;
            opReadWriteOptions = readWriteOptionsPanel1.Options;
            string CONN_STRING = gprsSqlConnection;
            dbCon1 = new SqlConnection(CONN_STRING);
            btnSave.Visible = false;
        }
        public void getTimeDiff_Dtl()
        {
            string sql = "select TimeDiffinMin from TimeConfig where Type='TruckTurnAroundTimeDiff'";
            SqlCeCommand cmd = new SqlCeCommand(sql, dbCon);
            TruckTurnAroundTimeDiff = int.Parse(cmd.ExecuteScalar().ToString());
            cmd.Dispose();

            string sql1 = "select TimeDiffinMin from TimeConfig where Type='DataSyncTimeDiff'";
            SqlCeCommand cmd1 = new SqlCeCommand(sql1, dbCon);
            DataSyncTimeDiff = int.Parse(cmd1.ExecuteScalar().ToString());
            cmd1.Dispose();
            
        }

        public void getReaderDetails()
        {
           
                        string cmd = "select [ReaderIP] from [ReaderConfig] where [Status]='A'";
                        SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        readerip = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        da.Dispose();
                        ds.Dispose();
                        string cmd1 = "select [ReaderId] from ReaderConfig ";
                        try
                        {
                            SqlCeDataAdapter da1 = new SqlCeDataAdapter(cmd1, dbCon);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            ReaderId = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                            
                            da1.Dispose();
                            ds1.Dispose();
                        }
                        catch
                        {
                            ReaderId = "0";
                        }
                      
          
        }
       
        #region Load/Closing
        public void gethandledetailsfromlocaldb()
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string cmd = "select ReaderNo,ReaderIP,ReaderId,ReaderName  from ReaderConfig where Status='A'";

            try
            {
                
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                readerno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                readerip = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                ReaderId = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                ReaderName = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                if (ReaderId == "")
                {
                    MessageBox.Show("Reconfigure the Reader Config.");
                    classLog.writeLog("Error @:Internal-ReaderId Missing from the Reader Config");
                    _btReadReadWrite.Enabled = false;
                    _btClearReadWrite.Enabled = false;
                }
               
            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }
       
        private void YardOps_Load(object sender, EventArgs e)
        {
            classLog.writeLog("Message @:Internal Form Loaded");
            _btReadReadWrite.Focus();
            btnSave.Visible = false;
             dbCon = new SqlCeConnection(localConnection);
             dbCon.Open();
             getTimeDiff_Dtl();
            gethandledetailsfromlocaldb();
            try
            {
                this.Enabled = false;
                this.Show(); this.Refresh();

                bLoad = true;

                // Init Sound
                string SoundFile = @"\Windows\exclam.wav";
                try
                {
                    Sound = new PTEMEAMS.Multimedia.SoundWrapper(SoundFile);
                }
                catch
                {
                    MessageBox.Show("Error loading sound file : \n" + SoundFile);
                }

                Clear();


                Reader = new CAENRFIDReader();

                OpenReader();
                Display("Reader opened (COM" + this.ComPort.ToString() + ":)", Images.Success);


                // Display Protocol
                if (bOpen)
                {
                    // Necessary timeout for the reader to be initialized
                    System.Threading.Thread.Sleep(250);


                    switch (Reader.GetProtocol())
                    {
                        case CAENRFIDProtocol.CAENRFID_EPC_C1G1:
                            _cbProtocol.SelectedItem = "EPC C1G1";
                            break;
                        case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                            _cbProtocol.SelectedItem = "EPC C1G2";
                            break;
                        case CAENRFIDProtocol.CAENRFID_ISO18000_6b:
                            _cbProtocol.SelectedItem = "ISO 18000-6B";
                            break;
                        default:
                            MessageBox.Show("Error loading Protocol");
                            break;
                    }
                    Display("Get Protocol: OK", Images.Success);


                    if (Reader.GetReaderInfo().GetModel() == "A528" ||
                        Reader.GetReaderInfo().GetModel() == "R1230CB")
                    {
                        // Get RF Regulation and set at the same time all the bitrate available for this RF regulation
                        _cbRFRegulation.Text = Reader.GetRFRegulation().ToString();
                    }
                    else // A828 (50mW)
                    {
                        _cbBitRate.Items.Clear();
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX10RX40");
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX160");

                        switch (Reader.GetBitRate())
                        {
                            case CAENRFIDBitRate.DSB_ASK_FM0_TX10RX40:
                                _cbBitRate.Text = "DSB_ASK_FM0_TX10RX40";
                                break;
                            case CAENRFIDBitRate.DSB_ASK_FM0_TX40RX160:
                                _cbBitRate.Text = "DSB_ASK_FM0_TX40RX160";
                                break;
                            case CAENRFIDBitRate.DSB_ASK_FM0_TX40RX40:
                                _cbBitRate.Text = "DSB_ASK_FM0_TX40RX40";
                                break;
                            default:
                                break;
                        }
                        Display("Get BitRate: OK", Images.Success);
                    }



                    CAENRFIDLogicalSource[] logical_sources = Reader.GetSources();
                    if (logical_sources.Length == 0)
                        throw new Exception("No logical sources");

                    m_Source0 = logical_sources[0];
                    Display("Get Logical Sources: OK", Images.Success);

                    // Default value = 3
                    _txtQvalue.Text = (3).ToString(); // m_Source0.GetQ_EPC_C1G2().ToString();
                    Display("Get Q value: OK", Images.Success);

                    switch (m_Source0.GetSession_EPC_C1G2())
                    {
                        case CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S0:
                            _cbSession.Text = "S0";
                            break;
                        case CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S1:
                            _cbSession.Text = "S1";
                            break;
                        case CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2:
                            _cbSession.Text = "S2";
                            break;
                        case CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3:
                            _cbSession.Text = "S3";
                            break;

                        default:
                            break;
                    }
                    Display("Get Session: OK", Images.Success);

                    try
                    {
                        if (Reader.GetReaderInfo().GetModel() == "A828" ||
                            Reader.GetReaderInfo().GetModel() == "A828EU" ||
                            Reader.GetReaderInfo().GetModel() == "A828US")
                        {
                            // Deactive power combo box for the A828 reader (50 mW)
                            // Power modification is not available in this case
                            _cbPower.Enabled = false;
                            _cbRFRegulation.Enabled = false;
                            _txtReadCycle.Enabled = false;
                        }
                        else
                        {
                            if (Reader.GetReaderInfo().GetModel() == "A828AEU" ||
                                Reader.GetReaderInfo().GetModel() == "A828AUS")
                            {
                                _cbPower.Items.Add("50 mW");
                                _cbPower.Items.Add("200 mW");
                                _cbPower.Text = "200 mW";
                                _cbRFRegulation.Enabled = false;
                                _txtReadCycle.Enabled = false;
                            }
                            else
                            {
                                if (Reader.GetReaderInfo().GetModel() == "A528")
                                {
                                    // it is a 500mW reader
                                    _cbPower.Items.Add("10 mW");
                                    _cbPower.Items.Add("25 mW");
                                    _cbPower.Items.Add("50 mW");
                                    _cbPower.Items.Add("100 mW");
                                    _cbPower.Items.Add("200 mW");
                                    _cbPower.Items.Add("300 mW");
                                    _cbPower.Items.Add("400 mW");
                                    _cbPower.Items.Add("500 mW");
                                    _cbPower.Text = "500 mW";

                                    // show 500mW options
                                    _txtReadCycle.Text = m_Source0.GetReadCycle().ToString();

                                }
                                else
                                {
                                    if (Reader.GetReaderInfo().GetModel() == "R1230CB") // Quark
                                    {
                                        _cbPower.Items.Add("200 mW");
                                        _cbPower.Text = "200 mW";
                                        _cbPower.Enabled = false;

                                    }
                                    else
                                    {
                                        _cbPower.Enabled = false;
                                        Display("Reader unknown: " + Reader.GetReaderInfo().GetModel().ToString(), Images.Fail);
                                        Display("Power can't be modified", Images.Fail);
                                        _cbRFRegulation.Enabled = false;
                                        _txtReadCycle.Enabled = false;
                                    }
                                }
                            }
                        }
                        // focus the first tab of tabControl
                        this.tabControl1.SelectedIndex = 0;
                        loadreadtab();

                    }
                    catch (Exception)
                    {
                    }
                }


                try
                {
                    // Load modules

                    Modules = PTEMEAMS.Modules.ModuleManager.Instance.Get(typeof(PTEMEAMS.Modules.CAENModule));
                    if (Modules != null)
                    {
                        foreach (PTEMEAMS.Modules.CAENModule module in Modules)
                        {
                            ListViewItem lvi = new ListViewItem(new string[] { module.Key, module.Version });
                            _lvModules.Items.Add(lvi);
                        }
                        Display("Plug-ins loaded", Images.Success);
                    }
                    else
                        Display("No Plug-ins found", Images.Success);
                }
                catch (Exception)
                {
                    Display("Error in Plug-ins loading", Images.Fail);
                }

                // Start WakeUp function to reinitialize the CAEN reader after Resume
                WakeUp.Start();
                WakeUp.ResumeEvent += new EventHandler(WakeUp_ResumeEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message + "\nPlease, check the CAEN reader connection on the Xmod.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                tabControl1.Enabled = false;
                Display("Error during the loading", Images.Fail);
            }
            finally
            {
                bLoad = false;
                this.Enabled = true;

            }
            //localdbrecordcount();
            //tttttttttttttt
            //dbCon.Close();
        }

        private delegate void d_ResumeEvent(object sender, EventArgs e);
        void WakeUp_ResumeEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new d_ResumeEvent(WakeUp_ResumeEvent), new object[] { sender, e });
            }
            else
            {
                bool bLoadSaved = bLoad;
                try
                {
                    bLoad = true;
                    Clear();
                    Display("RESUME", Images.Arrow);

                    // Reload settings
                    _cbBitRate_SelectedIndexChanged(null, e);
                    _cbPower_SelectedIndexChanged(null, e);
                    _txtQvalue_TextChanged(null, e);
                    _txtReadCycle_TextChanged(null, e);
                    _cbSession_SelectedIndexChanged(null, e);
                }
                catch (Exception ex)
                {
                    Display(ex.Message, Images.Fail);
                }
                finally
                {
                    bLoad = bLoadSaved;
                }
            }
        }

        private void YardOps_Closing(object sender, CancelEventArgs e)
        {
            dbCon.Close();
            try
            {
                // stop Wakeup
                WakeUp.ResumeEvent -= new EventHandler(WakeUp_ResumeEvent);
                WakeUp.Stop();

                CloseReader();
                Display("Reader closed", Images.Arrow);
                Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            classLog.writeLog("Message @:Internal Form Closed");
        }

        #endregion
        #region 'Configuration' Panel
        private void _cbProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();

                switch (_cbProtocol.SelectedItem.ToString())
                {
                    case "ISO 18000-6B":
                        Reader.SetProtocol(CAENRFIDProtocol.CAENRFID_ISO18000_6b);
                        _btReadIDPanel.Enabled = true;
                        _btReadWritePanel.Enabled = true;
                        addressEPC = 0x08; // First block to the user memory
                        nByteEPC = 0x10;
                        _cbBankMemory.Enabled = false;
                        break;
                    case "EPC C1G1":
                        Reader.SetProtocol(CAENRFIDProtocol.CAENRFID_EPC_C1G1);
                        _btReadIDPanel.Enabled = true;
                        _btReadWritePanel.Enabled = false;
                        Display("'Read/Write' presently unsupported.", Images.Arrow);
                        _cbBankMemory.Enabled = false;
                        break;
                    case "EPC C1G2":

                        if (Reader.GetReaderInfo().GetModel() == "A528")
                        {
                            // Authorize the access of ReadID and R/W
                            // A528 reader doesn't support the setProtocol (only EPC C1 G2)
                            _cbProtocol.Enabled = false;
                        }
                        else
                            Reader.SetProtocol(CAENRFIDProtocol.CAENRFID_EPC_C1G2);

                        _btReadIDPanel.Enabled = true;
                        _btReadWritePanel.Enabled = true;
                        _cbBankMemory.Enabled = true;
                        _cbBankMemory.SelectedItem = "EPC";
                        _cbEPCSize.SelectedItem = "96 bits";
                        _cbTIDSize.SelectedItem = "32 bits";
                        _cbUSERSize.SelectedItem = "96 bits";
                        break;

                    default:
                        break;
                }

                Display("Set Protocol: OK", Images.Success);
            }
            catch (Exception ex)
            {
                Display("Set Protocol: Fail", Images.Fail);
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }

        }

        #region Informations
        private void _ibSoftwareInformation_Click(object sender, EventArgs e)
        {
            Clear();
            Display("App: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, Images.Arrow);
            Display("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() +
                "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0'), Images.Arrow);
        }

        private void _ibReaderInformation_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Display("CAEN Reader Information", Images.Arrow);

                CAENRFIDReaderInfo ri = Reader.GetReaderInfo();

                //CAEN Reader informations

                Display("Model: " + ri.GetModel());
                Display("Serial Number: " + ri.GetSerialNumber().ToString());

                // Check if the current firmware release supports all the functions
                string FirmwareR = Reader.GetFirmwareRelease().ToString();

                Display("FW Version: " + FirmwareR, Images.Arrow);
                if (Reader.GetReaderInfo().GetModel() == "A528")
                {
                    Regex regFirmwareR = new Regex(@"^([\d]+)\.([\d]+)\.([\d]+)$");
                    Match monMatch = regFirmwareR.Match(FirmwareR);
                    if (monMatch.Success)
                    {
                        if (int.Parse(monMatch.Groups[1].Value) > versionRef.Major)
                        { Display("Firmware version: OK", Images.Success); }
                        else
                        {
                            if (int.Parse(monMatch.Groups[1].Value) == versionRef.Major &&
                                int.Parse(monMatch.Groups[2].Value) > versionRef.Minor)
                            { Display("Firmware version: OK", Images.Success); }
                            else
                            {
                                if (int.Parse(monMatch.Groups[1].Value) == versionRef.Major &&
                                    int.Parse(monMatch.Groups[2].Value) == versionRef.Minor &&
                                    int.Parse(monMatch.Groups[3].Value) >= versionRef.Build)
                                { Display("Firmware version: OK", Images.Success); }
                                else
                                    Display("This firmware version may not support all the functionalities of this demo", Images.Fail);
                            }
                        }
                    }
                    else
                        Display("This firmware version may not support all the functionalities of this demo", Images.Fail);
                }

                // Get DLL version of CAENRFIDLibraryPocketPC
                Assembly myAsm = Assembly.Load("CAENRFIDLibraryPocketPC");
                AssemblyName aName = myAsm.GetName();

                // Store the version number in a Version object.
                Version ver = aName.Version;
                Display(string.Format("DLL version: {0}.{1}.{2}", ver.Major, ver.Minor, ver.Build), Images.Arrow);

                // Display reader configuration
                Display("Protocol : " + Reader.GetProtocol().ToString());
                Display("RF Regulation : " + Reader.GetRFRegulation().ToString());
                Display("Power : " + Reader.GetPower().ToString());
                Display("BitRate : " + Reader.GetBitRate().ToString());
                Display("Q Value : " + Reader.GetQ_EPC_C1G2().ToString());

                if (ri.GetModel() == "A528")
                {
                    Display("LBT Mode : " + (Reader.GetLBTMode() == 1 ? "true" : "false"));
                    Display("RF Channel : " + Reader.GetRFChannel().ToString());
                }
                if (m_Source0 != null)
                {
                    Display("");
                    Display("------ Logical Source");
                    Display("Session : " + m_Source0.GetSession_EPC_C1G2().ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }
        #endregion

        private void _cbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();
                switch (_cbSession.SelectedItem.ToString())
                {
                    case "S0":
                        m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S0);
                        _cbSessionAlternate.Visible = false;
                        _cbSessionAlternate.Checked = false;
                        break;
                    case "S1":
                        m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S1);
                        _cbSessionAlternate.Visible = false;
                        _cbSessionAlternate.Checked = false;
                        break;
                    case "S2":
                        m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2);
                        _cbSessionAlternate.Visible = true;
                        break;
                    case "S3":
                        m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3);
                        _cbSessionAlternate.Visible = true;
                        break;
                    default:
                        break;
                }
                Display("Set session: OK", Images.Success);
            }
            catch (Exception)
            {
                Display("Set session: Fail", Images.Fail);
            }
        }

        private void _cbActiveEPCFilter_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();
                label11.Visible = _cbActiveEPCFilter.Checked;
                label12.Visible = _cbActiveEPCFilter.Checked;
                label13.Visible = _cbActiveEPCFilter.Checked;
                _txtMask.Visible = _cbActiveEPCFilter.Checked;
                _txtMaskLenght.Visible = _cbActiveEPCFilter.Checked;
                _txtPosition.Visible = _cbActiveEPCFilter.Checked;
                _btSetEPCFilter.Visible = _cbActiveEPCFilter.Checked;
                bActiveEPCFilter = _cbActiveEPCFilter.Checked;

                if (bActiveEPCFilter &&
                    Reader.GetReaderInfo().GetModel() != "A528")
                    m_Source0.SetSelected_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SELECTED_YES);
                else
                    m_Source0.SetSelected_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_ALL_SELECTED);

                Display("Set Selected: OK", Images.Success);
            }
            catch (Exception)
            {
                Display("Set Selected: Fail", Images.Fail);
            }
        }

        private void _cbSessionAlternate_CheckStateChanged(object sender, EventArgs e)
        {
            bAlternate = _cbSessionAlternate.Checked;
            _cbSessionReset.Visible = _cbSessionAlternate.Checked; ;
        }

        private void _rbHexadecimal_CheckedChanged(object sender, EventArgs e)
        {
            bHexadecimalDisplay = _rbHexadecimal.Checked;
        }

        private void _rbASCII_CheckedChanged(object sender, EventArgs e)
        {
            bASCIIDisplay = _rbASCII.Checked;
        }

        private void _cbSessionReset_CheckStateChanged(object sender, EventArgs e)
        {
            bReset = _cbSessionReset.Checked;
        }

        private void _cbBitRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();
                if (Reader.GetReaderInfo().GetModel() != "A528")
                {
                    switch (_cbBitRate.SelectedItem.ToString())
                    {
                        case "DSB_ASK_FM0_TX10RX40":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX10RX40);
                            break;
                        case "DSB_ASK_FM0_TX40RX40":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX40RX40);
                            break;
                        case "DSB_ASK_FM0_TX40RX160":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX40RX160);
                            break;
                        default:
                            break;
                    }
                }
                else // if A528 RFID CAEN Reader - 500 mW
                {
                    switch (_cbBitRate.SelectedItem.ToString())
                    {
                        case "DSB_ASK_FM0_TX40RX40":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX40RX40);
                            break;
                        case "DSB_ASK_FM0_TX40RX160":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX40RX160);
                            break;
                        case "DSB_ASK_FM0_TX160RX400":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_FM0_TX160RX400);
                            break;
                        case "DSB_ASK_M2_TX40RX160":
                            Reader.SetBitRate(CAENRFIDBitRate.DSB_ASK_M2_TX40RX160);
                            break;
                        case "PR_ASK_M4_TX40RX250":
                            Reader.SetBitRate(CAENRFIDBitRate.PR_ASK_M4_TX40RX250);
                            break;
                        case "PR_ASK_M4_TX40RX300":
                            Reader.SetBitRate(CAENRFIDBitRate.PR_ASK_M4_TX40RX300);
                            break;
                        case "PR_ASK_M2_TX40RX250":
                            Reader.SetBitRate(CAENRFIDBitRate.PR_ASK_M2_TX40RX250);
                            break;
                        default:
                            break;
                    }
                }
                Display("Set BitRate: OK", Images.Success);
            }
            catch (Exception)
            {
                Display("Set BitRate: Fail", Images.Fail);
            }
        }

        private void _txtQvalue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();
                short Q = Convert.ToInt16(_txtQvalue.Text);
                m_Source0.SetQ_EPC_C1G2(Q);
                Display("Set Q: OK", Images.Success);
            }
            catch
            {
                Clear();
                Display("Invalidate value", Images.Fail);
                try
                { _txtQvalue.Text = m_Source0.GetQ_EPC_C1G2().ToString(); }
                catch
                { Display("Impossible to get Q", Images.Fail); }
            }
        }

        private void _cbActiveChrono_CheckStateChanged(object sender, EventArgs e)
        {
            bActiveChrono = _cbActiveChrono.Checked;

            _txtNbTagsToRead.Visible = _cbActiveChrono.Checked;
            label7.Visible = _cbActiveChrono.Checked;
        }

        private void _txtNbTagsToRead_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iNbTagsToRead = Convert.ToInt32(_txtNbTagsToRead.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                _txtNbTagsToRead.Text = iNbTagsToRead.ToString();
            }
        }

        private void _btSetEPCFilter_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (_txtMask.Text.Length % 2 != 0)
                        throw new Exception("Lenght of Mask is invalid.\nUse multiple of bytes.\nex: 55 or 5522 or 552233 etc...");

                    Mask = new byte[_txtMask.Text.Length / 2];
                    for (int i = 0; i < _txtMask.Text.Length; i += 2)
                    {
                        Mask[i / 2] = byte.Parse(_txtMask.Text.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error setting data in 'Mask' field:\n" + ex.Message);
                }
                try { Position = short.Parse(_txtPosition.Text); }
                catch (Exception ex)
                { throw new Exception("Error setting data in 'Mask Lenght' field:\n" + ex.Message); }
                try { MaskLenght = short.Parse(_txtMaskLenght.Text); }
                catch (Exception ex)
                { throw new Exception("Error setting data in 'Position' field:\n" + ex.Message); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _cbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();

                if (_cbPower.SelectedIndex != -1)
                {
                    Reader.SetPower(int.Parse(_cbPower.SelectedItem.ToString().Replace("mW", "").Trim()));
                }
                Display("Set Power: OK", Images.Success);
                Display("Power measured: " + Reader.GetPower().ToString(), Images.Arrow);
            }
            catch (Exception ex)
            {
                Display("Set Power: Fail", Images.Fail);
                MessageBox.Show(ex.Message);
            }
        }

        private void _cbRFRegulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool SavebLoad = bLoad;

            // save temporarily the current RF Regulation if the reader is locked
            CAENRFIDRFRegulations SaveRFRegulation = CAENRFIDRFRegulations.ETSI_302208;
            try
            {
                if (!bLoad) Clear();
                bLoad = true;

                SaveRFRegulation = Reader.GetRFRegulation();

                _cbBitRate.Items.Clear();
                switch (_cbRFRegulation.SelectedItem.ToString())
                {
                    case "ETSI_302208": //0
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("DSB_ASK_M2_TX40RX160");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX300");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.ETSI_302208);
                        break;
                    case "ETSI_300220":
                        // Not supported by the A528
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.ETSI_300220);
                        break;
                    case "FCC_US": //2
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX160RX400");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M2_TX40RX250");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.FCC_US);
                        break;
                    case "MALAYSIA": //3
                        // Not supported 
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.MALAYSIA);
                        break;
                    case "JAPAN": //4
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX300");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.JAPAN);
                        break;
                    case "KOREA": //5
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX300");
                        _cbBitRate.Items.Add("PR_ASK_M2_TX40RX250");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.KOREA);
                        break;
                    case "AUSTRALIA": //6
                        // Not supported
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.AUSTRALIA);
                        break;
                    case "CHINA": //7
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M2_TX40RX250");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.CHINA);
                        break;
                    case "TAIWAN": //8
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("DSB_ASK_M2_TX40RX160");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M2_TX40RX250");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.TAIWAN);
                        break;
                    case "SINGAPORE": //9
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX40RX40");
                        _cbBitRate.Items.Add("DSB_ASK_FM0_TX160RX400");
                        _cbBitRate.Items.Add("PR_ASK_M4_TX40RX250");
                        _cbBitRate.Items.Add("PR_ASK_M2_TX40RX250");
                        Reader.SetRFRegulation(CAENRFIDRFRegulations.SINGAPORE);
                        break;


                    default:
                        break;
                }
                Display("Set RF Regulation: OK", Images.Success);

            }
            catch (Exception)
            {
                Display("Set RF Regulation: Fail", Images.Fail);
                Display("Reader locked", Images.Arrow);

                // Reader seems to be locked, we disable the access to the RF Regulation combo box
                _cbRFRegulation.Text = SaveRFRegulation.ToString();
                _cbRFRegulation.Enabled = false;
            }

            // Reset Power if it is not the call to apply the RF Regulation modification
            if (!SavebLoad)
            {
                try
                {
                    // Only for A528 reader
                    if (Reader.GetReaderInfo().GetModel() == "A528")
                    {
                        // Reset Power
                        RFIDDriver rfidDriver = new RFIDDriver();
                        rfidDriver.Disable();
                        System.Threading.Thread.Sleep(500);
                        rfidDriver.Enable();
                        System.Threading.Thread.Sleep(500);
                        Display("Power reset: OK", Images.Success);

                        // Reload settings
                        _cbPower_SelectedIndexChanged(null, e);
                        _txtQvalue_TextChanged(null, e);
                        _txtReadCycle_TextChanged(null, e);
                        _cbSession_SelectedIndexChanged(null, e);
                    }
                }
                catch (Exception)
                {
                    Display("Power reset: Fail", Images.Fail);
                }
            }

            // Load best bitrate according to the RF regulation
            Display("Load best bitrate", Images.Arrow);
            switch (_cbRFRegulation.SelectedItem.ToString())
            {
                case "ETSI_302208":
                    // faster bitrate
                    _cbBitRate.Text = "DSB_ASK_M2_TX40RX160";
                    break;
                case "FCC_US":
                    // faster bitrate
                    _cbBitRate.Text = "DSB_ASK_FM0_TX160RX400";
                    break;
                case "JAPAN":
                    _cbBitRate.Text = "PR_ASK_M4_TX40RX250";
                    break;
                case "KOREA":
                    _cbBitRate.Text = "PR_ASK_M4_TX40RX250";
                    break;
                case "CHINA":
                    _cbBitRate.Text = "PR_ASK_M4_TX40RX250";
                    break;
                case "TAIWAN":
                    _cbBitRate.Text = "PR_ASK_M4_TX40RX250";
                    break;
                case "SINGAPORE":
                    _cbBitRate.Text = "PR_ASK_M4_TX40RX250";
                    break;
                default:
                    break;
            }

            bLoad = SavebLoad;

        }

        private void _txtReadCycle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bLoad) Clear();
                short rc = Convert.ToInt16(_txtReadCycle.Text);
                m_Source0.SetReadCycle(rc);
                Display("Set Read Cycle: OK", Images.Success);
            }
            catch
            {
                Clear();
                Display("Invalidate value", Images.Fail);
                try
                { _txtReadCycle.Text = m_Source0.GetReadCycle().ToString(); }
                catch
                { Display("Impossible to get Read Cycle", Images.Fail); }
            }

        }

        private void _cbBankMemory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbBankMemory.SelectedIndex != -1)
            {
                _cbEPCSize.Visible = false;
                _cbTIDSize.Visible = false;
                _cbUSERSize.Visible = false;

                _cbUsePassword.Visible = false;
                _txtPassword.Visible = false;
                switch (_cbBankMemory.SelectedIndex)
                {
                    case 0: // Reserved
                        CurrentBankMemory = BankMemory.Reserved;
                        _btWriteReadWrite.Enabled = false;
                        _btReadReadWrite.Enabled = true;
                        break;
                    case 1: // EPC
                        CurrentBankMemory = BankMemory.EPC;
                        _btWriteReadWrite.Enabled = true;
                        _btReadReadWrite.Enabled = true;
                        _cbEPCSize.Visible = true;

                        _cbUsePassword.Visible = true;
                        _txtPassword.Visible = true;
                        break;
                    case 2: // TID
                        CurrentBankMemory = BankMemory.TID;
                        _btWriteReadWrite.Enabled = false;
                        _btReadReadWrite.Enabled = true;
                        _cbTIDSize.Visible = true;
                        break;
                    case 3: // User
                        CurrentBankMemory = BankMemory.USER;
                        _btWriteReadWrite.Enabled = true;
                        _btReadReadWrite.Enabled = true;
                        _cbUSERSize.Visible = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void _cbEPCSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbEPCSize.SelectedIndex != -1)
            {
                addressEPC = 0x04;
                switch (_cbEPCSize.SelectedItem.ToString())
                {
                    case "PC": // protocol control - refer EPC Global Class 1 Gen 2 UHF RFID 
                        addressEPC = 0x02;
                        nByteEPC = 0x02;

                        break;
                    case "96 bits":
                        addressEPC = 0x04;
                        nByteEPC = 0x0c; // 96bits, 12bytes

                        break;
                    case "240 bits":
                        addressEPC = 0x04;
                        nByteEPC = 0x1E; // 240bits, 30bytes

                        break;
                    case "512 bits":
                        addressEPC = 0x04;
                        nByteEPC = 0x40; // 512bits, 64bytes

                        break;
                    default:
                        break;
                }
            }
        }

        private void _cbTIDSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbTIDSize.SelectedIndex != -1)
            {
                addressTID = 0x00;
                switch (_cbTIDSize.SelectedItem.ToString())
                {
                    case "32 bits":
                        addressTID = 0x00;
                        nByteTID = 0x04; // 32bits, 4bytes

                        break;
                    case "64 bits":
                        addressTID = 0x00;
                        nByteTID = 0x08; // 64bits, 8bytes

                        break;
                    default:
                        break;
                }
            }
        }


        private void _cbUSERSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbUSERSize.SelectedIndex != -1)
            {
                addressUSER = 0x00;
                switch (_cbUSERSize.SelectedItem.ToString())
                {
                    case "96 bits":
                        addressUSER = 0x00;
                        nByteUSER = 0x0c; // 96bits, 12bytes

                        break;
                    case "512 bits":
                        addressUSER = 0x00;
                        nByteUSER = 0x40; // 512bits, 64bytes

                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        #region 'ReadID' Panel
        private void _btReadReadID_Click(object sender, EventArgs e)
        {
            if (opReadIDOptions.ContinuousRead)
            {
                if (_btReadReadID.Text == "Read")
                {
                    try
                    {
                        if (bAlternate)
                        {
                            if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2)
                                m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3);
                            else
                                if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3)
                                    m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2);
                            if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2 ||
                                m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3)
                                if (bReset)
                                    m_Source0.ResetSession_EPC_C1G2();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    ClearReadID();
                    _btConfigurationPanel.Enabled = false;
                    _btReadWritePanel.Enabled = false;
                    _btClearReadID.Enabled = true;

                    _timer.Enabled = true;
                    _btReadReadID.Text = "Stop";

                    if (bActiveChrono)
                        StartTime = Environment.TickCount;
                }
                else
                {
                    _btConfigurationPanel.Enabled = true;
                    _btReadWritePanel.Enabled = true;
                    _btClearReadID.Enabled = true;

                    _timer.Enabled = false;
                    _btReadReadID.Text = "Read";
                }
            }
            else
            {
                try
                {
                    if (bAlternate)
                    {
                        if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2)
                            m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3);
                        else
                            if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3)
                                m_Source0.SetSession_EPC_C1G2(CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2);
                        if (m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S2 ||
                            m_Source0.GetSession_EPC_C1G2() == CAENRFIDLogicalSourceConstants.EPC_C1G2_SESSION_S3)
                            if (bReset)
                                m_Source0.ResetSession_EPC_C1G2();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ReadTagID();
                DisplayTagID();
            }
        }


        private void _btClearReadID_Click(object sender, EventArgs e)
        {
            ClearReadID();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            ReadTagID();
            DisplayTagID();
        }

        private bool ReadTagID()
        {
            try
            {

                int s = Environment.TickCount;

                ArrayList Inv = Inventory();

                int u = Environment.TickCount - s;
                System.Diagnostics.Trace.Assert(true, string.Format("{0:000}", u));

                foreach (string IDTag in Inv)
                {
                    _lbInfoReadID.Text = "Found"; _lbInfoReadID.Refresh();
                    bool boo = false;
                    foreach (ArrayList ListTemp in ListTags)
                    {
                        //  If IDTag exists in the list of IDTags
                        if (((string)ListTemp[0] == IDTag))
                        {
                            ListTemp[1] = ((int)ListTemp[1] + 1);
                            boo = true;
                            if (opReadIDOptions.BeepEachDetection)
                            {
                                try { Sound.Play(); }
                                catch { }
                            }
                            break;
                        }
                    }
                    //  If IDTag doesn't exist
                    if (!boo)
                    {
                        ArrayList DetailTag = new ArrayList();
                        DetailTag.Add(IDTag);
                        DetailTag.Add(1);
                        ListTags.Add(DetailTag);

                        if (opReadIDOptions.BeepEachDifferentTag)
                        {
                            try { Sound.Play(); }
                            catch { }
                        }
                    }

                    _lbInfoReadID.Text = "";
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        private void DisplayTagID()
        {
            int cptChipsDiff = 0;
            int CptChips = 0;
            int cpt;
            bool boo;

            foreach (ArrayList ListTemp in ListTags)
            {

                ListViewItem lvi = new ListViewItem(new string[] {
								 ListTemp[0].ToString(),
								 ListTemp[1].ToString()});

                if ((this._lvReadID.Items.Count == 0))
                {
                    this._lvReadID.Items.Add(lvi);
                    CptChips = 1;
                    cptChipsDiff = 1;
                }
                else
                {
                    boo = false;
                    for (cpt = 0; cpt < this._lvReadID.Items.Count; cpt++)
                    {
                        // If exists in the list of tags
                        if ((this._lvReadID.Items[cpt].SubItems[0].Text == (string)ListTemp[0]))
                        {
                            boo = true;
                            if ((this._lvReadID.Items[cpt].SubItems[1].Text != Convert.ToString(ListTemp[1])))
                            {
                                this._lvReadID.Items[cpt].SubItems[1].Text = Convert.ToString(ListTemp[1]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    // If no exists. Add tag.
                    if (!boo)
                    {
                        this._lvReadID.Items.Add(lvi);
                    }

                    CptChips += (int)ListTemp[1];
                    cptChipsDiff++;
                }
            }

            this._lbSumReadsReadID.Text = CptChips.ToString();
            this._lbSumTagsReadID.Text = cptChipsDiff.ToString();

            if (bActiveChrono)
                if (cptChipsDiff >= iNbTagsToRead)
                {
                    int Time = Environment.TickCount - StartTime;

                    if (_btReadReadID.Text == "Stop")
                        _btReadReadID_Click(null, new EventArgs());

                    _lblChrono.Text = "Chrono: " + Time + " ms";
                }

        }

        private void ClearReadID()
        {
            ListTags.Clear();
            _lvReadID.Items.Clear();
            _lbInfoReadID.Text = "";
            _lbSumReadsReadID.Text = "0";
            _lbSumTagsReadID.Text = "0";

            if (bActiveChrono)
                StartTime = Environment.TickCount;

            _lblChrono.Text = "";
        }
        #endregion
        #region 'Read/Write' Panel
        private void _btClearReadWrite_Click(object sender, EventArgs e)
        {
            try
            {
                btndisable();
                ClearReadWrite();
                //txtRCN.Text = "";
                txtCommodity.Text = "";
                txtDestination.Text = "";
                txtSource.Text = "";
                txtTaskDtl.Text = "";
                lblOperation.Text = "";
                txtTruckNo.Text = "";
                localdbrecordcount();
                btnenable();
                _plReadWrite.BackColor = System.Drawing.SystemColors.Highlight;
                btnSave.Visible = false;
                btnLoadorUnLoad.Visible = false;
                btnVerification.Visible = false;
                _btReadReadWrite.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #region Tag Read....
        private void _btReadReadWrite_Click(object sender, EventArgs e)
        {
            //dbCon = new SqlCeConnection(localConnection);
            //dbCon.Open();
            btnSave.Visible = false;
            _plReadWrite.BackColor = System.Drawing.SystemColors.Highlight;
            btndisable();
            txtTruckNo.Text = "";
            _txtReadReadWrite.Text = "";
            txtCommodity.Text = "";
            txtTaskDtl.Text = "";
            txtSource.Text = "";
            txtDestination.Text = "";
            TaskCode = "";
            SubTaskCode = "";
            ReaderOperation = "";
            LocationId = "";
            //_btReadReadWrite.Enabled = false;
            try
            {
                _lbReadInfoReadWrite.Text = "Reading...";
                _lbReadInfoReadWrite.Refresh();
                _txtReadReadWrite.Text = "";

                ArrayList Inv = Inventory();

                if (Inv.Count > 1)
                    throw new Exception("Too many tags in the field");

                byte[] read = null;

                switch (Reader.GetProtocol())
                {
                    case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                        switch (CurrentBankMemory)
                        {
                            case BankMemory.Reserved:
                                read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x00, (short)0, (short)8);
                                break;
                            case BankMemory.EPC:
                                read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x01, (short)addressEPC, (short)nByteEPC);
                                break;
                            case BankMemory.TID:
                                read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x02, (short)addressTID, (short)nByteTID);
                                break;
                            case BankMemory.USER:
                                read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x03, (short)addressUSER, (short)nByteUSER);
                                break;
                            default:
                                break;
                        }
                        break;
                    case CAENRFIDProtocol.CAENRFID_ISO18000_6b:
                        read = m_Source0.ReadTagData(m_RFIDTags[0], (short)addressEPC, (short)nByteEPC); // Address: 08, nByte: 16
                        break;
                }

                if (opReadWriteOptions.ASCIIDisplay)
                {
                    _txtReadReadWrite.Text = "";
                    for (int i = 0; i < read.Length; i++)
                        _txtReadReadWrite.Text += Convert.ToChar(read[i]);
                }
                if (opReadWriteOptions.HexadecimalDisplay)
                {
                    _txtReadReadWrite.Text = System.BitConverter.ToString(read).Replace("-", "").Substring(0, 5);
                    if (_txtReadReadWrite.Text == "")
                    {
                        _lbReadInfoReadWrite.Text = "Tagdetails Not found";
                    }
                    else
                    {
                        _lbReadInfoReadWrite.Text = "Reading succeed";
                        txtTruckNo.ReadOnly = false;
                        
                            btnVerify_Click();
                        
                    }
                }

                //_lbReadInfoReadWrite.Text = "Reading succeed";
            }
            catch (Exception ex)
            {
                _lbReadInfoReadWrite.Text = ex.Message.Replace("RFID Error: ", "");
            }

            _btReadReadWrite.Enabled = true;
            _btReadReadWrite.Focus();
            btnenable();
            //dbCon.Close();
            _btReadReadWrite.Focus();
        }
        #endregion
        #region Validate the Tag details with existing Task-Truck details and Save in Local DB
        string Tkid;
        string TaskCode;
        string SubTaskCode;
        string LocationId;
        string ReaderOperation;
        string ReaderId;
        private void btnVerify_Click() //Pulling the details from local db with tag no....
        {
            //classLog.writeLog("Message @:Details Save Started.");
            
                string tagno = _txtReadReadWrite.Text.ToString();
                if (tagno.ToString() == "")
                {
                    _lbReadInfoReadWrite.Text = "Tag details not found.";
                    _btReadReadWrite.Focus();
                }
                else
                {

                    string datet = System.DateTime.Now.AddMinutes(-+TruckTurnAroundTimeDiff).ToString("yyy-MM-dd HH:mm:ss ");//five minits
                    //string datet = System.DateTime.Now.AddMinutes(-0.1).ToString("yyy-MM-dd HH:mm:ss ");//..1sec
                    string ndt = "{ts '" + datet + "'}";
                    
                    string sql = "select count(*) from InternalFleet  where ReadTime>" + ndt + " and TagNo='" + _txtReadReadWrite.Text + "'";
                    SqlCeCommand cm = new SqlCeCommand(sql, dbCon);
                    int flg;
                    flg = int.Parse(cm.ExecuteScalar().ToString());
                    cm.Dispose();
                    
                    if (flg > 0)
                    {
                        _lbReadInfoReadWrite.Text = "Already Saved.";
                        _txtReadReadWrite.Text = "";
                    }
                    else if(flg == 0)
                    {
                        string tim = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss ");
                        string ntim = "{ts '" + tim + "'}";
                        //writeNewText(tagno + "," + ntim);
                        try
                        {
                            
                            string cmd = "select [TruckNo],[Tkid],[TaskCode],[SubTaskCode] from [TruckMaster] where [TagNo]='" + tagno + "'";
                            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            txtTruckNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                            Tkid = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                            txtTaskDtl.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString() + "-" + ds.Tables[0].Rows[0].ItemArray[3].ToString();
                            txtTruckNo.ReadOnly = true;
                            TaskCode = ds.Tables[0].Rows[0].ItemArray[2].ToString(); if (TaskCode == "") TaskCode = "0";
                            SubTaskCode = ds.Tables[0].Rows[0].ItemArray[3].ToString(); if (SubTaskCode == "") SubTaskCode = "0";
                           
                            string cmd1="";
                            if (TaskCode != "0" && SubTaskCode == "0")
                                cmd1 = "select [TaskCode],[SubTaskCode],[RefCode],[SubRefCode],[Commodity],[Source],[Destination],[LocationId],[ReaderOperation],[ReaderId] from TaskConfig where [TaskCode]='" + TaskCode + "'";
                            else if (TaskCode == "0" && SubTaskCode == "0")
                                cmd1 = "";
                            else if (TaskCode != "0" && SubTaskCode != "0")
                                cmd1 = "select [TaskCode],[SubTaskCode],[RefCode],[SubRefCode],[Commodity],[Source],[Destination],[LocationId],[ReaderOperation],[ReaderId] from TaskConfig where [TaskCode]='" + TaskCode + "' and [SubTaskCode]='" + SubTaskCode + "'";
                            if (cmd1 == "")
                            {
                                _lbReadInfoReadWrite.Text = "No Task for This truck.";
                                _plReadWrite.BackColor = Color.Red;
                                btnSave.Enabled = false;
                                btnSave.Visible = false;
                                
                                _btReadReadWrite.Focus();
                            }
                            else
                            {
                                SqlCeDataAdapter da1 = new SqlCeDataAdapter(cmd1, dbCon);
                                DataSet ds1 = new DataSet();
                                da1.Fill(ds1);
                                txtCommodity.Text = ds1.Tables[0].Rows[0].ItemArray[4].ToString();
                                txtSource.Text = ds1.Tables[0].Rows[0].ItemArray[5].ToString();
                                txtDestination.Text = ds1.Tables[0].Rows[0].ItemArray[6].ToString();
                                LocationId = ds1.Tables[0].Rows[0].ItemArray[7].ToString(); if (LocationId == "") LocationId = "0";
                                ReaderOperation = ds1.Tables[0].Rows[0].ItemArray[8].ToString();
                                
                                lblOperation.Text = ReaderOperation.ToString();
                               
                                da.Dispose();
                                ds.Dispose();
                                da1.Dispose();
                                ds1.Dispose();

                                _plReadWrite.BackColor = Color.Green;
                            }
                            
                                insertdatatoLocalDB();
                                localdbrecordcount();
                            
                        }
                        catch (Exception ex)
                        {
                            if (txtTruckNo.Text == "")
                            {
                                btnLoadorUnLoad.Visible = false;
                                btnVerification.Visible = false;
                                _plReadWrite.BackColor = Color.Red;
                                _lbReadInfoReadWrite.Text = "Enter Truck No.";
                                btnSave.Enabled = true;
                                btnSave.Visible = true;
                                txtTruckNo.Focus();
                                
                            }
                            else
                            {
                                _lbReadInfoReadWrite.Text = "No Task for This truck.";
                                _plReadWrite.BackColor = Color.Red;
                                btnSave.Enabled = false;
                                btnSave.Visible = false;
                                btnLoadorUnLoad.Visible = false;
                                btnVerification.Visible = false;
                                insertdatatoLocalDB();
                                localdbrecordcount();
                                _btReadReadWrite.Focus();
                            }
                            if (ReaderId == "" || ReaderId == "0")
                                getReaderDetails();
                            txtCommodity.Text = "";
                            txtTaskDtl.Text = "";
                            txtSource.Text = "";
                            txtDestination.Text = "";
                            lblOperation.Text = "";
                            TaskCode = "0";
                            SubTaskCode = "0";
                            LocationId = "0";
                            ReaderOperation = "";

                        }

                      
                    }
                }

        }
        #endregion

        private void _btWriteReadWrite_Click(object sender, EventArgs e)
        {
            try
            {
                _lbWriteInfoReadWrite.Text = "Writing...";
                _lbWriteInfoReadWrite.Refresh();

                string string_to_send = _txtWriteReadWrite.Text.Trim();

                int currentAddress = 0;
                int nByte = 0;

                if (CurrentBankMemory == BankMemory.EPC)
                {
                    nByte = nByteEPC;
                }
                if (CurrentBankMemory == BankMemory.USER)
                {
                    nByte = nByteUSER;
                }

                // Define all the EPC size (12 bytes by default)
                byte[] byte_to_send = new byte[nByte];

                //Convert string in array of bytes
                if (opReadWriteOptions.ASCIIDisplay)
                {
                    for (int i = 0; i < string_to_send.Length; i++)
                        byte_to_send[i] = Convert.ToByte(string_to_send[i]);
                }
                if (opReadWriteOptions.HexadecimalDisplay)
                {
                    if (string_to_send.Length % 2 != 0)
                        string_to_send += "0";

                    for (int i = 0; i < string_to_send.Length; i += 2)
                        byte_to_send[i / 2] = Byte.Parse(string_to_send.Substring(i, 2),
                            System.Globalization.NumberStyles.HexNumber);
                }

                byte[] read = null;


                // Try 5 times to write data
                for (int cpt = 0; cpt < 5; cpt++)
                {
                    try
                    {
                        if (CurrentBankMemory == BankMemory.EPC)
                        {
                            currentAddress = addressEPC;
                            nByte = nByteEPC;
                        }
                        if (CurrentBankMemory == BankMemory.USER)
                        {
                            currentAddress = addressUSER;
                            nByte = nByteUSER;
                        }
                        int currentNByte = 0;
                        int MaxBytesToBeWritten = 16;

                        for (int BytesWritten = 0; BytesWritten < nByte; BytesWritten += MaxBytesToBeWritten)
                        {
                            // check if necessary to cut the data to write
                            if (nByte - BytesWritten >= MaxBytesToBeWritten)
                                currentNByte = MaxBytesToBeWritten;
                            else
                                currentNByte = nByte - BytesWritten;

                            // copy array to send
                            byte[] partial_byte_to_send = new byte[currentNByte];
                            Array.Copy(byte_to_send, BytesWritten, partial_byte_to_send, 0, currentNByte);

                            ArrayList Inv = Inventory();

                            if (Inv.Count > 1)
                                throw new Exception("Too many tags in the field");

                            switch (Reader.GetProtocol())
                            {
                                case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                                    switch (CurrentBankMemory)
                                    {
                                        case BankMemory.Reserved:
                                            break;
                                        case BankMemory.EPC:

                                            if (PasswordUsed)
                                            {
                                                int password = int.Parse(_txtPassword.Text, System.Globalization.NumberStyles.HexNumber);
                                                m_Source0.WriteTagData_EPC_C1G2(m_RFIDTags[0], 0x01, (short)currentAddress, (short)currentNByte, partial_byte_to_send, password);
                                            }
                                            else
                                                m_Source0.WriteTagData_EPC_C1G2(m_RFIDTags[0], 0x01, (short)currentAddress, (short)currentNByte, partial_byte_to_send);


                                            // Make another inventory to get the new EPC
                                            m_RFIDTags = m_Source0.InventoryTag();
                                            if (m_RFIDTags == null)
                                                throw new Exception("No tag in the field");
                                            if (m_RFIDTags.Length == 0)
                                                throw new Exception("No tag in the field");
                                            if (m_RFIDTags.Length > 1)
                                                throw new Exception("Too many tags in the field");

                                            // Read EPC to confirm the write success
                                            read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x01, (short)currentAddress, (short)currentNByte);
                                            // Compare data to write and data written
                                            if (!CompareByteArrays(partial_byte_to_send, read))
                                                throw new Exception("Error Writing");
                                            break;
                                        case BankMemory.TID:
                                            break;
                                        case BankMemory.USER:
                                            m_Source0.WriteTagData_EPC_C1G2(m_RFIDTags[0], 0x03, (short)currentAddress, (short)currentNByte, partial_byte_to_send);

                                            // Make another inventory to get the new USER memory
                                            m_RFIDTags = m_Source0.InventoryTag();
                                            if (m_RFIDTags == null)
                                                throw new Exception("No tag in the field");
                                            if (m_RFIDTags.Length == 0)
                                                throw new Exception("No tag in the field");
                                            if (m_RFIDTags.Length > 1)
                                                throw new Exception("Too many tags in the field");

                                            // Read USER Memory to confirm the write success
                                            read = m_Source0.ReadTagData_EPC_C1G2(m_RFIDTags[0], 0x03, (short)currentAddress, (short)currentNByte);
                                            // Compare data to write and data written
                                            if (!CompareByteArrays(partial_byte_to_send, read))
                                                throw new Exception("Error Writing");
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case CAENRFIDProtocol.CAENRFID_ISO18000_6b:
                                    m_Source0.WriteTagData(m_RFIDTags[0], (short)addressEPC, (short)(byte_to_send.Length), byte_to_send);
                                    break;
                            }
                            currentAddress += currentNByte;
                        }

                        _lbWriteInfoReadWrite.Text = "Writing succeed";
                        break;
                    }
                    catch (Exception ex)
                    {
                        _lbWriteInfoReadWrite.Text = ex.Message.Replace("RFID Error: ", "");
                        // Sleep 20 ms
                        System.Threading.Thread.Sleep(20);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                _lbWriteInfoReadWrite.Text = ex.Message.Replace("RFID Error: ", "");
            }
        }

        private bool CompareByteArrays(byte[] data1, byte[] data2)
        {
            // If both are null, they're equal
            if (data1 == null && data2 == null)
            {
                return true;
            }
            // If either but not both are null, they're not equal
            if (data1 == null || data2 == null)
            {
                return false;
            }
            if (data1.Length != data2.Length)
            {
                return false;
            }
            for (int i = 0; i < data1.Length; i++)
            {
                if (data1[i] != data2[i])
                {
                    return false;
                }
            }
            return true;
        }


        private void _ibCopy_Click(object sender, EventArgs e)
        {
            _txtWriteReadWrite.Text = _txtReadReadWrite.Text;
        }

        private void ClearReadWrite()
        {
            _lbReadInfoReadWrite.Text = "";
            _lbWriteInfoReadWrite.Text = "";
            _txtReadReadWrite.Text = "";
            _txtWriteReadWrite.Text = "";
        }
        #endregion
        #region Modules
        private void _btModules_Click(object sender, EventArgs e)
        {
            try
            {
                if (_lvModules.SelectedIndices.Count == 1)
                    if (Modules != null)
                    {
                        string key = _lvModules.Items[_lvModules.SelectedIndices[0]].SubItems[0].Text;
                        PTEMEAMS.Modules.CAENModule module = (PTEMEAMS.Modules.CAENModule)PTEMEAMS.Modules.ModuleManager.Instance.Get(
                             typeof(PTEMEAMS.Modules.CAENModule),
                             key);

                        module.Execute(Reader, m_Source0);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
        #endregion
        #region Lock functionalities (access password)
        private bool PasswordUsed = false;
        private void _cbUsePassword_CheckStateChanged(object sender, EventArgs e)
        {
            PasswordUsed = _cbUsePassword.Checked;
            _txtPassword.Enabled = _cbUsePassword.Checked;
        }

        private void _btChangeAccessPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (_lbLockTagsList.SelectedIndex == -1)
                    throw new Exception("Please select a tag");
                // check if the password length is equal to 8
                if (_txtAccessPassword.Text.Length != 8)
                    throw new Exception("Invalid password length");

                Display("changing lock password ...");
                // init the password with the data informed in the Textbox
                byte[] newPassword = new byte[_txtAccessPassword.Text.Length / 2];
                for (int i = 0; i < _txtAccessPassword.Text.Length; i += 2)
                    newPassword[i / 2] = Byte.Parse(_txtAccessPassword.Text.Substring(i, 2),
                        System.Globalization.NumberStyles.HexNumber);

                // perform an inventory to get the unique tag which the pawssword will be changed
                if (bOpen)
                {
                    string IDTag = string.Empty;
                    string tagSelected = _lbLockTagsList.SelectedItem.ToString();
                    CAENRFIDTag tagToChangePassword = null;

                    m_RFIDTags = m_Source0.InventoryTag();

                    if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    //check the index of the tag desired
                    foreach (CAENRFIDTag tag in m_RFIDTags)
                    {
                        IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                        if (IDTag == tagSelected)
                        {
                            tagToChangePassword = tag;
                            break;
                        }
                    }

                    if (tagToChangePassword == null)
                        throw new Exception("Tag selected not found");

                    // Write kill password
                    switch (Reader.GetProtocol())
                    {
                        case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                            m_Source0.WriteTagData_EPC_C1G2(
                                tagToChangePassword, 0x0, // RESERVED bank memory
                                4, 4, // beginning to the block 4 write the following 4 bytes with the password
                                newPassword);
                            break;
                        default:
                            throw new Exception("Not yep implemented with this protocol");
                    }
                }
                else
                    throw new Exception("Reader is not opened");

                Display("Access password changed", Images.Success);
            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }

        private void _btRW3Help_Click(object sender, EventArgs e)
        {
            Clear();
            Display("If 'pwd-write'=0 then", Images.Arrow);
            Display("Region is writeable with or without an Access Password.");

            Display("If 'pwd-write'=1 then", Images.Arrow);
            Display("Access Password required to write to region.");
        }

        private void _btLock_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Display("locking...");

                if (_lbLockTagsList.SelectedIndex == -1)
                    throw new Exception("Please select a tag");

                // check if the password length is equal to 8
                if (_txtAccessPassword.Text.Length != 8)
                    throw new Exception("Invalid password length");
                int password = int.Parse(_txtAccessPassword.Text,
                    System.Globalization.NumberStyles.HexNumber);

                // Refer to the EPC Global UHF C1 Gen2 documentation to know how calculate the payload
                // and complementary information

                // write pwd-write information for EPC memory
                int mask = 1 << 5;

                //If 'pwd-write'=0 then
                //Region is writeable with or without an Access Password
                //If 'pwd-write'=1 then
                //Access Password required to write to region
                int action = ((_cbEPCMemoryPwdWrite.Checked ? 1 : 0) << 5);

                // calculte payload (20 bits) (mask + action)
                int payload = (mask << 10) | action;
                Display("payload= " + payload.ToString());

                if (bOpen)
                {
                    string IDTag = string.Empty;
                    string tagSelected = _lbLockTagsList.SelectedItem.ToString();
                    CAENRFIDTag tagToLock = null;

                    m_RFIDTags = m_Source0.InventoryTag();

                    if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    //check the index of the tag desired
                    foreach (CAENRFIDTag tag in m_RFIDTags)
                    {
                        IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                        if (IDTag == tagSelected)
                        {
                            tagToLock = tag;
                            break;
                        }
                    }

                    if (tagToLock == null)
                        throw new Exception("Tag selected not found");

                    // Lock tag with payload value and access password 
                    switch (Reader.GetProtocol())
                    {
                        case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                            m_Source0.LockTag_EPC_C1G2(
                               tagToLock, payload, password);
                            break;
                        default:
                            throw new Exception("Not yep implemented with this protocol");
                    }
                }

                Display("Lock succeeded", Images.Success);

            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }


        private void _btLockRefreshList_Click(object sender, EventArgs e)
        {
            try
            {
                _lbLockTagsList.Items.Clear();
                string IDTag = string.Empty;
                m_RFIDTags = m_Source0.InventoryTag();
                if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                    throw new Exception("No tag in the field");
                foreach (CAENRFIDTag tag in m_RFIDTags)
                {
                    IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                    _lbLockTagsList.Items.Add(IDTag);
                }
            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }
        #endregion
        #region Kill functionalities (kill password)
        private void _btKillRefreshList_Click(object sender, EventArgs e)
        {
            try
            {
                _lbKillTagsList.Items.Clear();
                string IDTag = string.Empty;
                m_RFIDTags = m_Source0.InventoryTag();
                if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                    throw new Exception("No tag in the field");
                foreach (CAENRFIDTag tag in m_RFIDTags)
                {
                    IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                    _lbKillTagsList.Items.Add(IDTag);
                }
            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }


        private void _btChangeKillPassword_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (_lbKillTagsList.SelectedIndex == -1)
                    throw new Exception("Please select a tag");
                // check if the password length is equal to 8
                if (_txtKillPassword.Text.Length != 8)
                    throw new Exception("Invalid password length");

                Display("changing kill password ...");
                // init the password with the data informed in the Textbox
                byte[] newPassword = new byte[_txtKillPassword.Text.Length / 2];
                for (int i = 0; i < _txtKillPassword.Text.Length; i += 2)
                    newPassword[i / 2] = Byte.Parse(_txtKillPassword.Text.Substring(i, 2),
                        System.Globalization.NumberStyles.HexNumber);

                // perform an inventory to get the unique tag which the pawssword will be changed
                if (bOpen)
                {
                    string IDTag = string.Empty;
                    string tagSelected = _lbKillTagsList.SelectedItem.ToString();
                    CAENRFIDTag tagToChangePassword = null;

                    m_RFIDTags = m_Source0.InventoryTag();

                    if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    //check the index of the tag desired
                    foreach (CAENRFIDTag tag in m_RFIDTags)
                    {
                        IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                        if (IDTag == tagSelected)
                        {
                            tagToChangePassword = tag;
                            break;
                        }
                    }

                    if (tagToChangePassword == null)
                        throw new Exception("Tag selected not found");

                    // Write kill password
                    switch (Reader.GetProtocol())
                    {
                        case CAENRFIDProtocol.CAENRFID_EPC_C1G2:
                            m_Source0.WriteTagData_EPC_C1G2(
                                tagToChangePassword, 0x0, // RESERVED bank memory
                                0, 4, // beginning to the block 0 write the following 4 bytes with the password
                                newPassword);
                            break;
                        default:
                            throw new Exception("Not yep implemented with this protocol");
                    }
                }
                else
                    throw new Exception("Reader is not opened");

                Display("Kill password changed", Images.Success);
            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }

        private void _btKillTag_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (_lbKillTagsList.SelectedIndex == -1)
                    throw new Exception("Please select a tag");

                if (_txtKillPassword.Text.Length != 8)
                    throw new Exception("Invalid password length");

                int password = int.Parse(_txtKillPassword.Text,
                    System.Globalization.NumberStyles.HexNumber);

                if (password == 0) // refering to the EPC C1 G2 Air Protocol
                    throw new Exception("Password has to be different to 0");

                if (bOpen)
                {
                    string IDTag = string.Empty;
                    string tagSelected = _lbKillTagsList.SelectedItem.ToString();
                    CAENRFIDTag tagToKill = null;

                    m_RFIDTags = m_Source0.InventoryTag();

                    if (m_RFIDTags == null || m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    //check the index of the tag desired
                    foreach (CAENRFIDTag tag in m_RFIDTags)
                    {
                        IDTag = System.BitConverter.ToString(tag.GetId()).Replace("-", "");
                        if (IDTag == tagSelected)
                        {
                            tagToKill = tag;
                            break;
                        }
                    }

                    if (tagToKill == null)
                        throw new Exception("Tag selected not found");

                    // kill the selected tag (password has to be different to 0)
                    m_Source0.KillTag_EPC_C1G2(tagToKill, password);
                }
                else
                    throw new Exception("Reader is not opened");

                Display("Tag killed", Images.Success);
            }
            catch (Exception ex)
            {
                Display(ex.Message.Replace("RFID Error: ", ""), Images.Fail);
            }
        }

        #endregion
        #region Application Exit-Logout
        private void btnLogout_Click(object sender, EventArgs e)
        {
            btndisable();
            System.Threading.Thread.Sleep(9000);
            Process.GetCurrentProcess().Kill();
            Application.Exit();
            classLog.writeLog("Message @:Internal Form Exit");
        } //Exit....

        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btndisable();
                if (_txtReadReadWrite.Text.ToString() == "")
                {
                    _lbReadInfoReadWrite.Text = "Tag details not found...";
                }
                else if (txtTruckNo.Text.Trim().ToString() == "")
                {
                    _lbReadInfoReadWrite.Text = "Enter Truck No.";
                }
                else
                {
                    _lbReadInfoReadWrite.Text = "Saveing the Record...";
                    insertdatatoLocalDB();
                    localdbrecordcount();
                    txtTruckNo.Text = "";
                    _txtReadReadWrite.Text = "";
                    txtCommodity.Text = "";
                    txtTaskDtl.Text = "";
                    txtSource.Text = "";
                    txtDestination.Text = "";
                    TaskCode = "";
                    SubTaskCode = "";
                    ReaderOperation = "";
                    LocationId = "";
                    btnSave.Visible = false;
                    _btReadReadWrite.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            txtTruckNo.ReadOnly = false;
            btnenable();
        }
        public void btnenable()
        {
            btnLogout.Enabled = true;
            btnSync.Enabled = true;
            _btReadReadWrite.Enabled = true;
            _btClearReadWrite.Enabled = true;
            btnSave.Enabled = true;
        }
        public void btndisable()
        {
            btnLogout.Enabled = false;
            btnSync.Enabled = false;
            _btReadReadWrite.Enabled = false;
            _btClearReadWrite.Enabled = false;
            btnSave.Enabled = false;
        }
        int Rid;
        int SyncRecordCount;
        public void syncdatafromLocalDBtoSQLDB()//Getting the details from localdb (sdf) and saveing them in to SQL DB
        {
            string sql = "select count(*) from InternalFleet  where Status='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, dbCon);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            txtCount.Text = "Sync Progress: " +flg.ToString();
            //classLog.DataSyncLog("Sync Progress: " + flg.ToString(), fname);
            if (flg == 0)
            {
                MessageBox.Show("No data in the localdb to sync.");
                //classLog.DataSyncLog("No data in the localdb to sync.", fname);
            }
            else
            {
                SyncRecordCount = flg;
                DataSyncLogInfo();
                //classLog.DataSyncLog("[TagNo],[TruckNo],[TaskCode],[SubTaskCode],[Location],[ReaderNo],[ReaderIP],[TStatus],[ReadTime],[CreatedBy],[CreatedDate],[ReaderOperation]", fname);
                int i;
                for (i = 0; i < flg; i++)
                {
                    txtCount.Text = "Sync Progress: " + (flg - i).ToString();
                    string c = "Select min(Id) from InternalFleet where Status='Active'";
                    SqlCeCommand c1 = new SqlCeCommand(c, dbCon);
                    Rid = int.Parse(c1.ExecuteScalar().ToString());
                    c1.Dispose();
                    string cmd1 = "select Id,TagNo,TruckNo,TaskCode,SubTaskCode,Location,ReaderNo,ReaderIP,Status,ReadTime,CreatedBy,ReaderOperation from InternalFleet where  Id=" + Rid + "";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd1, dbCon);
                    DataSet ds = new DataSet();

                    try
                    {
                        da.Fill(ds);
                        string id = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string tagno = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        //string tagrigid = getregidvalue(tagno);
                        string truckno = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        string taskcode = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                        if (taskcode == "") taskcode = "0";
                        string subtaskcode = ds.Tables[0].Rows[0].ItemArray[4].ToString(); if (subtaskcode == "") subtaskcode = "0";
                        string Location = ds.Tables[0].Rows[0].ItemArray[5].ToString(); if (Location == "") Location = "0";
                        string readtime = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        DateTime dt = Convert.ToDateTime(readtime);
                        string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                        string ndt = "{ts '" + cdt + "'}";
                        string readerno = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                        string readerip = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                        string status = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                        string createdby = ds.Tables[0].Rows[0].ItemArray[10].ToString();
                        string readeroperation = ds.Tables[0].Rows[0].ItemArray[11].ToString();
                        string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                        string nodt = "{ts '" + datet + "'}";
                        string TableName = "InternalFleet_Data";
                       
                        signal();
                        if (strength < -10 && strength > -95)
                        {
                            string CONN_STRING = sqlConnection;
                            SqlConnection dbCon1 = new SqlConnection(CONN_STRING);
                            try
                            {
                                dbCon1.Open();
                                string Query = "Insert into [dbo].[" + TableName + "] ([TagNo],[TruckNo],[TaskCode],[SubTaskCode],[Location],[ReaderNo],[ReaderIP],[TStatus],[ReadTime],[CreatedBy],[CreatedDate],[ReaderOperation],[Synctime]) Values('" + tagno + "','" + truckno + "'," + taskcode + "," + subtaskcode + "," + Location + "," + readerno + ",'" + readerip + "','" + status + "'," + ndt + ",'" + createdby + "'," + ndt + ",'" + readeroperation + "',GETDATE())";
                                SqlCommand cm = new SqlCommand(Query, dbCon1);
                                cm.ExecuteNonQuery();
                                dbCon1.Close();
                                clearlocaldb();
                                da.Dispose();
                                ds.Dispose();
                                //classLog.writeLog(Query.ToString());
                                //classLog.DataSyncLog( tagno+  "," + truckno + "," + taskcode + "," + subtaskcode + "," + Location + "," + readerno + "," + readerip + "," + status + "," + ndt + "," + createdby + "," + ndt + "," + readeroperation , fname);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Database Disconnected."); i = 100000;
                                //classLog.DataSyncLog("Database Disconnected", fname);
                                //classLog.writeLog("Error @:"+ex.ToString());
                                classLog.writeLog("Error @:SQL Server Connectivity disconnected");
                                
                            }
                            count--;
                        }
                        else
                        {
                            _lbReadInfoReadWrite.Text = "Wifi Disconnected.";
                            //classLog.DataSyncLog("Wifi Disconnected", fname);
                            i = 100000;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Details not found in the Database.");
                    }
                }
                if (i == flg)
                {
                    //clearlocaldball();
                    
                    System.Threading.Thread.Sleep(5000); 
                    _lbReadInfoReadWrite.Text = "Sync Complete";
                    classLog.writeLog("Message @:Sync Complete.");
                    DataSyncLogCompleteInfo();
                    DataSyncTime_TaskSync();
                    //classLog.DataSyncLog("Sync Complete", fname);
                }
                else
                    _lbReadInfoReadWrite.Text = "DB Connection Failed.";
            }
        }
        public void clearlocaldb()
        {
            //string Query = "DELETE FROM InternalFleet where Id=" + Rid + "";
            string Query = "Update InternalFleet set Status='InActive' where Id=" + Rid + "";
            SqlCeCommand cm = new SqlCeCommand(Query, dbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            _btReadReadWrite.Focus();
        }
        public void clearlocaldball()
        {
            //string Query = "DELETE FROM InternalFleet where Id=" + Rid + "";
            string Query = "Update InternalFleet set Status='InActive'";
            SqlCeCommand cm = new SqlCeCommand(Query, dbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            _btReadReadWrite.Focus();
        }
        public void localdbrecordcount()//To get the initial record count from localdb and displaying in the application...
        {
            string sql = "select count(*) from InternalFleet  where status='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, dbCon);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            
            if (flg == 0)
            {
                lblLocalDBstatus.Text = "No data in the localdb.";
            }
            else
            {
                count = flg;
                lblLocalDBstatus.Text = "Total Records in the LocalDB :" + count.ToString();
            }
            cmd.Dispose();
        }

        //string fname;
        private void btnSync_Click(object sender, EventArgs e)
        {
            btnLoadorUnLoad.Visible = false;
            btnVerification.Visible = false;
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            //fname = "DataSyncLog" + datet.Replace("-", "").Replace(":", "") + ".txt";
            //classLog.DataSyncLog("Data Sync Started..." + lblLocalDBstatus.Text, fname);

            //classLog.writeLog(lblLocalDBstatus.Text);
            signal();
            if (strength < -10 && strength > -95)
            {
                txtCount.Visible = true;
                btndisable();
                string CONN_STRING = sqlConnection;
                SqlConnection dbCon1= new SqlConnection(CONN_STRING);
                try
                {
                    dbCon1.Open();
                    dbCon1.Close();
                    syncdatafromLocalDBtoSQLDB();
                    localdbrecordcount();
                }
                catch
                {
                    _lbReadInfoReadWrite.Text = "Server not  connected.";
                    txtCount.Visible = false;
                    classLog.writeLog("Error @:Internal DB Server Connectivity problem");
                    //classLog.DataSyncLog("Server not connected", fname);
                }
                btnenable();
                txtCount.Visible = false;
            }
            else
            {
                _lbReadInfoReadWrite.Text = "Wifi signal is Week.";
                txtCount.Visible = false;
                classLog.writeLog("Error @:Internal Wifi Signal Problem");
                //classLog.DataSyncLog("Wifi signal is Week", fname);
            }
        }
       
        public void insertdatatoLocalDB() //inserting data into localdb
        {
                //Save Record in Handle sqlce database (sdf)
                string connectionString = localConnection;
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();
                    string tagno = _txtReadReadWrite.Text.ToString();

                    if (tagno.ToString() == "")
                    {
                        _lbReadInfoReadWrite.Text = "Tag details not found.";
                    }
                    else
                    {
                        try
                        {
                            if (ReaderId == "")
                            {
                                getReaderDetails();
                            }
                            if (LocationId == "") LocationId = "0";
                            string truckno = truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
                            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                            string ndt = "{ts '" + datet + "'}";
                            //if (GPRS == "Y")
                            //{
                            //    GPRS = "N";
                            //    InternetAvailable();
                            //}
                            //else
                            //{
                                if (ReaderOperation == "Load" || ReaderOperation == "UnLoad" || ReaderOperation == "Verification" || ReaderOperation == "")
                                {

                                    SqlCeCommand cmd1 = new SqlCeCommand("Insert into InternalFleet(TagNo,TruckNo,TaskCode,SubTaskCode,Location,ReaderNo,ReaderIP,Status,CreatedBy,ReadTime,ReaderOperation)" + "Values('" + tagno + "','" + truckno + "','" + TaskCode + "','" + SubTaskCode + "','" + LocationId + "','" + ReaderId + "','" + readerip + "','Active','" + User + "'," + ndt + ",'" + ReaderOperation + "')", connection);
                                    cmd1.ExecuteNonQuery();
                                    connection.Close();
                                    _lbReadInfoReadWrite.Text = "Saved..in Offline.";
                                    classLog.writeLog("Message @:Details Saved In Offline.");
                                    //MessageBox.Show("Saved..in Offline.");
                                    InternetAvailable(tagno, ndt, ReaderId);

                                }
                                else if (ReaderOperation == "Load&Verification" || ReaderOperation == "UnLoad&Verification")
                                {
                                    if (ReaderOperation == "Load&Verification")
                                    {
                                        btnLoadorUnLoad.Text = "Load";
                                        btnLoadorUnLoad.Visible = true;
                                    }
                                    else if (ReaderOperation == "UnLoad&Verification")
                                    {
                                        btnLoadorUnLoad.Text = "UnLoad";
                                        btnLoadorUnLoad.Visible = true;
                                    }
                                    btnVerification.Text = "Verify";
                                    btnVerification.Visible = true;
                                    btnLoadorUnLoad.Enabled = true;
                                    btnVerification.Enabled = true;
                                }
                            //}
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            //_lbReadInfoReadWrite.Text = "Internet Connection Failed.";
                            //classLog.writeLog("Error @:Internet Connection Failed.");
                            _lbReadInfoReadWrite.Text = "Not able to Save Contact Admin.";
                            //MessageBox.Show("");
                            classLog.writeLog("Error @:Internal-Save faild");
                           
                        }

                    }
                }
                localdbrecordcount();
            
        }
      
        public void insertdatatoSQLDB()
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            string truckno = truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string ndt = "{ts '" + datet + "'}";
            string TableName = "";
            if (ReaderOperation == "Load")
            {
                TableName = "InternalFleet_Load";
            }
            else if (ReaderOperation == "UnLoad")
            {
                TableName = "InternalFleet_UnLoad";
            }
            else if (ReaderOperation == "Verification")
            {
                TableName = "InternalFleet_Verification";
            }
            else
            {
                TableName = "InternalFleet_Dust_GPRS";
            }
            if (ReaderId == "")
            {
                try
                {
                    getReaderDetails();
                }
                catch
                {
                    ReaderId = "0";
                }
            }
            string Query = "Insert into [dbo].[" + TableName + "] ([TagNo],[TruckNo],[TaskCode],[SubTaskCode],[Location],[ReaderNo],[ReaderIP],[TStatus],[ReadTime],[CreatedBy],[CreatedDate],[ReaderOperation],[Synctime]) Values('" + _txtReadReadWrite.Text + "','" + txtTruckNo.Text + "'," + TaskCode + "," + SubTaskCode + "," + LocationId + "," + ReaderId + ",'" + readerip + "','Active'," + ndt + ",'" + User + "'," + ndt + ",'" + ReaderOperation + "',GETDATE())";
            SqlCommand cmd = new SqlCommand(Query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            _lbReadInfoReadWrite.Text = "Saved..in Online.";
            txtTruckNo.Text = "";
            _txtReadReadWrite.Text = "";
            txtCommodity.Text = "";
            txtTaskDtl.Text = "";
            txtSource.Text = "";
            txtDestination.Text = "";
            TaskCode = "";
            SubTaskCode = "";
            ReaderOperation = "";
            LocationId = "";
            lblOperation.Text = "";
        }
        string sts;
        private void menuItem2_Click(object sender, EventArgs e)
        {
            btnLoadorUnLoad.Visible = false;
            btnVerification.Visible = false;
            btndisable();
            txtCount.Visible = true;
            bool hasNet = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://www.google.co.in");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                request.Timeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        hasNet = true;
                        syncdatafromLocalDBtoSQLDBuseingGPRS();
                    }
                    else
                    {
                        _lbReadInfoReadWrite.Text = "GPRS Connection Failed.";
                        classLog.writeLog("Error @:GPRS Not Connected..");
                    }
                }
            }
            catch
            {
                _lbReadInfoReadWrite.Text = "Server not connected.";
                classLog.writeLog("Error @:Sync faild due to lack of Connectivity");
                txtCount.Visible = false;
            }
            
            btnenable();
            txtCount.Visible = false;
            btnenable();
        }
        public void syncdatafromLocalDBtoSQLDBuseingGPRS()//Getting the details from localdb (sdf) and saveing them in to SQL DB Useing GPRS
        {
            string sql = "select count(*) from InternalFleet  where Status='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, dbCon);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            
            txtCount.Text = "Sync Progress: " + flg.ToString();
            if (flg == 0)
            {
                MessageBox.Show("No data in the localdb to sync.");
            }
            else
            {
                for (int i = 0; i < flg; i++)
                {
                    txtCount.Text = "Sync Progress: " + (flg - i).ToString();
              
                    string c = "Select min(Id) from InternalFleet where Status='Active' ";
                    SqlCeCommand c1 = new SqlCeCommand(c, dbCon);
                    Rid = int.Parse(c1.ExecuteScalar().ToString());
                    string cmd1 = "select Id,TagNo,ReaderNo,ReadTime from InternalFleet where  Id=" + Rid + "";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd1, dbCon);
                    DataSet ds = new DataSet();
                    //try
                    //{
                        da.Fill(ds);
                        string id = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string tagno = ds.Tables[0].Rows[0].ItemArray[1].ToString(); 
                        string readtime = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                        DateTime dt = Convert.ToDateTime(readtime);
                        string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                        string ndt = "{ts '" + cdt + "'}";
                        string readerno = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        InternetAvailableOffline(tagno, ndt, readerno);
                        if(sts == "Y") clearlocaldb();
                            count--;
                            if (GPRSSyncSts == "Fail") i = flg;
                    //}
                    //catch
                    //{
                    //    MessageBox.Show("Details not found in the Database.");
                    //}
                    //cn.Close();
                }
                if (GPRSSyncSts == "Fail") _lbReadInfoReadWrite.Text = "Sync Failed";
                _lbReadInfoReadWrite.Text = "Sync Complete"; classLog.writeLog("Message @:Sync Complete.");
                 localdbrecordcount();
            }
        }
      
        #region Enter Kay handling
        private void formInternal_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                this._btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this._btReadReadWrite.PerformClick();
            //}
        }

       

        private void _btReadReadWrite_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                
                _btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
        }
        

        private void _txtReadReadWrite_KeyDown(object sender, KeyEventArgs e)
        {
            string code=e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                this._btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
            
        }

        private void txtTruckNo_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                this._btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
        }

        private void txtCount_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                this._btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
        }
       

        private void _btClearReadWrite_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter) || code == "239")
            {
                // Enter
                this._btReadReadWrite.Focus();
                _btReadReadWrite_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up                
                btnSync_Click(null, null);
            }
        }
        #endregion

        #region DataSyncLog
        public void DataSyncLogInfo()
        {
            try
            {
                string datet = System.DateTime.Now.AddMinutes(-+DataSyncTimeDiff).ToString("yyy-MM-dd HH:mm:ss ");//five minits
                string ndt = "{ts '" + datet + "'}";
                string CONNSTRING = sqlConnection;
                SqlConnection sqldbCon1 = new SqlConnection(CONNSTRING);
                sqldbCon1.Open();
                string sqlc = "select count(*) from [dbo].[DataSync_Log]  where [ReaderId]=" + ReaderId + " and [SyncStaus]='InProgress' and [SyncEndTime]>" + ndt + "";
                SqlCommand cmd = new SqlCommand(sqlc, sqldbCon1);
                int fl;
                fl = int.Parse(cmd.ExecuteScalar().ToString());
                //classLog.writeLog(sqlc.ToString());
                if (fl == 0)
                {
                    //classLog.writeLog(fl.ToString());
                    string sql1 = "Insert into [dbo].[DataSync_Log] ([ReaderId],[SyncStartTime],[TotalRecordstoSync],[SyncStaus],[SyncEndTime],[ReaderNo]) Values ('" + ReaderId + "',GETDATE()," + SyncRecordCount + ",'InProgress',GETDATE(),'" + ReaderName + "')";
                    SqlCommand cmd1 = new SqlCommand(sql1, sqldbCon1);
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                    //classLog.writeLog(sql1.ToString());
                }
                else //if(fl >= 1)
                {
                    string sql2 = "Update [dbo].[DataSync_Log] set [UpdateTime]=GETDATE()where [ReaderId]=" + ReaderId + " and [SyncStaus]='InProgress' and [SyncEndTime]>" + ndt + "";
                    SqlCommand cmd2 = new SqlCommand(sql2, sqldbCon1);
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                }
                sqldbCon1.Close();
            }
            catch (Exception ex)
            {
                classLog.writeLog("Error @:"+ex.ToString());
            }
        }

        public void DataSyncLogCompleteInfo()
        {
            string datet = System.DateTime.Now.AddMinutes(-+DataSyncTimeDiff).ToString("yyy-MM-dd HH:mm:ss ");
            string ndt = "{ts '" + datet + "'}";
            string CONNSTRING = sqlConnection;
            SqlConnection sqldbCon1 = new SqlConnection(CONNSTRING);
            sqldbCon1.Open();
            string sql1 = "Update  [dbo].[DataSync_Log] set [SyncStaus]='Completed',[SyncEndTime]=GETDATE() where [ReaderId]=" + ReaderId + " and [SyncStaus]='InProgress' and [SyncEndTime]>" + ndt + "";
            SqlCommand cmd1 = new SqlCommand(sql1, sqldbCon1);
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            sqldbCon1.Close();
        }
        #endregion

        #region Load or UnLoad Button Operation for Plant Operation
        private void btnLoadorUnLoad_Click(object sender, EventArgs e)
        {
            btnLoadorUnLoad.Enabled = false;
            btnVerification.Enabled = false;
            string connectionString = localConnection;
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                if (btnLoadorUnLoad.Text == "UnLoad") ReaderOperation = "UnLoad"; if (btnLoadorUnLoad.Text == "Load") ReaderOperation = "Load";
                string tagno = _txtReadReadWrite.Text.ToString();
                string truckno = truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
                string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + datet + "'}";
                SqlCeCommand cmd1 = new SqlCeCommand("Insert into InternalFleet(TagNo,TruckNo,TaskCode,SubTaskCode,Location,ReaderNo,ReaderIP,Status,CreatedBy,ReadTime,ReaderOperation)" + "Values('" + tagno + "','" + truckno + "','" + TaskCode + "','" + SubTaskCode + "','" + LocationId + "','" + ReaderId + "','" + readerip + "','Active','" + User + "'," + ndt + ",'" + ReaderOperation + "')", connection);
                cmd1.ExecuteNonQuery();
                connection.Close();
                _lbReadInfoReadWrite.Text = "Saved..in Offline."; classLog.writeLog("Message @:Details Saved In Offline.");
                //MessageBox.Show("Saved..in Offline."); 
                InternetAvailable(tagno, ndt, ReaderId); 
                btnLoadorUnLoad.Visible = false;
                btnVerification.Visible = false;
                
            }
            localdbrecordcount();
        }
        #endregion
        #region Verification Button Operation for Plant Operation
        private void btnVerification_Click(object sender, EventArgs e)
        {
            btnLoadorUnLoad.Enabled = false;
            btnVerification.Enabled = false;
            string connectionString = localConnection;
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                ReaderOperation = "Verification";
                //if (btnLoadorUnLoad.Text == "UnLoad") ReaderOperation = "UnLoad"; if (btnLoadorUnLoad.Text == "Load") ReaderOperation = "Load";
                string tagno = _txtReadReadWrite.Text.ToString();
                string truckno = truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
                string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + datet + "'}";
                SqlCeCommand cmd1 = new SqlCeCommand("Insert into InternalFleet(TagNo,TruckNo,TaskCode,SubTaskCode,Location,ReaderNo,ReaderIP,Status,CreatedBy,ReadTime,ReaderOperation)" + "Values('" + tagno + "','" + truckno + "','" + TaskCode + "','" + SubTaskCode + "','" + LocationId + "','" + ReaderId + "','" + readerip + "','Active','" + User + "'," + ndt + ",'" + ReaderOperation + "')", connection);
                cmd1.ExecuteNonQuery();
                connection.Close();
                _lbReadInfoReadWrite.Text = "Saved..in Offline."; classLog.writeLog("Message @:Details Saved In Offline.");
                //MessageBox.Show("Saved..in Offline."); 
                InternetAvailable(tagno, ndt, ReaderId); 
                btnLoadorUnLoad.Visible = false;
                btnVerification.Visible = false;
                
            }
            localdbrecordcount();
            
        }
        #endregion
        #region Update the DataSync Time in TaskSync_Log Table.
        public void DataSyncTime_TaskSync()
        {
            string cmd = "SELECT [TaskCode],[SubTaskCode],[ReaderId] FROM TaskConfig WHERE Status='Active'";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                    DataTable dt=new DataTable();
                    try
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr != null)
                                {
                                    
                                    string UpdateQry = "Update [dbo].[TaskSync_Log] set [DataSyncTime]=GETDATE(),[DataSyncBy]= '" + User + "' where [TaskCode]='" + dr[0].ToString() + "' and [SubTaskCode]='" + dr[1].ToString() + "' and [ReaderId]='" + dr[2].ToString() + "' and LogId=(Select Max(LogId) from [dbo].[TaskSync_Log] where [TaskCode]='" + dr[0].ToString() + "' and [SubTaskCode]='" + dr[1].ToString() + "' and [ReaderId]='" + dr[2].ToString() + "') ";
                                    
                                    string CONNSTRING = sqlConnection;
                                    SqlConnection sqldbCon1 = new SqlConnection(CONNSTRING);
                                    
                                    try
                                        {
                                            sqldbCon1.Open();
                                            SqlCommand cm = new SqlCommand(UpdateQry, sqldbCon1);
                                            cm.ExecuteNonQuery();
                                            cm.Dispose();
                                            sqldbCon1.Close();
                                            classLog.writeLog("Message @:DataSync Time Updated.");
                                            
                                        }
                                   catch { classLog.writeLog("Message @:Data to Sync Time Not updated.");
                                         }
                                }
                            }
                        }
                        {
                            classLog.writeLog("Warning @:Task Not Available.");
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        classLog.writeLog("Message @:No Data to Sync.");
                    }
        }
        #endregion


        #region GPRS Transaction Capturing
        private void InternetAvailableOld(string tagno,string ndt,string reader)
        {
            bool hasNet = false;
            //try
            //{
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://www.google.co.in");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                request.Timeout = 5000;
                //request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        hasNet = true;
                        
                        classLog.writeLog("Message @:Internet Connected.");
                        string CONN_STRING = gprsSqlConnection;
                        SqlConnection dbCon1 = new SqlConnection(CONN_STRING);
                        try
                        {
                            dbCon1.Open();
                            classLog.writeLog("Message @:SQL Connected.");
                            string Query = "Insert into [dbo].[GPRS_DataTbl] ([TagNo],[ReaderId],[TagReadTime],[SyncTime]) Values('" + tagno + "','" + ReaderId + "'," + ndt + ",GETDATE())";
                            SqlCommand cm = new SqlCommand(Query, dbCon1);
                            cm.ExecuteNonQuery();
                            dbCon1.Close();
                            _lbReadInfoReadWrite.Text = "Saved In Online";
                            classLog.writeLog("Message @:Details Saved In Online."); clearGPRSlocaldb();
                            
                        }
                        catch (Exception ex)
                        {
                            _lbReadInfoReadWrite.Text = "SQL Connection Failed.";
                            txtCount.Visible = false;
                            //MessageBox.Show(ex.ToString());
                            classLog.writeLog("Error @:SQL Not Connected.");
                        }
                    }
                    else
                    {
                        _lbReadInfoReadWrite.Text = "Internet Connection Failed.";
                        classLog.writeLog("Error @:Internet Not Connected..");
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "NetAvail2");
            //    classLog.writeLog("Error @:Internet Not Connected.");
            //}
        }
        private void InternetAvailableNew(string tagno, string ndt, string reader)
        {
            bool hasNet = false;
            try
            {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://www.google.co.in");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.Proxy = null;
            request.Timeout = 5000;
            //request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    hasNet = true;
                    //classLog.writeLog("Message @:Internet Connected.");                    
                    try
                    {
                        if (dbCon1.State == ConnectionState.Closed)
                        {
                            dbCon1.Open();
                        }
                        //dbCon1.Open();
                        //classLog.writeLog("Message @:SQL Connected.");
                        string Query = "Insert into [dbo].[GPRS_DataTbl] ([TagNo],[ReaderId],[TagReadTime],[SyncTime]) Values('" + tagno + "','" + ReaderId + "'," + ndt + ",GETDATE())";
                        SqlCommand cm = new SqlCommand(Query, dbCon1);
                        cm.Parameters.Add("@ReaderId", SqlDbType.NVarChar).Value = ReaderId;
                        
                        cm.ExecuteNonQuery(); cm.Dispose();
                        //dbCon1.Close();
                        _lbReadInfoReadWrite.Text = "Saved In Online";
                        classLog.writeLog("Message @:Details Saved In Online.");

                    }
                    catch (Exception ex)
                    {
                        _lbReadInfoReadWrite.Text = "SQL Connection Failed.";
                        txtCount.Visible = false;
                        classLog.writeLog("Error @:SQL Not Connected.");
                    }
                }
                else
                {
                    _lbReadInfoReadWrite.Text = "GPRS Connection Failed.";
                    classLog.writeLog("Error @:GPRS Not Connected..");
                }
            }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NetAvail2");
                _lbReadInfoReadWrite.Text = "Internet Connection Failed.";
                classLog.writeLog("Error @:Internet Connection Failed.");
            }
        }
        string GPRSSyncSts;
        private void InternetAvailableOffline(string tagno, string ndt, string reader)
        {
            sts = "N";
            bool hasNet = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://www.google.co.in");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                request.Timeout = 5000;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        hasNet = true;
                        string CONN_STRING = gprsSqlConnection;
                        SqlConnection dbCon1 = new SqlConnection(CONN_STRING);                    
                        try
                        {
                            dbCon1.Open();
                            string Query = "Insert into [dbo].[GPRS_DataTbl] ([TagNo],[ReaderId],[TagReadTime],[SyncTime]) Values('" + tagno + "','" + ReaderId + "'," + ndt + ",GETDATE())";
                            SqlCommand cm = new SqlCommand(Query, dbCon1);
                            cm.Parameters.Add("@ReaderId", SqlDbType.NVarChar).Value = ReaderId;
                            cm.ExecuteNonQuery(); cm.Dispose(); sts = "Y";
                            dbCon1.Close();
                            _lbReadInfoReadWrite.Text = "Saved In Online";
                            
                            classLog.writeLog("Message @:Details Saved In Online.");
                            GPRSSyncSts = "Success";
                        }
                        catch (Exception ex)
                        {
                            _lbReadInfoReadWrite.Text = "SQL Connection Failed.";
                            txtCount.Visible = false;
                            classLog.writeLog("Error @:SQL Not Connected."); GPRSSyncSts = "Fail";
                        }
                    }
                    else
                    {
                        _lbReadInfoReadWrite.Text = "GPRS Connection Failed.";
                        classLog.writeLog("Error @:GPRS Not Connected.."); GPRSSyncSts = "Fail";
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NetAvail2");
                _lbReadInfoReadWrite.Text = "Internet Connection Failed.";
                classLog.writeLog("Error @:Internet Connection Failed."); GPRSSyncSts = "Fail";
            }
        }
        #endregion


        public void clearGPRSlocaldb()
        {
            string Query = "UPDATE InternalFleet SET Status='InActive' WHERE Id IN (SELECT Max(Id) FROM InternalFleet)";
            SqlCeCommand cm = new SqlCeCommand(Query, dbCon);
            cm.ExecuteNonQuery();
            cm.Dispose();
            _btReadReadWrite.Focus();
        }
        //public static bool IsConnectedToInternet
        //{
        //    get
        //    {
        //        Uri url = new Uri("www.abhisheksur.com");
        //        string pingurl = string.Format("{0}", url.Host);
        //        string host = pingurl;
        //        bool result = false;
        //        Ping p = new Ping();
        //        try
        //        {
        //            PingReply reply = p.Send(host, 3000);
        //            if (reply.Status == IPStatus.Success)
        //                return true;
        //        }
        //        catch { }
        //        return result;
        //    }
        //}


        #region GPRS Transaction useing WebService
        private void InternetAvailable(string tagno, string ndt, string reader)
        {
            bool hasNet = false;
            try
            {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://www.google.co.in");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://182.72.244.27/RFIDWeb/RFIDService.asmx");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.Proxy = null;
            request.Timeout = 5000;
            //request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    hasNet = true;
                    classLog.writeLog("Message @:Internet Connected.");
                        //classLog.writeLog("Message @:Tag,Reader,ReadeTime" + tagno.ToString() + "," + reader.ToString() + "," + ndt.ToString());
                        RFIDWEB.Service1 todb = new RFIDWEB.Service1();
                        string opstatus = todb.ReadDetails(tagno, reader, ndt);
                        if (opstatus == "SUCCESS")
                        {
                            _lbReadInfoReadWrite.Text = "Saved In Online";
                            classLog.writeLog("Message @:Details Saved In Online."); clearGPRSlocaldb();
                        }
                        else
                        {
                            _lbReadInfoReadWrite.Text = "Connection Failed.";
                            classLog.writeLog("Message @:Internet Connectivity Failed.");
                        }
                }
                else
                {
                    _lbReadInfoReadWrite.Text = "Internet Connection Failed.";
                    classLog.writeLog("Error @:Internet Not Connected..");
                }
            }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NetAvail2");
                classLog.writeLog("Error @:Internet Not Connected.");
            }
        }
        #endregion
    }
}