/*********************************
Author Name:		Sandeep.K
Project Name:		RFID-EPMS
Purpose:	        Form Config
Created Date:		29 June 2016
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
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net;
using KPCTSDS.appserver;

namespace WAP3
{
    public partial class formConfig : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string sqlGPRSConnection = classServerDetails.GPRSSQLConnection;
        string DatabaseVarient = classServerDetails.DBType;
        string User = classLogin.User;
        int strength;
        
        
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
                string cmd = "select ReaderOperation,ConnectionType,Location,ReaderNo from ReaderConfig order by ID desc";
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, connectionString);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string OpType = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                string ConnType = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                classLogin.Location = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                classLogin.ReaderNo = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                classLogin.OpType = OpType;
                classLogin.ConnType = ConnType;
            }
        }
        string Optype;
        private void btnRequestConfig_Click(object sender, EventArgs e)
        {
            btnRequestConfig.Enabled = false;

            if (radioRegistration.Checked == false && radioUnRegistration.Checked == false && radioYard.Checked == false && radioKPCT.Checked == false)
                    {
                        MessageBox.Show("Please Select Reg or UnReg or Yard.");
                    }

                    else
                    {
                                signal();
                                if (strength < -10 && strength > -85)
                                {
                                        getConfigdatafromWebservice();//getConfigdatafromSQLdb();
                                }
                                else
                                {
                                    MessageBox.Show("Wifi Not Available.");
                                }
                }

            btnRequestConfig.Enabled = true;
        }
        //int TaskConfigId;
        //string TaskConfID;
        //public void getConfigdatafromSQLdb()//getting the details from Reader Config details from SQL database...
        //{
        //    string myHost = System.Net.Dns.GetHostName();
        //    string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
        //    string readerip = classLogin.ReaderIP;
        //    //Connection to RFID SQLSERVER database
        //    string CONN_STRING = sqlConnection;
        //    //if (cmbConnectionType.Text == "Wifi")
        //    //{
        //    //    CONN_STRING = sqlConnection;
        //    //}
        //    //else if (cmbConnectionType.Text == "GPRS")
        //    //{
        //    //    CONN_STRING = sqlGPRSConnection;
        //    //}
            
        //        SqlConnection dbCon = new SqlConnection(CONN_STRING);


        //        string cmd = "select Readerno,ReaderName,Readermacid,ServerIP,Serverport,Task_ID,Config_ID  from [dbo].[ReaderMaster] where ReaderIP='" + readerip + "' and RStatus='A'";

        //            try
        //            {
        //                dbCon.Open();
        //                SqlDataAdapter da = new SqlDataAdapter(cmd, dbCon);
        //                DataSet ds = new DataSet();
        //                da.Fill(ds);
        //                txtReaderNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
        //                txtReaderName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
        //                txtMacAdd.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
        //                txtServerIP.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
        //                txtServerPort.Text= ds.Tables[0].Rows[0].ItemArray[4].ToString();
        //                if (ds.Tables[0].Rows[0].ItemArray[5].ToString() == "")
        //                {
        //                    txtProcess.Text = "External";
        //                }
        //                else
        //                {
        //                    txtProcess.Text = "Internal";
        //                    //txtTaskId.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
        //                    TaskConfigId = Convert.ToInt16(ds.Tables[0].Rows[0].ItemArray[6].ToString());
        //                    TaskConfID = ds.Tables[0].Rows[0].ItemArray[6].ToString();
        //                }
        //                txtReaderIP.Text = classLogin.ReaderIP;
                        
                       
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Config Details Not found.");
        //        }
        //}
        public void saveCofigdetailsinsdfdb()//saveing the configuration details in sdf db..
        {
            //Save Record in Handle sqlce database (sdf)
           // string connectionString = "Data Source=" +
           //(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) +
           //"\\localdb.sdf;Persist Security info=False";
            string connectionString = classServerDetails.SdfConnection;

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {

                if (radioRegistration.Checked == true) { Optype = "Reg"; }
                else if (radioUnRegistration.Checked == true) { Optype = "UnReg"; }
                else if (radioYard.Checked == true) { Optype = "Yard"; }
                else if (radioKPCT.Checked == true) { Optype = "KPCT"; }
                string readerip = txtReaderIP.Text.ToString();
                string readerno = txtReaderNo.Text.ToString();
                string readername = txtReaderName.Text.ToString();
                string macadd = txtMacAdd.Text.ToString();
                string serverip = txtServerIP.Text.ToString();
                string serverport = txtServerPort.Text.ToString();
                string taskcode = "";//txtTaskId.Text.ToString();
                string process = txtProcess.Text.ToString();
                string location = cmbLocation.Text.ToString();
                string conntype = "WebService";//cmbConnectionType.Text.ToString();
                try
                {
                    connection.Open();
                    string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    string ndt = "{ts '" + datet + "'}";
                    SqlCeCommand cmd = new SqlCeCommand("Insert into ReaderConfig (ReaderNo,ReaderName,ReaderIP,ReaderMacAdd,ServerIP,ServerPort,ConfigDateTime,Status,ReaderOperation,TaskCode,TaskConfigID,ProcessCode,Location,ConnectionType)" + " Values ('" + readerno + "'," + "'" + readername + "'" + "," + "'" + readerip + "'" + "," + "'" + macadd + "'" + "," + "'" + serverip + "'" + "," + "'" + serverport + "'" + "," + ndt + ",'A','" + Optype + "','" + taskcode + "','KPCT','KPCT Operations','" + location + "','" + conntype + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    //saveCofigdetailsinsdfdb();
                    MessageBox.Show("Configuration Success...");
                    getReaderOperation();
                    SyncLocationMaster();
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


            string cmd = "select ReaderNo,ReaderName,ReaderIP,ReaderMacAdd,ServerIP,ServerPort,ReaderOperation,TaskCode,ProcessCode,Location,ConnectionType  from ReaderConfig where Status='A'";

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
                txtProcess.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                cmbLocation.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                dbCon.Close();
                if (readerop == "Reg")
                    radioRegistration.Checked = true;
                else if (readerop == "UnReg")
                    radioUnRegistration.Checked = true;
                else if (readerop == "Yard")
                    radioYard.Checked = true;
                else if (readerop == "KPCT")
                    radioKPCT.Checked = true;
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
            getConfigdatafromsdfdb(); DatabaseVarient = classServerDetails.DBType;
          //  cmbConnectionType.Text = "Wifi";
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
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                //string signal;
                strength = connection.SignalStrength;
                //strength = -30;
                //if (strength < -20 && strength > -85)
                //{
                   //sqlConnection = classServerDetails.WifiSQLConnection;
                //}
                //else
                //{
                //    sqlConnection = classServerDetails.GPRSSQLConnection;
                //}
                //strength = -30;
            }
            catch
            {
                strength = -99;//MessageBox.Show("Wifi Not Available");
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
        #region HHD Configuaration Transaction useing WebService
        private void getConfigdatafromWebservice()
        {
           
            KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
            try
            {
                    string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    if (ConnectionStatus == "Failed")
                    {
                        ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    }
                    if (ConnectionStatus == "Success")
                    {
                        classLog.writeLog("Message @:Web Service Connected.");
                        classLog.writeLog("Message @:Reader IP: " + classLogin.ReaderIP.ToString());
                        DataSet ds = new DataSet();
                        //classLog.writeLog("Message @:" + DatabaseVarient.ToString());
                        if(DatabaseVarient=="SQL")
                            ds=details.GetReaderDetailsSQL(classLogin.ReaderIP);
                        else if (DatabaseVarient == "Oracle")
                            ds = details.GetReaderDetailsOracle(classLogin.ReaderIP);
                        //classLog.writeLog("Message @: Readerdetails recevied from database." );
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("Config Details Not found.");
                            classLog.writeLog("Message @:Config Details Not found.");
                        }
                        else
                        {
                            txtReaderNo.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                            txtReaderName.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                            txtReaderIP.Text = classLogin.ReaderIP;
                            txtMacAdd.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                            txtServerIP.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                            txtServerPort.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                            //txtProcess.Text = "KPCT Operations";
                            classLog.writeLog("Message @:Details Received from DB.");
                            clearlocaldb();
                            saveCofigdetailsinsdfdb();
                            
                        }
                    }
                    else
                    {

                        MessageBox.Show("Network Not Connected..");
                        classLog.writeLog("Error @:Network Not Connected..");
                    }
                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NetAvail2");
                classLog.writeLog("Error @:Web Service Not Connected."); classLog.writeLog("Error @: " + ex.ToString());
            }
        }
        #endregion

    #region Sync Location Master details from CFMS SQL DB
        public void SyncLocationMaster()
        {
            string getdetails = cmbLocation.Text.ToString();
            if (getdetails == "CT-Yard" || getdetails == "CT-Warehouse")
            {
                classLog.writeLog("Message @: Get Master Details for:" + getdetails.ToString());
                DataTable dt = new DataTable();
                KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                try
                {
                    string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    if (ConnectionStatus == "Failed")
                    {
                        ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    }
                    if (ConnectionStatus == "Success")
                    {
                        classLog.writeLog("Message @:Web Service Connected.");
                        classLog.writeLog("Message @:GetDtails: " + getdetails.ToString());
                        dt = details.GetLocationMasterDetailsCFSSQL(getdetails);
                        if (dt.Rows.Count == 0)
                        {
                            classLog.writeLog("Message @: Master Details Not found.");
                        }
                        else
                        {
                            classLog.writeLog("Message @:Master Received from DB.");
                            clearlocaldbLocationMaster();
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr != null)
                                {

                                    string InsertQry = "INSERT INTO [LocationMst] ([LId],[LocationId],[LocationName]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "')";

                                    string CONN_STRING = localConnection;
                                    SqlCeConnection Con = new SqlCeConnection(CONN_STRING);
                                    Con.Open();
                                    SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                    cmd.ExecuteNonQuery();
                                    Con.Close();

                                }
                            }
                            classLog.writeLog("Message @:Location Mster Sync Complete.");
                        }
                    }
                    else
                    {
                        classLog.writeLog("Error @:Network Not Connected..");
                    }

                }
                catch (Exception ex)
                {
                    classLog.writeLog("Error @:Web Service Not Connected."); classLog.writeLog("Error @: " + ex.ToString());
                }
            }
        }

        public void clearlocaldbLocationMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM LocationMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
    #endregion

    }
}