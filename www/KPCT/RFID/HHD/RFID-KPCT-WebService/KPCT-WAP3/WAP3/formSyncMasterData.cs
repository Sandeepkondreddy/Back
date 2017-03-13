using System;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KPCTSDS.appserver;
using System.Net;
namespace WAP3
{
    public partial class formSyncMasterData : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string DatabaseVarient = classServerDetails.DBType;
        public formSyncMasterData()
        {
            InitializeComponent();
        }

        
        private void btnRequestSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncData.Text != "----Select----" && cmbSyncData.Text != "" && cmbSyncData.Text != "Location Master" && cmbSyncData.Text != "System.Data.DataRowView")
                {
                    cmbSyncData.Enabled = false; btnRequestSync.Enabled = false;
                    lblStatus.Text = "";
                    string getdetails = cmbSyncData.Text.ToString();
                    if (getdetails == "Location Master")
                    {
                        getdetails = "Location Master" + "-" + classLogin.Location;
                    }
                    classLog.writeLog("Message @: Get Master Details for:" + getdetails.ToString());
                    DataTable dt = getDatafromWebservice(getdetails);
                    //................................................

                    lblStatus.Text = "Sync In Progress....";
                    string InsertQry = "";
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (cmbSyncData.Text == "User Master")
                            {
                                clearlocaldbUserMaster();
                            }
                            else if (cmbSyncData.Text == "Reason Master")
                            {
                                clearlocaldbReasonMaster();
                            }
                            else if (cmbSyncData.Text == "Loader Master")
                            {
                                clearlocaldbLoaderMaster();
                            }
                            else if (cmbSyncData.Text == "Location Master")
                            {
                                clearlocaldbLocationMaster();
                            }
                            else if (cmbSyncData.Text == "Cargo Condition Master")
                            {
                                clearlocaldbCargoConditionMaster();
                            }
                            else if (cmbSyncData.Text == "OPHandled By Master")
                            {
                                clearlocaldbOpHandledByMaster();
                            }
                            else if (cmbSyncData.Text == "Weather Condition Master")
                            {
                                clearlocaldbWeatherConditionMaster();
                            }
                            classLog.writeLog("Message @: Local Details Cleared for:" + getdetails.ToString());
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr != null)
                                {
                                    lblStatus.Text = "Sync ....";
                                    if (cmbSyncData.Text == "User Master")
                                    {
                                        InsertQry = "INSERT INTO [UserMst] ([Id],[LoginId],[Password],[LoginType],[UserName],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','Active')";
                                    }
                                    if (cmbSyncData.Text == "Reason Master")
                                    {
                                        InsertQry = "INSERT INTO [ReasonMst] ([Id],[ReasonName],[Description]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[1].ToString() + "')";
                                    }
                                    if (cmbSyncData.Text == "Loader Master")
                                    {
                                        InsertQry = "INSERT INTO [LoaderMst] ([LId],[LoaderName],[LoaderCode],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','Active')";
                                    }
                                    if (cmbSyncData.Text == "Location Master")
                                    {
                                        InsertQry = "INSERT INTO [LocationMst] ([LId],[LocationId],[LocationName]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[1].ToString() + "')";
                                    }
                                    else if (cmbSyncData.Text == "Cargo Condition Master")
                                    {
                                        InsertQry = "INSERT INTO [CargoConditionMst] ([Id],[Name],[Description]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "')";
                                    }
                                    else if (cmbSyncData.Text == "OPHandled By Master")
                                    {
                                        InsertQry = "INSERT INTO [OpHandledByMst] ([Id],[Name],[Description]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "')";
                                    }
                                    else if (cmbSyncData.Text == "Weather Condition Master")
                                    {
                                        InsertQry = "INSERT INTO [WeatherConditionMst] ([Id],[Name],[Description]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "')";
                                    }
                                    string CONN_STRING = localConnection;
                                    SqlCeConnection Con = new SqlCeConnection(CONN_STRING);
                                    Con.Open();
                                    SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                    cmd.ExecuteNonQuery();
                                    Con.Close();
                                    lblStatus.Text = "Sync In Progress....";
                                }
                            }

                        }
                        else
                        {
                            throw new Exception("No row for insertion");
                        }
                        dt.Dispose();
                        lblStatus.Text = "Sync Complete.";
                    }

                    catch (Exception ex)
                    {
                        dt.Dispose();
                        throw new Exception("Please attach file in Proper format.");
                    }
                }
                else
                {
                    lblStatus.Text = "Please Select Proper details.";
                }
                cmbSyncData.Enabled = true; btnRequestSync.Enabled = true;
            }
            catch
            {
                lblStatus.Text = "Wifi Not Available.";
            }
        }
       

        public void clearlocaldbUserMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM UserMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbReasonMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM ReasonMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbLoaderMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM LoaderMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
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
        public void clearlocaldbWeatherConditionMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM WeatherConditionMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbOpHandledByMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM OpHandledByMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbCargoConditionMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM CargoConditionMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        private void formSyncMasterData_Closing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new formConfig();
            config.Show();
            this.Hide();
        }

        private void cmbSyncData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        #region HHD Configuaration Transaction useing WebService
        private DataTable getDatafromWebservice(string getdetails)
        {
            DataTable dt=new DataTable();
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
                        if(DatabaseVarient=="SQL")
                        dt = details.GetMasterDetailsSQL(getdetails);
                        else if(DatabaseVarient=="Oracle")
                            dt = details.GetMasterDetailsOracle(getdetails);
                        if (dt.Rows.Count == 0)
                        {
                            lblStatus.Text = "Details Not found.";
                            classLog.writeLog("Message @: Master Details Not found.");
                        }
                        else
                        {
                            classLog.writeLog("Message @:Master Received from DB.");
                        }
                        details.Dispose();
                    }
                    else
                    {
                        lblStatus.Text = "Network Not Connected..";
                        classLog.writeLog("Error @:Network Not Connected..");
                    }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "NetAvail2");
                lblStatus.Text = "Connection Failed.";
                classLog.writeLog("Error @:Web Service Not Connected."); classLog.writeLog("Error @: " + ex.ToString());
            }
            return dt;
        }
        #endregion

        private void formSyncMasterData_Load(object sender, EventArgs e)
        {
            DatabaseVarient = classServerDetails.DBType;
        }

    }
}