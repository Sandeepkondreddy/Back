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
using PsionTeklogix.Keyboard;
using PsionTeklogix.Trigger;
using PsionTeklogix.Barcode;
using PsionTeklogix.Barcode.ScannerServices;
namespace WAP3
{
    public partial class formVerify : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string User = classLogin.User;
        string readerip = classLogin.ReaderIP;
        string ConnType = classLogin.ConnType;
     
        string readerno;
        int strength;
        int count;
        #region Triggar Control....
        const string szTemporaryClient = "Pistol-Grip";

        bool bScannerIsActive = false;

        Key[] ScannerKeyList = { Key.SideLeftScan, 
                                 Key.Scan, 
                                 Key.HandgripScan, 
                                 Key.SideRightScan};

        String[] szFriendlyNameList = { "[Left Scan]",
                                        "[Scan button]",
                                        "[Pistol Grip]",
                                        "[Right Scan]" };


        Scanner scanner = null;
        ScannerServicesDriver scannerServicesDriver = null;

        TriggerControl tc = null;
        private bool InitializeScannerControl()
        {
            try
            {
                scanner = new PsionTeklogix.Barcode.Scanner();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Scanner instantiation failed with " + ex.Message);
                return false;
            }

            try
            {
                scannerServicesDriver = new ScannerServicesDriver();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ScannerServicesDriver instantiation failed with " + ex.Message);
                return false;
            }
            scanner.Driver = scannerServicesDriver;
            scanner.ScanCompleteEvent += new ScanCompleteEventHandler(OnScanCompleteEvent);
            scanner.ScanFailedEvent += new ScanFailedEventHandler(OnScanFailedEvent);

            return true;
        }
        private bool InitializeTriggerControl()
        {
            // Instantiate Trigger Control
            try
            {
                tc = new TriggerControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("TriggerControl instantiation failed with " + ex.Message);
                return false;
            }

            // Initialize Trigger Control
            try
            {
                tc.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("TriggerControl initialization failed with " + ex.Message);
                return false;
            }

            ArrayList names = new ArrayList();
            try
            {
                tc.GetRegisteredConsumers(names);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetRegisteredConsumers() failed with " + ex.Message);
                return false;
            }

            Boolean ConsumerAlreadExist = false;
            foreach (string s in names)
            {
                if (s.Equals(szTemporaryClient))
                {
                    ConsumerAlreadExist = true;
                    break;
                }
            }

            // Register the temporary trigger event consumer
            if (ConsumerAlreadExist == false)
            {
                try
                {
                    tc.RegisterConsumer(szTemporaryClient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("RegisterConsumer() failed with " + ex.Message);
                    return false;
                }
            }

            // Associate Scanner keys and Temporary Client
            foreach (Key k in ScannerKeyList)
            {
                try
                {
                    tc.AddMapping(Keyboard.TranslateToTriggerID(k), szTemporaryClient, TriggerControl.Flags.Override | TriggerControl.Flags.Temporary | TriggerControl.Flags.IgnoreDupReg | TriggerControl.Flags.WantsTriggerEvents);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddMapping() failed with " + ex.Message);
                    return false;
                }
            }

            for (int index = 0; index < ScannerKeyList.Length; index++)
            {
                try
                {
                    tc.SetTriggerSourceFriendlyName(Keyboard.TranslateToTriggerID(ScannerKeyList[index]), szFriendlyNameList[index]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SetTriggerSourceFriendlyName() failed with " + ex.Message);
                    return false;
                }
            }


            // Register for events
            try
            {
                tc.RegisterForEvents(szTemporaryClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RegisterForEvents failed");
                return false;
            }

            // Add delegate
            try
            {
                tc.triggerEvent += new TriggerEventHandler(OnTriggerEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed with\r\n" + ex.Message, "new TriggerEventHandler()");
                return false;
            }
            return true;
        }
        delegate void OnScanCompleteEventDelegate(object sender, PsionTeklogix.Barcode.ScanCompleteEventArgs e);
        private void OnScanCompleteEvent(object sender, PsionTeklogix.Barcode.ScanCompleteEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new OnScanCompleteEventDelegate(OnScanCompleteEvent), new object[] { sender, e });
                return;
            }
            bScannerIsActive = false;
            //listBox1.Items.Add(DateTime.Now.ToLocalTime().ToLongTimeString() + " - " + e.Text + " (" + e.Symbology + ")");
            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        delegate void OnScanFailedEventDelegate(object sender, PsionTeklogix.Barcode.ScanFailedEventArgs e);
        private void OnScanFailedEvent(object sender, PsionTeklogix.Barcode.ScanFailedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new OnScanFailedEventDelegate(OnScanFailedEvent), new object[] { sender, e });
                return;
            }
            bScannerIsActive = false;
            // listBox1.Items.Add(DateTime.Now.ToLocalTime().ToLongTimeString() + " - " + e.ToString());
            // listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void OnTriggerEvent(object sender, TriggerEvent e)
        {
            Read();
        }
        #endregion
        #region Properties
        int ComPort;
        bool bOpen = false;
        // Read ID Options  (default)
        PTEMEAMS.Designs.ReadIDOptionsPanel.ReadIDOptions opReadIDOptions;

        // Read ID: Chrono options
        // private bool bActiveChrono = false;
        //private int iNbTagsToRead = 10;
        //private int StartTime = 0;

        // Read ID: Display
        private bool bASCIIDisplay = false;
        private bool bHexadecimalDisplay = true;

        // Read ID: Session
        //private bool bAlternate = false;
        //private bool bReset = false;

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
        #region Display
        enum Images
        {
            Point,
            Arrow,
            Success,
            Fail
        }

        private void Display(string Message, Images i)
        {
            if (Message != null)
                if (Message != "")
                {
                    ListViewItem lvi = new ListViewItem(new string[] { Message });
                    lvi.ImageIndex = (int)i;
                    // Add ListViewItem to the listview
                    _lv.Items.Add(lvi);
                }

            _lv.Refresh();
        }

        private void Display(string Message)
        {
            Display(Message, Images.Point);
        }

        private void Clear()
        {
            _lv.Items.Clear();
            _lv.Refresh();
        }
        #endregion
        #region CAEN Reader
        public void OpenReader()
        {
            try
            {
                Reader.Connect(com.caen.RFIDLibrary.CAENRFIDPort.CAENRFID_RS232, "COM" + this.ComPort.ToString() + ":115200");

                bOpen = true;
            }
            catch (Exception ex)
            {
                Display("Error", Images.Fail);
                throw ex;
            }
        }

        public void CloseReader()
        {
            try
            {
                if (bOpen)
                {
                    Reader.Disconnect();

                    bOpen = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList Inventory()
        {
            try
            {
                if (bOpen)
                {
                    ArrayList al = new ArrayList();


                    if (bActiveEPCFilter)
                        m_RFIDTags = m_Source0.InventoryTag(Mask, MaskLenght, Position);
                    else
                        m_RFIDTags = m_Source0.InventoryTag();


                    if (m_RFIDTags == null)
                        throw new Exception("No tag in the field");

                    if (m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    for (int cpt = 0; cpt < m_RFIDTags.Length; cpt++)
                    {
                        string IDTag = "";
                        if (bHexadecimalDisplay)
                            IDTag = System.BitConverter.ToString(m_RFIDTags[cpt].GetId()).Replace("-", "");
                        if (bASCIIDisplay)
                            foreach (byte b in m_RFIDTags[cpt].GetId())
                                IDTag += ((char)b).ToString();

                        al.Add(IDTag);

                    }
                    return al;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region graphics
        private void InitGraphics()
        {
            _plConfiguration.Location = new Point(0, _plConfiguration.Location.Y);
            _plReadID.Location = new Point(0, _plReadID.Location.Y);
            _plReadWrite.Location = new Point(0, _plReadWrite.Location.Y);
            // Add specific columns for sort data
            this._lvReadID.Columns.Add(new ColHeader("TagID", 169, System.Windows.Forms.HorizontalAlignment.Left, true));
            this._lvReadID.Columns.Add(new ColHeader("#", 39, System.Windows.Forms.HorizontalAlignment.Left, true));
            this._lvReadID.Refresh();
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Refresh();


        }
        public class ColHeader : ColumnHeader
        {
            public bool ascending;
            public ColHeader(string text, int width, HorizontalAlignment align, bool asc)
            {
                this.Text = text;
                this.Width = width;
                this.TextAlign = align;
                this.ascending = asc;
            }
        }
        #endregion
        #region button enable/disable
        public void btndisable()
        {
            //btnSave.Enabled = false;
            _btReadReadWrite.Enabled = false;
            _btClearReadWrite.Enabled = false;
            //btnExit.Enabled = false;
           // btnSync.Enabled = false;
        }
        public void btnenable()
        {
            //btnSave.Enabled = true;
            _btReadReadWrite.Enabled = true;
            _btClearReadWrite.Enabled = true;
            //btnExit.Enabled = true;
            //btnSync.Enabled = true;
        }
        #endregion
        public formVerify(int ComPort)
        {
            InitializeComponent();
            InitGraphics();
            this.ComPort = ComPort;
            opReadIDOptions = readIDOptionsPanel1.Options;
            opReadWriteOptions = readWriteOptionsPanel1.Options;
            #region Trigger Region
            if (!InitializeScannerControl())
            {
                this.Close();
                return;
            }

            if (!InitializeTriggerControl())
            {
                this.Close();
                return;
            }
            #endregion
        }
        private void _btReadReadWrite_Click(object sender, EventArgs e)
        {
            Read();
        }
        public void Read()
        {
            btndisable();
            _plReadWrite.BackColor = System.Drawing.SystemColors.Highlight;
            //lblHdr.BackColor = System.Drawing.SystemColors.Highlight;
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
                        txtTruckNo.ReadOnly = false;
                        txtCargo.ReadOnly = false;
                        if (ConnType.ToString() == "Wifi")
                        {
                            signal();
                            if (strength < -20 && strength > -85)
                            {
                            btnVerify_Click();
                            }
                            else
                            {
                                _lbReadInfoReadWrite.Text = "Enter Truck No.";
                                txtTruckNo.Focus();
                                _lbReadInfoReadWrite.Text = "Wifi Signal is Week.";
                            }
                        }
                        else if(ConnType.ToString() == "GPRS")
                        {
                            btnVerify_Click();
                        }
                        //    insertdatatoLocalDB();
                        //    localdbrecordcount();
                    }
                }

                _lbReadInfoReadWrite.Text = "Reading succeed";
            }
            catch (Exception ex)
            {
                _lbReadInfoReadWrite.Text = ex.Message.Replace("RFID Error: ", "");
            }

            _btReadReadWrite.Enabled = true;
            _btReadReadWrite.Focus();
            btnenable();
        }
        public void signal()
        {
            PsionTeklogix.WLAN.ConnectionState connection =
                WAP3.WLANHelper.GetCurrentConnection();
            //string signal;
            strength = connection.SignalStrength;
            //_lbReadInfoReadWrite.Text = connection.SignalStrength.ToString();
        }
        public void gethandledetailsfromlocaldb()
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            //string readerip = classLogin.ReaderIP;
            //Connection to RFID SQLSERVER database
            string CONN_STRING = localConnection;

            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);


            string cmd = "select ReaderNo,ConnectionType  from ReaderConfig where Status='A'";

            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                readerno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                ConnType = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                dbCon.Close();

            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }
        string TagRegid;
        private void btnVerify_Click() //Pulling the details from SQL db with tag no....
        {
            
            
                string tagno = _txtReadReadWrite.Text.ToString();
                if (tagno.ToString() == "")
                {
                    _lbReadInfoReadWrite.Text = "Tag details not found.";
                    _btReadReadWrite.Focus();
                }
                else
                {
                    string CONN_STRING = sqlConnection;
                    SqlConnection dbCon = new SqlConnection(CONN_STRING);
                    try
                    {
                        dbCon.Open();
                        string cmd = "select [Truck No],[Tag ID],(SELECT dbo.CommodityName([CommodityId])) from [dbo].[Tag_Register] where [Tag No]='" + tagno + "' and [TransctionStatus]='P' order by [Tag ID] desc";
                        SqlDataAdapter da = new SqlDataAdapter(cmd, dbCon);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        txtTruckNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        TagRegid = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        txtCargo.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        txtTruckNo.ReadOnly = true;
                        txtCargo.ReadOnly = true;
                        //btnVerified.Focus();
                        //btnSave_Click();
                    }
                    catch (Exception ex)
                    {
                        _lbReadInfoReadWrite.Text = "Details not found.";
                        txtTruckNo.Focus();
                        txtTruckNo.ReadOnly = true;
                        //_lbReadInfoReadWrite.Text = "Tag details not found.";
                    }
                    dbCon.Close();
                }
            
            
        }
        #region Form Loading/Closeing
        public void getlocation()
        {
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);

            dbCon.Open();
            string cmd = "select Location from ReaderConfig ";
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
            DataSet ds = new DataSet();

            try
            {
                da.Fill(ds);
                lblLocationVal.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                

            }

            catch (SqlCeException ex)
            {
                //Console.WriteLine(ex.Message);
                
                _lbReadInfoReadWrite.Text = "Location Not Found.";
            }
        }
        public void loadreadtab()
        {
            ClearReadWrite();
            getlocation();
            gethandledetailsfromlocaldb();
            if (CurrentBankMemory == BankMemory.EPC)
            {
                _txtReadReadWrite.MaxLength = nByteEPC *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteEPC *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }
            if (CurrentBankMemory == BankMemory.USER)
            {
                _txtReadReadWrite.MaxLength = nByteUSER *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteUSER *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }

            if (Reader.GetReaderInfo().GetModel() == "A528")
                _lbProtocolReadWrite.Text = "EPC C1 G2";
            else
                _lbProtocolReadWrite.Text = _cbProtocol.SelectedItem.ToString();
            _plReadWrite.BringToFront();

        }
        private void ClearReadWrite()
        {
            _lbReadInfoReadWrite.Text = "";
            _lbWriteInfoReadWrite.Text = "";
            _txtReadReadWrite.Text = "";
            _txtWriteReadWrite.Text = "";
        }
        private void formVerify_Load(object sender, EventArgs e)
        {
            if (ConnType.ToString() == "Wifi")
            {
                sqlConnection = classServerDetails.WifiSQLConnection;
            }
            else if (ConnType.ToString() == "GPRS")
            {
                sqlConnection = classServerDetails.GPRSSQLConnection;
            }
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

        private void formVerify_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                // stop Wakeup
                WakeUp.ResumeEvent -= new EventHandler(WakeUp_ResumeEvent);
                WakeUp.Stop();

                CloseReader();
                Display("Reader closed", Images.Arrow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
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
                if (ConnType == "Wifi")
                {
                    signal();
                    if (strength < -20 && strength > -85)
                    {
                        try
                        {

                            save();

                        }
                        catch
                        {
                            _lbReadInfoReadWrite.Text = "DB Not Connected.";
                        }
                    }
                    else
                    {
                        _lbReadInfoReadWrite.Text = "Wifi Signal is Week.";
                        insertdatatoLocalDB();
                        localdbrecordcount();
                    }
                }
                else if (ConnType == "GPRS")
                {
                    try
                    {
                        save();
                    }
                    catch
                    {
                        _lbReadInfoReadWrite.Text = "GPRS Signal is Week.";
                        insertdatatoLocalDB();
                        localdbrecordcount();
                    }
                }
            }
            
            txtTruckNo.ReadOnly = false;
            btnenable();
        }
        public void save()
        {
            string CONN_STRING = sqlConnection;
            SqlConnection dbCon = new SqlConnection(CONN_STRING);
            try
            {
                dbCon.Open();
                insertVerifieddatatoSQLDB();

                //updateTransctionstatusinTagRegTbl();
                //insertexitdatatoLocalDB();
            }
            catch
            {
                insertdatatoLocalDB();
                localdbrecordcount();
            }
        }
        string Cstatus = "0000";
        public void getCurrentStatus()
        {
            try
            {
                string CONNSTRING = sqlConnection;
                SqlConnection cn = new SqlConnection(CONNSTRING);
                cn.Open();
                string sql = "select count(*) from  [dbo].[Tag_Verified] where [TagRegid] ='" + TagRegid + "'";
                SqlCommand cmd = new SqlCommand(sql, cn);
                int flg;
                flg = int.Parse(cmd.ExecuteScalar().ToString());

                if (flg > 0)
                {
                    string sql1 = "select [CStatus] from  [dbo].[Tag_Verified] where [TagRegid] ='" + TagRegid + "' and [CStatus] is NOT NULL order by [VerfiedID] desc";
                    SqlDataAdapter da = new SqlDataAdapter(sql1, cn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Cstatus = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    //SqlCommand cmd1 = new SqlCommand(sql1, cn);
                    //Cstatus = cmd1.ExecuteScalar().ToString();
                }
                else
                {
                    Cstatus = "0000";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        string newCStatus;
        public void insertVerifieddatatoSQLDB() //inserting data into sqldb
        {
            getCurrentStatus();
            if (lblLocationVal.Text == "Warehouse")
            {
                _plReadWrite.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                //lblHdr.BackColor = Color.Salmon;
            }
            else if (lblLocationVal.Text == "CT Gate")
            {
                _plReadWrite.BackColor = System.Drawing.SystemColors.ControlLightLight;
                //lblHdr.BackColor = Color.Aqua;
                
            }
            string tagno = _txtReadReadWrite.Text.ToString();
            
            if (tagno.ToString() == "")
            {
               lblTransactionStatus.Text="Tag details not found.";
               //lblTransactionStatus.ForeColor = Color.Red;
               _plReadWrite.BackColor = Color.Red;
            }
            else
            {
                if ((Cstatus == "0000" || Cstatus == "0001") && lblLocationVal.Text == "Warehouse")
                {
                    lblTransactionStatus.Text = "WB Transaction Pending.";
                    lblTransactionStatus.ForeColor = Color.Black;
                    _plReadWrite.BackColor = Color.Red;
                    //lblNextStageVal.Text = "WB-In";
                }
                else if (Cstatus == "0004" && lblLocationVal.Text == "CT Gate")
                {
                    lblTransactionStatus.Text = "Warehouse Transaction Pending.";
                    lblTransactionStatus.ForeColor = Color.Black;
                    _plReadWrite.BackColor = Color.Red;
                    //lblNextStageVal.Text = "Warehouse-Out";
                }
                else if (Cstatus == "0006" && lblLocationVal.Text == "Warehouse")
                {
                    lblTransactionStatus.Text = "Invalid Transaction.";
                    lblTransactionStatus.ForeColor = Color.Black;
                    _plReadWrite.BackColor = Color.Red;
                    //lblNextStageVal.Text = "WB-Out";
                }
                else if (Cstatus == "0009")
                {
                    lblTransactionStatus.Text = "Transactions Completed.";
                    lblTransactionStatus.ForeColor = Color.Black;
                    _plReadWrite.BackColor = Color.Red;
                    //lblNextStageVal.Text = "Completed.";
                }
                else
                {
                    if (Cstatus == "0000"  && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate-In for First Wt.";
                        newCStatus = "0001";
                        lblTransactionStatus.ForeColor = Color.Black;
                        //_plReadWrite.BackColor = System.Drawing.SystemColors.Info;
                        _plReadWrite.BackColor = Color.LightBlue;
                        //lblNextStageVal.Text = "WB-In.";
                    }
                    else if ( Cstatus == "0001" && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Verification.";
                        newCStatus = "0001";
                        lblTransactionStatus.ForeColor = Color.Black;
                        //_plReadWrite.BackColor = System.Drawing.SystemColors.Info;
                        _plReadWrite.BackColor = Color.LightBlue;
                        //lblNextStageVal.Text = "WB-In.";
                    }
                    else if (Cstatus == "0002" && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Out for WH operations.";
                        newCStatus = "0003";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.Purple;
                        //lblNextStageVal.Text = "Yard/Warehouse-In.";
                    }
                    else if ((Cstatus == "0003") && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Verification.";
                        newCStatus = "0003";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.Purple;
                        //lblNextStageVal.Text = "Yard/Warehouse-In.";
                    }
                    else if ((Cstatus == "0003" || Cstatus == "0002") && lblLocationVal.Text == "Warehouse")
                    {
                        lblTransactionStatus.Text = "Truck Entered in WH/Yard.";
                        newCStatus = "0004";
                        lblTransactionStatus.ForeColor = Color.Black;
                        //_plReadWrite.BackColor = System.Drawing.SystemColors.ActiveCaption;
                        _plReadWrite.BackColor = Color.Aqua;
                        //lblNextStageVal.Text = "Yard/Warehouse-Out.";
                    }

                    else if ((Cstatus == "0004" || Cstatus == "0005") && lblLocationVal.Text == "Warehouse")
                    {
                        lblTransactionStatus.Text = "Truck Out from WH/Yard.";
                        newCStatus = "0005";
                        lblTransactionStatus.ForeColor = Color.Black;
                        //_plReadWrite.BackColor = System.Drawing.SystemColors.AppWorkspace;
                        _plReadWrite.BackColor = Color.Orange;
                        //lblNextStageVal.Text = "WB-Out.";
                    }
                   
                    else if (Cstatus == "0005"  && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate In for Second Wt.";
                        newCStatus = "0006";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.DarkBlue;
                        //lblNextStageVal.Text = "WB-Out.";
                    }
                    else if ( Cstatus == "0006" && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Verification.";
                        newCStatus = "0006";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.DarkBlue;
                        //lblNextStageVal.Text = "WB-Out.";
                    }
                    else if (Cstatus == "0007" && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Out for Exit Process.";
                        newCStatus = "0008";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.GreenYellow;
                        //lblNextStageVal.Text = "Exit Gate.";
                    }
                    else if (Cstatus == "0008" && lblLocationVal.Text == "CT Gate")
                    {
                        lblTransactionStatus.Text = "CT Gate Verification.";
                        newCStatus = "0008";
                        lblTransactionStatus.ForeColor = Color.Black;
                        _plReadWrite.BackColor = Color.GreenYellow;
                        //lblNextStageVal.Text = "Exit Gate.";
                    }
                    string myHost = System.Net.Dns.GetHostName();
                    string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();

                    string truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();

                    string datet = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string ndt = "{ts '" + datet + "'}";
                    string trucktype = "";

                    //Connection to RFID SQLSERVER database
                    string CONN_STRING = sqlConnection;
                    SqlConnection dbCon = new SqlConnection(CONN_STRING);
                    try
                    {
                        dbCon.Open();
                        string Query = "Insert into [dbo].[Tag_Verified]([TagRegid],[TagNo],[TruckNo],[LoadorEmpty],[VerifiedDateTime],[VerifiedHandleNo],[VerifiedHandleIP],[RStatus],[CreatedBy],[CreatedDate],[Location],[CStatus])" + "Values(" + TagRegid + "," + "'" + tagno + "'" + "," + "'" + truckno + "'" + "," + "'" + trucktype + "'" + "," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active'," + "'" + User + "'" + "," + ndt + ",'" + lblLocationVal.Text.ToString() + "','" + newCStatus + "')";
                        SqlCommand cmd = new SqlCommand(Query, dbCon);
                        cmd.ExecuteNonQuery();
                        dbCon.Close();
                        //updateTransctionstatusinTagRegTbl(tagno);
                        _lbReadInfoReadWrite.Text = "Saved..in Online.";
                        // txtTruckNo.Text = "";
                        // _txtReadReadWrite.Text = "";
                        //txtCargo.Text = "";
                    }
                    catch
                    {
                        insertdatatoLocalDB();
                    }
                }
            }
            //adSystem.Threading.Thread.Sleep(2000);
            //_plReadWrite.BackColor = System.Drawing.SystemColors.Highlight;
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
                    string myHost = System.Net.Dns.GetHostName();

                    string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();

                    string trucktype = "";
                    //string readerip = classLogin.ReaderIP;
                   
                    string truckno = truckno = txtTruckNo.Text.Trim().Replace("'", "").Replace(" ", "").ToUpper().ToString();
                    string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    string ndt = "{ts '" + datet + "'}";
                    SqlCeCommand cmd = new SqlCeCommand("Insert into TagVerified(TagNo,TruckNo,EmptyorLoad,VerifiedDateTime,VerifiedHandleNo,VerifiedHandleIP,RStatus,CreatedBy,CreatedDate,Location)" + "Values(" + "'" + tagno + "'" + "," + "'" + truckno + "'" + "," + "'" + trucktype + "'" + "," + ndt + "," + "'" + readerno + "'" + "," + "'" + readerip + "'" + ",'Active'," + "'" + User + "'" + "," + ndt + ",'" + lblLocationVal.Text.ToString() + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    _lbReadInfoReadWrite.Text = "Saved..in Offline.";
                    txtTruckNo.Text = "";
                    _txtReadReadWrite.Text = "";
                    
                }
            }
            localdbrecordcount();
        }
        public void localdbrecordcount()//To get the initial record count from localdb and displaying in the application...
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string sql = "select count(*) from TagVerified  where Rstatus='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, cn);
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

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
        #region Sync
        private void btnSync_Click(object sender, EventArgs e)
        {
            signal();
            if (strength < -20 && strength > -85)
            {
                btndisable();
                string CONN_STRING = sqlConnection;
                SqlConnection dbCon = new SqlConnection(CONN_STRING);
                try
                {
                    dbCon.Open();

                    syncdatafromLocalDBtoSQLDB();
                    localdbrecordcount();
                }
                catch
                {
                    _lbReadInfoReadWrite.Text = "Server cannot be connected.";
                }
                btnenable();
            }
            else
            {
                lblLocalDBstatus.Text = "Signal Strength is Low.";
            }
        }
        int Rid;
        public void syncdatafromLocalDBtoSQLDB()//Getting the details from localdb (sdf) and saveing them in to SQL DB
        {

            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string sql = "select count(*) from TagVerified  where RStatus='Active'";
            SqlCeCommand cmd = new SqlCeCommand(sql, cn);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            if (flg == 0)
            {
                MessageBox.Show("No data in the localdb to sync.");
            }
            else
            {


                for (int i = 0; i < flg; i++)
                {
                    cn.Open();
                    string c = "Select min(VerifiedID) from TagVerified";
                    SqlCeCommand c1 = new SqlCeCommand(c, cn);

                    Rid = int.Parse(c1.ExecuteScalar().ToString());
                    string cmd1 = "select VerifiedID,TagNo,TruckNo,EmptyorLoad,VerifiedDateTime,VerifiedHandleNo,VerifiedHandleIP,RStatus,CreatedBy,Location from TagVerified where  id=" + Rid + "";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd1, cn);
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        string id = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string tagno = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        string tagrigid = getregidvalue(tagno);
                        string truckno = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        string emptyorload = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                        string verifieddatetime = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                        DateTime dt = Convert.ToDateTime(verifieddatetime);
                        string cdt = dt.ToString("yyy-MM-dd HH:mm:ss");
                        string ndt = "{ts '" + cdt + "'}";
                        string handleid = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                        string handleip = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                        string status = ds.Tables[0].Rows[0].ItemArray[7].ToString();

                        string createdby = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                        string location = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                        //Connection to RFID SQLSERVER database
                        string CONN_STRING = sqlConnection;
                        SqlConnection dbCon = new SqlConnection(CONN_STRING);
                        try
                        {
                            dbCon.Open();
                            string Query = "Insert into [dbo].[Tag_Verified]([TagRegid],[TagNo],[TruckNo],[LoadorEmpty],[VerifiedDateTime],[VerifiedHandleNo],[VerifiedHandleIP],[RStatus],[CreatedBy],[CreatedDate],[Location]) Values('" + tagrigid + "'," + "'" + tagno + "'" + ",'" + truckno + "','" + emptyorload + "'," + ndt + "," + "'" + handleid + "'" + "," + "'" + handleip + "'" + ",'" + status + "','" + createdby + "'," + ndt + ",'" + location + "')";
                            SqlCommand cm = new SqlCommand(Query, dbCon);
                            cm.ExecuteNonQuery();
                            //updateTransctionstatusinTagRegTbl(tagno);
                            dbCon.Close();
                            clearlocaldb();
                        }
                        catch
                        {
                            MessageBox.Show("Database Disconnected.");
                        }
                        count--;
                    }
                    catch
                    {
                        MessageBox.Show("Details not found in the Database.");
                    }
                    cn.Close();

                }

                //MessageBox.Show("Successfully Saved..in Online.");
                _lbReadInfoReadWrite.Text = "Sync Complete";
            }


        }
        public void clearlocaldb()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM TagVerified where VerifiedID=" + Rid + "";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
            _btReadReadWrite.Focus();
        }

        public string getregidvalue(string tagno)
        {
            string CONN_STRING = sqlConnection;
            SqlConnection dbCon = new SqlConnection(CONN_STRING);
            string tagrigid;
            try
            {
                dbCon.Open();
                string cmd1 = "select [Truck No],[Tag ID] from [dbo].[Tag_Register] where [Tag No]='" + tagno + "' and [TransctionStatus]='P' order by [Tag ID] desc";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1, dbCon);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                //txtTruckNo.Text = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
                tagrigid = ds1.Tables[0].Rows[0].ItemArray[1].ToString();


            }
            catch
            {
                tagrigid = "";
            }
            return tagrigid;
        }
        #endregion
        private void _btClearReadWrite_Click(object sender, EventArgs e)
        {
            _txtReadReadWrite.Text = "";
            _lbReadInfoReadWrite.Text = "";
            txtTruckNo.Text = "";
            txtCargo.Text = "";
            lblTransactionStatus.Text = "";
            _plReadWrite.BackColor = System.Drawing.SystemColors.Highlight;
        }

       
    }
}