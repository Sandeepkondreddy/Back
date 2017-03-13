using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Magnetic.MGCDriver.Base;
using Magnetic.MGCDriver.MGCConnector;
using Magnetic.MGCDriver.Utils;
using System.Net;

namespace CS_RFID3_Host_Sample1
{
    public partial class formBoomBarrierTest : Form
    {
        private cMGCConnector _con = new cMGCConnector();
        public formBoomBarrierTest()
        {
            InitializeComponent();
        }

        #region BoomBarrier Connection
        public string connect_Boombarrier()
        {
            string ConnectionStaue="";
            if (!_con.IsConnected)
            {
                string ip = classGlobalVariables.AntenaSet1Loc_BoomBarrier_IP.ToString();
                IPAddress address;
                if (IPAddress.TryParse(ip, out address))
                {
                    if (_con.ConnectByEthernetmodule(ip))
                    {
                        ConnectionStaue = "Connected";
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
                ConnectionStaue = "Disconnected";
            }
            return ConnectionStaue;
        }
        #endregion
    }
}
