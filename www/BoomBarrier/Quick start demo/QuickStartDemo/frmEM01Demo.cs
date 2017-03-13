using System;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using Magnetic.MGCDriver.Base;
using Magnetic.MGCDriver.MGCConnector;
using Magnetic.MGCDriver.Utils;

namespace QuickStartDemo
{
    public partial class frmEM01Demo : Form
    {
        private cMGCConnector _con = new cMGCConnector();

        public frmEM01Demo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Connect to the barrier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_con.IsConnected)
            {
                string ip = tbIp1.Text + "." + tbIp2.Text + "." + tbIp3.Text + "." + tbIp4.Text;
                IPAddress address;
                if (IPAddress.TryParse(ip, out address))
                {
                    if (_con.ConnectByEthernetmodule(ip))
                    {
                        btnConnect.Text = "Disconnect";
                    }    
                }
                else
                {
                    MessageBox.Show("Check IP!");
                }
            }
            else
            {
                _con.Disconnect();
                btnConnect.Text = "Connect";
            }
        }

        /// <summary>
        /// Opens the barrier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                _con.WriteModbusRegisterUInt16(0x0000, 0x0001);
            }
        }

        /// <summary>
        /// Colses the barrier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                _con.WriteModbusRegisterUInt16(0x0000, 0x0002);
            }
        }

        /// <summary>
        /// Changes the speed for closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpeed_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                const byte node = 0x01;         //Master
                const ushort index = 0x2102;    //Index Barrier Settings
                const byte sub = 0x03;          //Subindex Speed Down
                _con.WriteSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned8),
                              Convert.ToByte(udSpeed.Value));
            }
        }

        /// <summary>
        /// Refresh the Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                uint status = 0;

                if (_con.ReadModbusRegisterUInt32(0x0001, ref status))
                {
                    byte[] byteStatus = BitConverter.GetBytes(status);
                    //Update the Status - see [5815,0000 table 3]
                    laBoomAngle.Text = byteStatus[3].ToString(CultureInfo.InvariantCulture) + "°";
                    if ((byteStatus[2] & 0x80) == 0x80)
                    {
                        laStatus.Text = "Boom in position open";
                    }
                    else if ((byteStatus[2] & 0x10) == 0x10)
                    {
                        laStatus.Text = "Boom in position closed";
                    }
                    else
                    {
                        laStatus.Text = String.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Read the programm mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgMode_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                byte mode = 4;
                if (_con.ReadSDO(new cMGCDriver.cSdoSet(0x01, 0x2104, 0x01, cMGCDriver.eCANOpenDatatypes.Unsigned8), out mode))
                {
                    laProgMode.Text = mode.ToString();
                }
            }
        }

        private void frmEM01Demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_con.IsConnected)
            {
                _con.Disconnect();
            }
        }

        private void btnSetOut6Ext_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                const byte node = 0x01;         //Master
                const ushort index = 0x2111;    //Index Output functions
                const byte sub = 0x0A;          //Subindex Relay 6
                const byte outExternal = 65;    //Set output for External control
                _con.WriteSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned8),
                              outExternal);
            }
        }

        private void cbToggleRelay6_CheckedChanged(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                const byte node = 0x01;         //Master
                const ushort index = 0x6300;    //Index Write Outputs
                const byte sub = 0x01;          //Subindex Relay 6
                ushort setClear;

                if (cbToggleRelay6.Checked)
                    setClear = 0x200;  //Set Bit 10
                else
                    setClear = 0x000;

                _con.WriteSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned16),
                              setClear);
            }
        }

        private void btnReadErrors_Click(object sender, EventArgs e)
        {
            if (_con.IsConnected)
            {
                const byte node = 0x01;         //Master
                ushort index = 0x2003;    //Index "Error History"
                byte sub = 0x01;                //Subindex "Index"
                byte num = (byte)udErrIdx.Value;

                // Set the error index
                if (_con.WriteSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned8), num))
                {
                    // Read the error code
                    sub = 0x02;
                    ushort errorCode;
                    if (_con.ReadSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned16), out errorCode))
                        laErrNr.Text = errorCode.ToString("X");

                    // Read the time stamp
                    sub = 0x04;
                    string timestamp;
                    if (_con.ReadSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.VString), out timestamp))
                        laErrorTimeStamp.Text = timestamp;

                    // Read the node id
                    sub = 0x03;
                    byte nodeId = 0x01;
                    if (_con.ReadSDO(new cMGCDriver.cSdoSet(node, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned8),
                                     out nodeId))
                    {

                        laErrorNode.Text = nodeId.ToString();

                        // lookup the error text
                        index = 0x2001;
                        // Write the error code
                        sub = 0x01;
                        if (_con.WriteSDO(new cMGCDriver.cSdoSet(nodeId, index, sub, cMGCDriver.eCANOpenDatatypes.Unsigned16),
                                          errorCode))
                        {
                            // read the error text
                            sub = 0x02;
                            string errorText;
                            _con.ReadSDO(new cMGCDriver.cSdoSet(nodeId, index, sub, cMGCDriver.eCANOpenDatatypes.VString), out errorText);
                            laErrText.Text = errorText;

                        }
                    }
                }
            }
        }
    }
}
