using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using Magnetic.MGCDriver.Base;
using Magnetic.MGCDriver.MGCConnector;
using Magnetic.MGCDriver.Utils;

namespace CS_RFID3_Host_Sample1
{
	class classBoomBarriorOperations
	{
        public static cMGCConnector _conSet1 = new cMGCConnector();//Set1
        public static cMGCConnector _conSet2 = new cMGCConnector();//Set2
        #region Boom Barrier Connection
        public static string BoomBarriorConnection(string BoomBarrierIP,cMGCConnector _con)
        {
            string ConnectionStatus = "NotConnected";
            if (!_con.IsConnected)
            {
                string ip = BoomBarrierIP;
                IPAddress address;
                if (IPAddress.TryParse(ip, out address))
                {
                    if (_con.ConnectByEthernetmodule(ip))
                    {
                        ConnectionStatus = "Connected";
                    }    
                }
                else
                {
                    ConnectionStatus="Check IP!";
                }
            }
            else
            {
                _con.Disconnect();
                ConnectionStatus = "Discooected";
            }

            return ConnectionStatus;
        }
        #endregion

        public static string BoomBarriorStstus(string BoomBarrierIP, cMGCConnector _con)
        {
            string Boomstatus = "";
            if (BoomBarriorConnection(BoomBarrierIP,_con) == "Conneted")
            {
                if (_con.IsConnected)
                {
                    uint status = 0;

                    if (_con.ReadModbusRegisterUInt32(0x0001, ref status))
                    {
                        byte[] byteStatus = BitConverter.GetBytes(status);
                        //Update the Status - see [5815,0000 table 3]
                        //laBoomAngle.Text = byteStatus[3].ToString(CultureInfo.InvariantCulture) + "°";
                        if ((byteStatus[2] & 0x80) == 0x80)
                        {
                            Boomstatus = "Boom in position open";
                        }
                        else if ((byteStatus[2] & 0x10) == 0x10)
                        {
                            Boomstatus = "Boom in position closed";
                        }
                        else
                        {
                            Boomstatus = String.Empty;
                        }
                    }
                    string closeConnection = BoomBarriorConnection(BoomBarrierIP,_con);
                }
            }
            else Boomstatus = "BoomBarrier Not Connected";
            return Boomstatus;
        }

        public static string OpenBoomBarriorO(string BoomBarrierIP, cMGCConnector _con)
        {
            string OpenBBStatus = "";
            if (BoomBarriorConnection(BoomBarrierIP,_con) == "Conneted")
            {
                if (_con.IsConnected)
                {
                    _con.WriteModbusRegisterUInt16(0x0000, 0x0001);
                    string closeConnection = BoomBarriorConnection(BoomBarrierIP,_con);
                    OpenBBStatus = "BoomBarrier Opened";
                }
            }
            else OpenBBStatus = "BoomBarrier Not Connected";

            return OpenBBStatus;
        }

        public static string CloseBoomBarriorO(string BoomBarrierIP, cMGCConnector _con)
        {
            string CloseBBStatus = "";
            if (BoomBarriorConnection(BoomBarrierIP,_con) == "Conneted")
            {
                if (_con.IsConnected)
                {
                    _con.WriteModbusRegisterUInt16(0x0000, 0x0002);
                    string closeConnection=BoomBarriorConnection(BoomBarrierIP,_con);
                    CloseBBStatus = "Boom Barrier Closed";
                }
            }
            else CloseBBStatus = "BoomBarrier Not Connected";

            return CloseBBStatus;
        }



        #region Boom Barrier Open
        public static string OpenBoomBarrior(string BoomBarrierIP, cMGCConnector _con)
        {
            string OpenBBStatus = "";
            if (!_con.IsConnected) BoomBarriorConnection(BoomBarrierIP,_con);
                if (_con.IsConnected)
                {
                    _con.WriteModbusRegisterUInt16(0x0000, 0x0001);
                    //string closeConnection = BoomBarriorConnection(BoomBarrierIP, _con);
                    OpenBBStatus = "Open";
                }            
                else OpenBBStatus = "Not Connected";

            return OpenBBStatus;
        }
        #endregion

        #region Boom Barrier Close
        public static string CloseBoomBarrior(string BoomBarrierIP, cMGCConnector _con)
        {
            string CloseBBStatus = "";
                if (_con.IsConnected)
                {
                    _con.WriteModbusRegisterUInt16(0x0000, 0x0002);
                    string closeConnection = BoomBarriorConnection(BoomBarrierIP, _con);
                    CloseBBStatus = "Boom Barrier Closed";
                }
                else CloseBBStatus = "Not Connected";

            return CloseBBStatus;
        }
        #endregion
    }
}
