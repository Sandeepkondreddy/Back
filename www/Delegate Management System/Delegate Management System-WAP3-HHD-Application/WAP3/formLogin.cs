/*********************************
Author Name:		Sandeep.K
Project Name:		RFID
Purpose:	        Login Screen
Created Date:		09 Nov 2015 
Updated Date:       23 Nov 2015
******************************************************/
using System;
using System.Data.SqlServerCe;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Xml;
using System.Reflection;
using System.Runtime.InteropServices;
using UpdateCEMobileLocalTime;
namespace WAP3
{

    public partial class formLogin : Form
    {
        string sqlConnection = classServerDetails.WifiSQLConnection;
        SqlConnection sqldbCon;
        int strength;

           public class IP_ADAPTER_INFO : SelfMarshalledStruct
        {
            #region Constructors
            public IP_ADAPTER_INFO()
                : base(640)
            {
            }
            public IP_ADAPTER_INFO(byte[] data, int offset)
                : base(data, offset)
            {
            }


            public IP_ADAPTER_INFO(IntPtr pData, int offset)
                : base(640)
            {
                Marshal.Copy(new IntPtr(pData.ToInt32() + offset), data, 0, 640);
            }
            #endregion

            #region Properties
            public IP_ADAPTER_INFO Next
            {
                get
                {
                    if (this.GetInt32(0) == 0)
                        return null;
                    return new IP_ADAPTER_INFO(new IntPtr(this.GetInt32(0)), 0);
                }
            }
            public uint ComboIndex
            {
                get { return GetUInt32(4); }
                set { Set(typeof(uint), 4, value); }
            }
            public string AdapterName
            {
                get { return GetStringAscii(8, 268 - 8); }
                set { Set(typeof(string), 8, value); }
            }
            public string Description
            {
                get { return GetStringAscii(268, 400 - 268); }
                set { Set(typeof(string), 268, value); }
            }
            public uint AddressLength
            {
                get { return GetUInt32(400); }
                set { Set(typeof(uint), 400, value); }
            }
            public byte[] Address
            {
                get { return GetSlice(404, (int)AddressLength); }
            }
            public uint Index
            {
                get { return GetUInt32(412); }
                set { Set(typeof(uint), 412, value); }
            }
            public uint Type
            {
                get { return GetUInt32(416); }
                set { Set(typeof(uint), 416, value); }
            }
            public uint DhcpEnabled
            {
                get { return GetUInt32(420); }
                set { Set(typeof(uint), 420, value); }
            }
            public IP_ADDR_STRING CurrentIpAddress
            {
                get
                {
                    IntPtr p = new IntPtr(GetInt32(424));
                    if (p == IntPtr.Zero)
                        return null;
                    return new IP_ADDR_STRING(p, 0);
                }
            }
            public IP_ADDR_STRING IpAddressList
            {
                get
                {
                    return new IP_ADDR_STRING(data, 428);
                }
            }
            public IP_ADDR_STRING GatewayList
            {
                get
                {
                    return new IP_ADDR_STRING(data, 468);
                }
            }
            public IP_ADDR_STRING DhcpServer
            {
                get
                {
                    return new IP_ADDR_STRING(data, 508);
                }
            }
            public int HaveWins
            {
                get { return GetInt32(548); }
                set { Set(typeof(int), 548, value); }
            }
            public IP_ADDR_STRING PrimaryWinsServer
            {
                get
                {
                    return new IP_ADDR_STRING(data, 552);
                }
            }
            public IP_ADDR_STRING SecondaryWinsServer
            {
                get
                {
                    return new IP_ADDR_STRING(data, 592);
                }
            }
            public uint LeaseObtained
            {
                get { return GetUInt32(632); }
                set { Set(typeof(uint), 632, value); }
            }
            public uint LeaseExpires
            {
                get { return GetUInt32(636); }
                set { Set(typeof(uint), 636, value); }
            }
            #endregion
        }

        /// <summary>
        /// Class IP_ADDR_STRING 
        /// Description:
        /// Implementation of custom marshaller for IPHLPAPI IP_ADDR_STRING
        /// </summary>
        public class IP_ADDR_STRING : SelfMarshalledStruct
        {
            #region Contructors
            public IP_ADDR_STRING()
                : base(40)
            {
            }
            public IP_ADDR_STRING(byte[] data, int offset)
                : base(data, offset)
            {
            }
            public IP_ADDR_STRING(IntPtr pData, int offset)
                : base(40)
            {
                Marshal.Copy(new IntPtr(pData.ToInt32() + offset), data, 0, 40);
            }
            #endregion

            #region Properties
            public IP_ADDR_STRING Next
            {
                get
                {
                    if (this.GetInt32(0) == 0)
                        return null;
                    return new IP_ADDR_STRING(new IntPtr(GetInt32(0)), 0);
                }
            }
            public IP_ADDRESS_STRING IpAddress
            {
                get { return new IP_ADDRESS_STRING(data, baseOffset + 4); }

            }
            public IP_ADDRESS_STRING IpMask
            {
                get { return new IP_ADDRESS_STRING(data, baseOffset + 20); }
            }
            public uint Context
            {
                get { return GetUInt32(36); }
                set { Set(typeof(uint), 36, value); }
            }
            #endregion
        }

        /// <summary>
        /// Class IP_ADDRESS_STRING 
        /// Description:
        /// Implementation of custom marshaller for IPHLPAPI IP_ADDRESS_STRING
        /// </summary>
        public class IP_ADDRESS_STRING : SelfMarshalledStruct
        {
            #region Contructors
            public IP_ADDRESS_STRING()
                : base(16)
            {
            }
            public IP_ADDRESS_STRING(byte[] data, int offset)
                : base(data, offset)
            {
            }
            public IP_ADDRESS_STRING(IntPtr pData, int offset)
                : base(16)
            {
                Marshal.Copy(new IntPtr(pData.ToInt32() + offset), data, 0, 16);
            }
            #endregion

            #region Properties
            public string String
            {
                get { return GetStringAscii(0, 15); }
                set { Set(typeof(string), 0, value); }
            }
            #endregion
        }

        #region P/Invoke definitions

        [DllImport("iphlpapi")]
        extern public static int GetAdaptersInfo(IntPtr p, ref int cb);

        [DllImport("coredll")]
        extern public static IntPtr LocalAlloc(int flags, int cb);

        [DllImport("coredll")]
        extern public static IntPtr LocalFree(IntPtr p);

        #endregion
       
        string readerid;
        //public void getIPAdd() //Getting the Reader IP address
        //{
        //    string myHost = System.Net.Dns.GetHostName();
        //    //MessageBox.Show(myHost);
        //    string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();

        //    //MessageBox.Show(myIP);
        //    lblReaderIPval.Text = myIP;
        //    //GetMACAddress();
        //}
        string localConnection = classServerDetails.SdfConnection;

        public formLogin()
        {
            InitializeComponent();
            getserverip();
            classLog.writeLog("Message @:Application Initiated");
        }

        public void getserverip()
        {

        }
        string id;
        public void getReaderOperation()
        {
            string connectionString = localConnection;
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                string cmd = "select ReaderOperation,ID,ConnectionType,ReaderIP from ReaderConfig order by ID desc";
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, connectionString);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string OpType = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                id = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                classLogin.OpType = OpType;
                classLogin.ConnType = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                if(classLogin.ReaderIP=="0.0.0.0"||classLogin.ReaderIP=="")
                classLogin.ReaderIP = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                if (classLogin.ReaderIP == "000.000.000.000") classLog.writeLog("Warning @:Reader Configuaration Pending.");
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            btnLogin.Enabled = false; syncDateTime();
            if (txtLoginId.Text == "")
            {
                MessageBox.Show("Please Enter Login Id.");
                classLog.writeLog("Warning @:Please Enter Login Id.");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter Login Password.");
                classLog.writeLog("Warning @:Please Enter Login Password.");
            }
            else
            {
                string LoginId = txtLoginId.Text.ToString().Trim();
                string Password = txtPassword.Text.ToString().Trim();
                //string connectionString = "Data Source=" +
                //(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
                //"\\localdb.sdf;Persist Security info=False";
                string connectionString = localConnection;
                using (SqlCeConnection connection = new SqlCeConnection(connectionString))
                {
                    connection.Open();
                    string cmd = "select LoginId,LoginType from UserMst where LoginId='" + LoginId + "' and Password='" + Password + "'";
                    SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, connectionString);
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        string usr = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string usrtype = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        classLogin.User = usr;
                        classLogin.UserType = usrtype;
                        getReaderOperation();
                        if (usr.ToString().Trim() == LoginId.ToString().Trim())
                        {
                            
                            classLogin.User = usr; 
                            if (usrtype.ToString().Trim() == "admin")
                            {
                                Form config = new formConfig();
                                config.Show();
                                this.Hide();
                            }
                            else if (usrtype.ToString().Trim() == "user")
                            {
                                if (id!="2")
                                {
                                    formConnection config = new formConnection();
                                    config.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Please Configure first.");
                                    classLog.writeLog("Message @:Please Configure first.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("User Details Not Updated Properly.");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Login Details Invalid");
                            classLog.writeLog("Error @:Login Details Invalid");
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Login Details Invalid...");
                        classLog.writeLog("Error @:Login Details Invalid...");
                    }
                }

            }
            btnLogin.Enabled = true;
        }
        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
                c.Enabled = false;



            int cb = 0;
            int ret = GetAdaptersInfo(IntPtr.Zero, ref cb);
            IntPtr pInfo = LocalAlloc(0x40, cb); //LPTR
            ret = GetAdaptersInfo(pInfo, ref cb);
            if (ret == 0)
            {
                IP_ADAPTER_INFO info = new IP_ADAPTER_INFO(pInfo, 0);
                while (info != null)
                {
                    IP_ADDR_STRING st = info.IpAddressList;
                    //{ info.AdapterName, info.CurrentIpAddress.IpAddress.String, info.CurrentIpAddress.IpMask.String, info.GatewayList.IpAddress.String }));
                    classLogin.ReaderIP = info.CurrentIpAddress.IpAddress.String; 
                    info = info.Next;
                }
            }
            LocalFree(pInfo);

            foreach (Control c in this.Controls)
                c.Enabled = true;
        }
        private void formLogin_Closing(object sender, CancelEventArgs e)
        {
            classLog.writeLog("Message @:Application Closeing.");
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            classLog.writeLog("Message @:Application Logout.");
            Application.Exit();
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            mnuRefresh_Click(null, null);
        }
        int ConfigCount;
        public void checkconfigdetails()
        {
            string connectionString = localConnection;
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                string sql = "select count(*) from ReaderConfig order by ID desc";
                SqlCeCommand cmd = new SqlCeCommand(sql, connection);
                int flg;
                flg = int.Parse(cmd.ExecuteScalar().ToString());
                
                    ConfigCount = flg;
                
            }
        }
        #region Sync Time
        public void signal()
        {
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                strength = connection.SignalStrength;
                if (strength < -10 && strength > -95)
                {
                    sqlConnection = classServerDetails.WifiSQLConnection;
                }
                else
                {  
                    MessageBox.Show("Wifi Not Available/Week Signal");
                    classLog.writeLog("Message @:Wifi Not Available/Week Signal");
                }
            }
            catch
            {
                MessageBox.Show("Wifi Not Available");
            }
        }
             public void syncDateTime()
             {
                    signal();
                    if (strength < -10 && strength > -95)
                    {
                        
                        try
                        {
                            sqldbCon = new SqlConnection(sqlConnection);
                            sqldbCon.Open();
                            sqldbCon.Close();
                            syncTime();
                        }
                        catch{
                            classLog.writeLog("Error @:Time Sync Failed due to Lack of Wifi");
                               }

                    }
             }

        public void syncTime()
        {
            
            string cmd = "select GetDATE()";
            sqldbCon.Open();
            DateTime dts = System.DateTime.Now;
            SqlDataAdapter da = new SqlDataAdapter(cmd, sqldbCon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0]);
            UpdateCEMobileLocalTime.classSyncTime.SetSystemDateTime(dt);
            sqldbCon.Close();
            classLog.writeLog("Message @:Time Sync Completed");
            da.Dispose();
            ds.Dispose();
        }
        #endregion
        #region Key Handling
        private void formLogin_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
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
                this.btnLogin.Focus();
                btnLogin_Click(null, null);
            }

        }

        private void txtLoginId_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyValue.ToString();
            if (e.KeyCode == Keys.F1) { }
            string cd = e.KeyCode.ToString();
            
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                this.btnLogin.Focus();
                btnLogin_Click(null, null);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                this.btnLogin.Focus();
                btnLogin_Click(null, null);
            }
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                this.btnLogin.Focus();
                btnLogin_Click(null, null);
            }
        }

        private void btnExit_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
                this.btnLogin.Focus();
                btnLogin_Click(null, null);
            }
        }
        #endregion
    }
}