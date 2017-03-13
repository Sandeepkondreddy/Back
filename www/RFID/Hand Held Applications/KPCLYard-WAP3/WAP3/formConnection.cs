using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlServerCe;
using PsionTeklogix.RFID;
namespace WAP3
{
    public partial class formConnection : Form
    {
        string OpType = classLogin.OpType;
        [DllImport("coredll.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);
        const uint SPI_GETPLATFORMTYPE = 257;
        const uint SPI_GETOEMINFO = 258;
        string Platform = string.Empty;
        RFIDDriver rfidDriver = new RFIDDriver();
        string localConnection = classServerDetails.SdfConnection;
        string Operation;
        public void getTaskdetailsfromLocaldb()//Reader Operation
        {
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select DeviceOperation  from TaskConfig ORDER BY ID DESC";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Operation = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                
            }
            catch
            {
                MessageBox.Show("Task Details Not found.");
            }
        }


        public formConnection()
        {
            
            InitializeComponent();
            // Get platform type / WAP 
            StringBuilder strbuild = new StringBuilder(200);
            SystemParametersInfo(SPI_GETOEMINFO, 200, strbuild, 0);
            Platform = strbuild.ToString();

            //_lblVersion.Text = "Version: " +
            //    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() +
            //   "." +
            //  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString().PadLeft(2, '0');


            if (rfidDriver.IsInstalled)
            {
                _lbDriverStatus.Text = "RFID Driver status: Installed";
                _lbDriverStatus.ForeColor = Color.Green;
                btnConnection.Enabled = true;
            }
            else
            {
                _lbDriverStatus.Text = "RFID Driver status: NOT installed";
                _lbDriverStatus.ForeColor = Color.Red;
                btnConnection.Enabled = false;
            }

            // Display adapted information according if the RFID Driver is installed
            btnDriverInfo_Click(null, new EventArgs());
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
        private void btnDriverInfo_Click(object sender, EventArgs e)
        {
            Clear();
            if (rfidDriver.IsInstalled)
            {
                List<String> infos = rfidDriver.GetInfos();
                Display("RFID DRIVER Info.", Images.Arrow);
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
                Display("Start again KPCL-RFID App");
                Display("You should see the new status 'Installed'");
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
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
                    //ComPort
                    //getTaskdetailsfromLocaldb();
                    //if (Operation == "Load")
                    //{
                    //    formInternalFleetReg f = new formInternalFleetReg(ComPort);
                    //    f.ShowDialog();
                    //}
                    if (OpType.ToString() == "Reg")
                    {
                        //DemoCAEN f = new DemoCAEN(_cbComPort.SelectedIndex + 1);

                        formGateRegister f = new formGateRegister(ComPort);
                        f.ShowDialog();
                        Display("RFID closed", Images.Arrow);
                        f.Dispose();
                    }
                    else if (OpType.ToString() == "UnReg")
                    {
                        formGateUnRegister f = new formGateUnRegister(ComPort);
                        f.ShowDialog();
                        Display("RFID closed", Images.Arrow);
                        f.Dispose();
                    }
                    else if (OpType.ToString() == "Verify")
                    {
                        formVerify f = new formVerify(ComPort);
                        f.ShowDialog();
                        Display("RFID closed", Images.Arrow);
                        f.Dispose();
                    }
                    else if (OpType.ToString() == "Yard")
                    {
                        formYardOperations f = new formYardOperations(ComPort);
                        f.ShowDialog();
                        Display("RFID closed", Images.Arrow);
                        f.Dispose();
                    }
                    else
                    {
                        //formVerify f = new formVerify(ComPort);
                        //f.ShowDialog();
                        //Display("RFID closed", Images.Arrow);
                        //f.Dispose();
                    }
                        Display("KPCL-RFID APP closed", Images.Arrow);
                    
                    

                }
                catch (RFIDDriverException rdex)
                {
                    Display("RFIDDriver exception", Images.Fail);
                    Display(rdex.Message);

                    if (rdex.RFIDDriverMessage == RFIDDriverMessages.RFID_ERR_ACTIVATING_VIRTUAL_PORT)
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
        
    }
}