/*****************************************************
Author Name:		Sandeep.K
Project Name:		RFID-DMS
Purpose:	        Visitor Verification
Created Date:		09 Nov 2015  
Updated Date:		23 Nov 2015 
******************************************************/
using System;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WAP3
{
    public partial class formSyncMasterData : Form
    {
        #region Signal Strenth
        int strength;
        public void signal()
        {
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                //string signal;
                strength = connection.SignalStrength; //classLog.writeLog("Message @:Wifi Connection:" + connection.ToString() + "-- Strength:" + strength.ToString());
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Wifi Not Available.";
                classLog.writeLog("Error @:Wifi Not Available");
                strength = -99;
            }
        }
        #endregion
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        string sqlConnection1;
        public formSyncMasterData()
        {
            InitializeComponent();
            classLog.writeLog("Message @: formSync Loaded...");
        }

        
        private void btnRequestSync_Click(object sender, EventArgs e)
        {
            signal();
            if (strength < -5 && strength > -95)
            {
                txtSyncStatus.Visible = true;
                try
                {
                    classLog.writeLog("Message @: Sync Intialized...");
                    string CONNSTRING = sqlConnection; 
                    if (cmbSyncData.Text != "----Select----" && cmbSyncData.Text != "" && cmbSyncData.Text != "System.Data.DataRowView")
                    {
                        cmbSyncData.Enabled = false; btnRequestSync.Enabled = false;
                        lblStatus.Text = "";
                        string sql = "";
                         if (cmbSyncData.Text == "User Master")
                        {
                            sql = " Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                        }
                        else if (cmbSyncData.Text == "Loader Master")
                        {
                            sql = " Select LId,LoaderName,LoaderCode from [dbo].[LoaderMst] where [LStatus]='Active'";
                        }
                        else if (cmbSyncData.Text == "Event Master")
                        {
                            sql = "Select [EId],[EventName],[EventCode],[Status],[Remarks],[CreatedBy] from [dbo].[EventMaster] where [Status]='Active'";
                            //classLog.writeLog("Message @:"+ sql.ToString());
                        }
                        else if (cmbSyncData.Text == "Delegate Master")
                        {
                            sql = "SELECT  [DMId],[DelegateName],[Category],[Company],[NoofPeople],[PAName],[PAMobileNo],[VehicleNo],[HostName],[HostMobileNo],[Remarks],[VPCategory],[Location],[TagNo]  FROM [dbo].[view_DelegateDetails] order by [DMId] asc";
                        }
                        else if (cmbSyncData.Text == "Delegate Master-SQL Local")
                        {
                            string DMId = getMaxDMId(); if (DMId.Trim() == "0" || DMId.Trim() == "")
                            sql = "SELECT  [DMId],[DelegateName],[Category],[Company],[NoofPeople],[PAName],[PAMobileNo],[VehicleNo],[HostName],[HostMobileNo],[Remarks],[VPCategory],[Location],[TagNo] FROM [dbo].[view_DelegateDetails] order by [DMId] asc";
                            else sql = "SELECT  [DMId],[DelegateName],[Category],[Company],[NoofPeople],[PAName],[PAMobileNo],[VehicleNo],[HostName],[HostMobileNo],[Remarks],[VPCategory],[Location],[TagNo] FROM [dbo].[view_DelegateDetails] where [DMId]>" + DMId + " order by [DMId] asc";
                            CONNSTRING = sqlConnection1;
                        }
                        //MessageBox.Show(CONNSTRING.ToString());
                        //MessageBox.Show(sql.ToString()); classLog.writeLog("Message @:" + sql.ToString());
                        SqlConnection dbCon = new SqlConnection(CONNSTRING);
                        //try
                        //{
                            dbCon.Open();
                            SqlDataAdapter da = new SqlDataAdapter(sql, dbCon); classLog.writeLog("Message @:" + da.ToString());
                            DataTable dt = new DataTable(); int i = 1;
                            da.Fill(dt); dbCon.Close();

                            if (cmbSyncData.Text == "User Master")
                            {
                                clearlocaldbUserMaster(); classLog.writeLog("Message @:User Master Cleared.");
                            }
                            else if (cmbSyncData.Text == "Loader Master")
                            {
                                clearlocaldbLoaderMaster();
                            }
                            else if (cmbSyncData.Text == "Event Master")
                            {
                                clearlocaldbEventMaster(); classLog.writeLog("Message @:Event Master Cleared.");
                            }
                            else if (cmbSyncData.Text == "Delegate Master")
                            {
                                clearlocaldbDelegateMaster(); classLog.writeLog("Message @:Delegate Master Cleared.");
                            }
                            //................................................
                            string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                            string ndt = "{ts '" + datet + "'}";
                            lblStatus.Text = "Sync In Progress....";
                            string InsertQry = "";
                            try
                            {
                                classLog.writeLog("Message @:" + cmbSyncData.Text + " Syncing Records Count:" + dt.Rows.Count.ToString());
                              
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows.Count != 0)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            txtSyncStatus.Text = cmbSyncData.Text + "Sync:" + dt.Rows.Count.ToString() + "-" + i.ToString();
                                            if (dr != null)
                                            {
                                                lblStatus.Text = "Sync ....";
                                                if (cmbSyncData.Text == "CargoGroup Master")
                                                {
                                                    InsertQry = "INSERT INTO [CargoGroupMst] ([CargoGroupId],[CargoGroupName],[RStatus]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','Active')";
                                                }
                                                else if (cmbSyncData.Text == "Commodity Master")
                                                {
                                                    InsertQry = "INSERT INTO [CommodityMst] ([CommodityId],[CommodityName],[CargoGroupId],[RFIDCode],[RStatus]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','Active')";
                                                }
                                                else if (cmbSyncData.Text == "User Master")
                                                {
                                                    InsertQry = "INSERT INTO [UserMst] ([Id],[LoginId],[Password],[LoginType],[UserName],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','Active')";
                                                }
                                                else if (cmbSyncData.Text == "Loader Master")
                                                {
                                                    InsertQry = "INSERT INTO [LoaderMst] ([Id],[LoaderName],[LoaderCode],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','Active')";
                                                }
                                                else if (cmbSyncData.Text == "Event Master")
                                                {
                                                    InsertQry = "INSERT INTO [EventMaster] ([EId],[EventName],[EventCode],[Status],[Remarks],[CreatedBy],[CreatedTime]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "'," + ndt + ")";
                                                }
                                                else if (cmbSyncData.Text == "Delegate Master" || cmbSyncData.Text == "Delegate Master-SQL Local")
                                                {
                                                    InsertQry = "INSERT INTO [DelegateMaster] ([DMId],[DelegateName],[Category],[Company],[NoofPeople],[PAName],[PAMobileNo],[VehicleNo],[HostName],[HostMobileNo],[Remarks],[VPCategory],[EventLocationsAllowed],[CreatedBy],[CreatedTime],[TagNo]) Values (" + dr[0].ToString() + ",'" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','" + dr[5].ToString() + "','" + dr[6].ToString() + "','" + dr[7].ToString() + "','" + dr[8].ToString() + "','" + dr[9].ToString() + "','" + dr[10].ToString() + "','" + dr[11].ToString() + "','" + dr[12].ToString() + "','" + classLogin.User + "'," + ndt + ",'" + dr[13].ToString() + "')";
                                                }
                                                string CONN_STRING = localConnection;
                                                SqlCeConnection Con = new SqlCeConnection(CONN_STRING);
                                                Con.Open();
                                                SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                                cmd.ExecuteNonQuery();
                                                Con.Close();
                                                i++;
                                                //lblStatus.Text = "Sync In Progress....";
                                                //System.Threading.Thread.Sleep(5000);
                                            }
                                        }
                                    }
                                    else lblStatus.Text = "No Data to Sync.";
                                }
                                else
                                {
                                    throw new Exception("No row for insertion");
                                    lblStatus.Text = "No Data to Sync.";
                                }
                                dt.Dispose();
                                lblStatus.Text = "Sync Complete.";
                                classLog.writeLog("Message @:" + cmbSyncData.Text + " Records Count:" + dt.Rows.Count.ToString() + "Sync Completed.");
                            }

                            catch (Exception ex)
                            {
                                dt.Dispose(); classLog.writeLog("Error @:Sync Master :" + ex.ToString());
                                //throw new Exception("Please attach file in Proper format.");
                            }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.ToString());
                        //}

                    }

                    cmbSyncData.Enabled = true; btnRequestSync.Enabled = true;

                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Wifi Not Available.";
                    classLog.writeLog("Error @:Sync Master :" + ex.ToString());
                } txtSyncStatus.Visible = false;
            }
            else
            {
                lblStatus.Text = "Wifi Not Available.";
            }
        }
        public void clearlocaldbCargoGroupMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM CargoGroupMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void clearlocaldbCommodityMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM CommodityMst";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
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

        public void clearlocaldbEventMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM EventMaster";
            SqlCeCommand cm = new SqlCeCommand(Query, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public void clearlocaldbDelegateMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM DelegateMaster";
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

        private void formSyncMasterData_Load(object sender, EventArgs e)
        {
            cmbSyncData.Text = "----Select----";
            sqlConnection1 = getDBConfigdetails();
        }
        public string getDBConfigdetails()//getting the Local SQL DB Config details from local database...
        {
            string ServerConnection = "";
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select DataSource,InitialCatalog,UserId,Password  from DBConnection where Status='Active'";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ServerConnection = "Data Source=" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + " ;Initial Catalog=" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + ";User ID=" + ds.Tables[0].Rows[0].ItemArray[2].ToString() + ";Password=" + ds.Tables[0].Rows[0].ItemArray[3].ToString() + ";";
                dbCon.Close();
                classLog.writeLog("Message @: SQL1 Connection: " + ServerConnection.ToString());

            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
            return ServerConnection;
        }
        public string getMaxDMId()//getting the Max DMId details from local database...
        {
            string DMId = "0";
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select Max(DMId)from [DelegateMaster]";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DMId = ds.Tables[0].Rows[0].ItemArray[0].ToString(); classLog.writeLog("Message @: Max Delegate Detail Id: "+DMId.ToString());
                dbCon.Close();
            }
            catch
            {
                MessageBox.Show("Delegate Details Not found.");
                classLog.writeLog("Error @: Delegate Details Not found.");
            }
            return DMId;
        }
    }
}