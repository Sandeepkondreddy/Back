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
    public partial class formTaskConfig : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string sqlGPRSConnection = classServerDetails.GPRSSQLConnection;
        string User = classLogin.User;
        int strength;
        public formTaskConfig()
        {
            InitializeComponent();
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
        private void btnRequestConfig_Click(object sender, EventArgs e)
        {
            btnRequestConfig.Enabled = false;
            signal();
            if (strength < -20 && strength > -85)
            {
                string CONN_STRING = sqlConnection;
                SqlConnection dbCon = new SqlConnection(CONN_STRING);
                try
                {
                    dbCon.Open();
                    getTaskConfigdatafromSQLdb();
                    //clearlocaldb();
                    saveTaskCofigdetailsinsdfdb();
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Wifi Not Available");
                }
            }

            btnRequestConfig.Enabled = true;
        }


        int TaskConfigId;
        string TaskConfID;
        public void getTaskConfigdatafromSQLdb()//getting the details from Reader Config details from SQL database...
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string readerip = classLogin.ReaderIP;
            //Connection to RFID SQLSERVER database
            string CONN_STRING = sqlConnection;
            
            SqlConnection dbCon = new SqlConnection(CONN_STRING);


            string cmd = "select a.TaskCode,a.OperationType,a.RefCode,b.SubTaskCode,b.SubRefCode,b.Source,b.Destination,b.Commodity  from [dbo].[Task_Tbl] a,[dbo].[SubTask_Tbl] b where a.[TaskCode]=" + txtTaskNo.Text.Trim() + " and b.[TaskCode]=a.[TaskCode] and  a.[TStatus]='A' and b.[STStatus]='Active'";//c.[ReaderIP]='" + readerip + "' and

            try
            {
                dbCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                txtTaskNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txtTaskNo.ReadOnly = true;
                txtOperationType.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                txtRefCode.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                txtSubTaskNo.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                txtSubRefCode.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                txtSource.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                txtDestination.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                txtCommodity.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();

            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }
        public void saveTaskCofigdetailsinsdfdb()//saveing the configuration details in sdf db..
        {
            //Save Record in Handle sqlce database (sdf)
            string connectionString = "Data Source=" +
           (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
           "\\localdb.sdf;Persist Security info=False";


            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {

                //if (radioRegistration.Checked == true) { Optype = "Reg"; }
                //else if (radioUnRegistration.Checked == true) { Optype = "UnReg"; }
                //else if (radioVerification.Checked == true) { Optype = "Verify"; }
                //else if (radioYard.Checked == true) { Optype = "Yard"; }
                //string readerip = txtReaderIP.Text.ToString();
                //string readerno = txtReaderNo.Text.ToString();
                //string readername = txtReaderName.Text.ToString();
                //string macadd = txtMacAdd.Text.ToString();
                //string serverip = txtServerIP.Text.ToString();
                //string serverport = txtServerPort.Text.ToString();
                //string taskcode = txtTaskId.Text.ToString();
                //string process = txtProcess.Text.ToString();
                //string conntype = cmbConnectionType.Text.ToString();
                //try
                //{
                //    connection.Open();
                //    string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                //    string ndt = "{ts '" + datet + "'}";
                //    SqlCeCommand cmd = new SqlCeCommand("Insert into ReaderConfig (ReaderNo,ReaderName,ReaderIP,ReaderMacAdd,ServerIP,ServerPort,ConfigDateTime,Status,ReaderOperation,TaskCode,TaskConfigID,ProcessCode,Location,ConnectionType)" + " Values (" + "'" + readerno + "'" + "," + "'" + readername + "'" + "," + "'" + readerip + "'" + "," + "'" + macadd + "'" + "," + "'" + serverip + "'" + "," + "'" + serverport + "'" + "," + ndt + ",'A','" + Optype + "','" + taskcode + "','" + TaskConfID + "','" + process + "','" + cmbLocation.Text.ToString() + "','" + conntype + "')", connection);
                //    cmd.ExecuteNonQuery();
                //    connection.Close();
                //    //saveCofigdetailsinsdfdb();
                //    MessageBox.Show("Configuration Success...");

                //}
                //catch
                //{
                //    MessageBox.Show("Internaldb details notfound.");
                //    MessageBox.Show("Configuration Faild...");
                //}
            }
        }
       
    }
}