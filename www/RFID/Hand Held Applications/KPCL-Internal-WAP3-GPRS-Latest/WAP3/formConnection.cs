using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PsionTeklogix.RFID;

namespace WAP3
{
    public partial class formConnection : Form
    {
        [DllImport("coredll.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);
        const uint SPI_GETPLATFORMTYPE = 257;
        const uint SPI_GETOEMINFO = 258;

        string Platform = string.Empty;

        RFIDDriver rfidDriver = new RFIDDriver();

        public formConnection()
        {
            InitializeComponent();

            // Get platform type / WAP 
            StringBuilder strbuild = new StringBuilder(200);
            SystemParametersInfo(SPI_GETOEMINFO, 200, strbuild, 0);
            Platform = strbuild.ToString();

            _lblVersion.Text = "Version: " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() +
                "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');


            if (rfidDriver.IsInstalled)
            {
                _lbDriverStatus.Text = "RFID Driver status: Installed";
                _lbDriverStatus.ForeColor = Color.Green;
                _bt.Enabled = true;
            }
            else
            {
                _lbDriverStatus.Text = "RFID Driver status: NOT installed";
                _lbDriverStatus.ForeColor = Color.Red;
                _bt.Enabled = false;
            }

            // Display adapted information according if the RFID Driver is installed
            _btDriverInfos_Click(null, new EventArgs());
        }

        private void _bt_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();

                Display("Initialize", Images.Arrow);
                // Enable RFID Driver
                rfidDriver.Enable();

                // Get ComPort number where RFID reader is connected
                int ComPort = rfidDriver.ComPort;

                Display("Launching RFID App ...", Images.Arrow);
                formInternal f = new formInternal(ComPort);
                //Form1 f = new Form1();
                f.ShowDialog();
                Display("RFID APP closed", Images.Arrow);
                f.Dispose();

            }
            catch (RFIDDriverException rdex)
            {
                Display("RFIDDriver exception", Images.Fail);
                Display(rdex.Message);

                if(rdex.RFIDDriverMessage == RFIDDriverMessages.RFID_ERR_ACTIVATING_VIRTUAL_PORT)
                    Display("This error can be thrown with Windows Mobile OS when Add-On CAB file is not installed");
            }
            catch (Exception ex)
            {
                Display("Error loading KPCL-RFID App", Images.Fail);
                Display(ex.Message);
            }
            finally
            {
                Display("Shutdown", Images.Arrow);
                // Disable RFID Driver 
                if (rfidDriver.IsEnabled) rfidDriver.Disable();
            }
        }

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
        }
        #endregion

        private void _btDriverInfos_Click(object sender, EventArgs e)
        {
            Clear();
            if (rfidDriver.IsInstalled)
            {
                List<String> infos = rfidDriver.GetInfos();
                Display("RFID DRIVER INFOS", Images.Arrow);
                foreach (string s in infos)
                {
                    Display(s);
                }
            }
            else
            {
                Display("RFID Driver NOT installed", Images.Fail);
                Display("Installation:", Images.Arrow);
                Display("Select the RFIDDriver CAB file according your RFID hardware");
                Display("Copy RFIDDriver CAB file on the Psion Teklogix device");
                Display("Execute the CAB file");
                Display("Start again CAEN Demo");
                Display("You should see the new status 'Installed'");
            }

        }

        private void formConnection_Closing(object sender, CancelEventArgs e)
        {
            classLog.writeLog("Connection Form Closed");
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void formConnection_Load(object sender, EventArgs e)
        {
            classLog.writeLog("Connection Form Loaded");
            _lblVersion.Text="Version: "+classLogin.Version;
        }

       
    }
}