/*********************************
Author Name:		Sandeep.K
Project Name:		RFID-DMS
Purpose:	        Form Config
Created Date:		09 Nov 2015 
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

namespace WAP3
{
    public partial class formConfig : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string sqlGPRSConnection = classServerDetails.GPRSSQLConnection;
        string User = classLogin.User;
        int strength;
        string ReaderId;
        public formConfig()
        {
            InitializeComponent();
        }

        
        public void getReaderOperation()
        {
            string connectionString = localConnection;
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();
                string cmd = "select ReaderOperation,ConnectionType from ReaderConfig order by ID desc";
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, connectionString);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string OpType = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                string ConnType = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                classLogin.OpType = OpType;
                classLogin.ConnType = ConnType;
            }
        }
        string Optype;
        private void btnRequestConfig_Click(object sender, EventArgs e)
        {
            btnRequestConfig.Enabled = false;

            if (radioVisitor.Checked == false && radioUnRegistration.Checked == false && radioVerification.Checked == false && radioYard.Checked == false)
                    {
                        MessageBox.Show("Please Select Reg or UnReg or Verify.");
                    }

                    else
                    {
                        if (cmbLocation.Text.ToString() == "CT Gate" || cmbLocation.Text.ToString() == "Warehouse")
                        {
                            if (cmbConnectionType.Text == "Wifi")
                            {
                                signal();
                                if (strength < -20 && strength > -85)
                                {
                                    string CONN_STRING = sqlConnection;
                                    SqlConnection dbCon = new SqlConnection(CONN_STRING);
                                    try
                                    {
                                        dbCon.Open();
                                        getConfigdatafromSQLdb();
                                        clearlocaldb();
                                        saveCofigdetailsinsdfdb();
                                        getReaderOperation();
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show("Wifi Not Available");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Wifi Not Available.");
                                }
                        }
                            else if (cmbConnectionType.Text == "GPRS")
                            {
                                string CONN_STRING = sqlGPRSConnection;
                                SqlConnection dbCon = new SqlConnection(CONN_STRING);
                                try
                                {
                                    dbCon.Open();
                                    getConfigdatafromSQLdb();
                                    clearlocaldb();
                                    saveCofigdetailsinsdfdb();
                                    getReaderOperation();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show("GPRS Not Available");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select Connection Type.");
                                cmbConnectionType.Focus();
                            }
                        
                    }
                        else
                        {
                            MessageBox.Show("Select Location.");
                            cmbLocation.Focus();
                        }
                }

            btnRequestConfig.Enabled = true;
        }
        int TaskConfigId;
        string TaskConfID;
        
        public void getConfigdatafromSQLdb()//getting the details from Reader Config details from SQL database...
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string readerip = classLogin.ReaderIP;
            //Connection to RFID SQLSERVER database
            string CONN_STRING = sqlConnection;
            if (cmbConnectionType.Text == "Wifi")
            {
                CONN_STRING = sqlConnection;
            }
            else if (cmbConnectionType.Text == "GPRS")
            {
                CONN_STRING = sqlGPRSConnection;
            }
            
                SqlConnection dbCon = new SqlConnection(CONN_STRING);


                string cmd = "select Readerno,ReaderName,Readermacid,ServerIP,Serverport,Task_ID,Config_ID,ReaderId  from [dbo].[ReaderMaster] where ReaderIP='" + readerip + "' and RStatus='A'";

                    try
                    {
                        dbCon.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd, dbCon);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        txtReaderNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        txtReaderName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        txtMacAdd.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        txtServerIP.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                        txtServerPort.Text= ds.Tables[0].Rows[0].ItemArray[4].ToString();
                        if (ds.Tables[0].Rows[0].ItemArray[5].ToString() == "")
                        {
                            txtProcess.Text = "External";
                        }
                        else
                        {
                            txtProcess.Text = "Internal";
                            txtTaskId.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                            TaskConfigId = Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray[6].ToString());
                            TaskConfID = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                        }
                        txtReaderIP.Text = classLogin.ReaderIP;
                        ReaderId = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                        
                       
                }
                catch
                {
                    MessageBox.Show("Config Details Not found.");
                }
        }
        public void saveCofigdetailsinsdfdb()//saveing the configuration details in sdf db..
        {
            //Save Record in Handle sqlce database (sdf)
            string connectionString = "Data Source=" +
           (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
           "\\localdb.sdf;Persist Security info=False";


            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {

                if (radioVisitor.Checked == true) { Optype = "Visitor"; }
                else if (radioUnRegistration.Checked == true) { Optype = "UnReg"; }
                else if (radioVerification.Checked == true) { Optype = "Verify"; }
                else if (radioYard.Checked == true) { Optype = "Yard"; }
                string readerip = txtReaderIP.Text.ToString();
                string readerno = txtReaderNo.Text.ToString();
                string readername = txtReaderName.Text.ToString();
                string macadd = txtMacAdd.Text.ToString();
                string serverip = txtServerIP.Text.ToString();
                string serverport = txtServerPort.Text.ToString();
                string taskcode = txtTaskId.Text.ToString();
                string process = txtProcess.Text.ToString();
                string conntype = cmbConnectionType.Text.ToString();
                try
                {
                    connection.Open();
                    string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    string ndt = "{ts '" + datet + "'}";
                    SqlCeCommand cmd = new SqlCeCommand("Insert into ReaderConfig (ReaderNo,ReaderName,ReaderIP,ReaderMacAdd,ServerIP,ServerPort,ConfigDateTime,Status,ReaderOperation,TaskCode,TaskConfigID,ProcessCode,Location,ConnectionType,ReaderId)" + " Values (" + "'" + readerno + "'" + "," + "'" + readername + "'" + "," + "'" + readerip + "'" + "," + "'" + macadd + "'" + "," + "'" + serverip + "'" + "," + "'" + serverport + "'" + "," + ndt + ",'A','" + Optype + "','" + taskcode + "','" + TaskConfID + "','" + process + "','" + cmbLocation.Text.ToString() + "','" + conntype + "','" + ReaderId + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    //saveCofigdetailsinsdfdb();
                    MessageBox.Show("Configuration Success...");

                }
                catch
                {
                    MessageBox.Show("Internaldb details notfound.");
                    MessageBox.Show("Configuration Faild...");
                }
            }
        }
        public void getConfigdatafromsdfdb()//getting the details from Reader Config details from local database...
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string readerip = classLogin.ReaderIP;
            //Connection to RFID SQLSERVER database
            string CONN_STRING = localConnection;
            
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);


            string cmd = "select ReaderNo,ReaderName,ReaderIP,ReaderMacAdd,ServerIP,ServerPort,ReaderOperation,TaskCode,ProcessCode,Location,ConnectionType,ReaderId  from ReaderConfig where Status='A'";

            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                txtReaderNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txtReaderName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                txtReaderIP.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                txtMacAdd.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                txtServerIP.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                txtServerPort.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                string readerop = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                txtTaskId.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                txtProcess.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                cmbLocation.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                cmbConnectionType.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                dbCon.Close();
                if (readerop == "Reg")
                    radioVisitor.Checked = true;
                else if (readerop == "UnReg")
                    radioUnRegistration.Checked = true;
                else if (readerop == "Verify")
                    radioVerification.Checked = true;
                else if (readerop == "Yard")
                    radioYard.Checked = true;
            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }

        private void formConfig_Closing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void formConfig_Load(object sender, EventArgs e)
        {
            getConfigdatafromsdfdb();
            cmbConnectionType.Text = "Wifi"; radioVisitor.Checked = true;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new formConnection();
            config.Show();
            this.Hide();
        }
        public void clearlocaldb()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM ReaderConfig";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void signal()
        {
            try
            {
                //PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                //string signal;
                //strength =connection.SignalStrength;
                strength = -30;
                if (strength < -20 && strength > -85)
                {
                    sqlConnection = classServerDetails.WifiSQLConnection;
                }
                else
                {
                    sqlConnection = classServerDetails.GPRSSQLConnection;
                }
            }
            catch
            {
                MessageBox.Show("Wifi Not Available");
            }
            //_lbReadInfoReadWrite.Text = connection.SignalStrength.ToString();
        }

        private void btnSyncTrucks_Click(object sender, EventArgs e)
        {
           // get the details from SQL database
            signal();
            if (strength < -20 && strength > -75)
            {
                
                string CONN_STRING = sqlConnection;
                SqlConnection dbCon = new SqlConnection(CONN_STRING);
                try
                {
                    dbCon.Open();
                    dbCon.Close();
                    synctrucksfromSQLDBtoLocaldb();
                }
                catch
                {
                    //_lbReadInfoReadWrite.Text = "Server cannot be connected.";
                }
               
            }
            else
            {
               // lblLocalDBstatus.Text = "Signal Strength is Low.";
            }
            //insert the details into offline db
        }
        int Rid;
        public void synctrucksfromSQLDBtoLocaldb()
        {
            string connectionString = sqlConnection;
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "select count(*) from [dbo].[Tag_TruckAllocation]  where RStatus='Active'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            int flg;
            flg = int.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();
            if (flg == 0)
            {
                MessageBox.Show("No data in the SQLDB to sync.");
            }
            else
            {
                
                    cn.Open();
                    string c = "Select min(Alltid) from [dbo].[Tag_TruckAllocation]";
                    SqlCommand c1 = new SqlCommand(c, cn);

                    Rid = int.Parse(c1.ExecuteScalar().ToString());
                    for (int i = 0; i < flg; i++)
                    {
                        string cmd1 = "select a.[Tkid],a.[Tagid],b.[TruckNo],c.[TagNo],a.[Alltid],a.[RStatus] from [dbo].[Tag_TruckAllocation] a, [dbo].[TruckMaster] b,[dbo].[TagMaster] c  where  a.[Alltid]=" + Rid + " and a.[Tkid]=b.[Tkid] and a.[Tagid]=c.[Tagid]";
                    SqlDataAdapter da = new SqlDataAdapter(cmd1, cn);
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        string Tkid = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                        string Tagid = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                        string truckno = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                        string tagno = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                        string id = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                        //Connection to RFID SQLSERVER database
                        string CONN_STRING = localConnection;
                        SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
                        try
                        {
                            dbCon.Open();
                            string Query = "Insert into TruckMaster ([ID],[Tkid],[TruckNo],[Tagid],[TagNo]) Values(" + id + ",'" + Tkid + "'," + "'" + truckno + "'" + ",'" + Tagid + "','" + tagno + "')";
                            SqlCeCommand cm = new SqlCeCommand(Query, dbCon);
                            cm.ExecuteNonQuery();
                           // updateTransctionstatusinTagRegTbl(tagno);
                            dbCon.Close();
                            //clearlocaldb();
                        }
                        catch
                        {
                            MessageBox.Show("Database Disconnected.");
                        }
                        //count--;
                    }
                    catch
                    {
                        MessageBox.Show("Details not found in the Database.");
                    }
                    cn.Close();
                    Rid++;
                }
                MessageBox.Show("Sync Complete.");
                //MessageBox.Show("Successfully Saved..in Online.");
                //_lbReadInfoReadWrite.Text = "Sync Complete";
            }
        }

        private void menuItem2_Click_1(object sender, EventArgs e)
        {
            Form config = new formSyncMasterData();
            config.Show();
            this.Hide();
        }

        private void menuItemSQLConnfig_Click(object sender, EventArgs e)
        {
            Form config = new KPCLVisitor.formSQL_DBConnection();
            config.Show();
            this.Hide();
        }
    }
}