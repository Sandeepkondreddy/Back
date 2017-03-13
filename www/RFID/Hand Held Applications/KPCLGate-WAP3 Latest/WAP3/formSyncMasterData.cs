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
        string localConnection = classServerDetails.SdfConnection;
        string sqlConnection = classServerDetails.WifiSQLConnection;
        public formSyncMasterData()
        {
            InitializeComponent();
        }

        
        private void btnRequestSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSyncData.Text != "----Select----" && cmbSyncData.Text != "" && cmbSyncData.Text != "System.Data.DataRowView")
                {
                    cmbSyncData.Enabled = false; btnRequestSync.Enabled = false;
                    lblStatus.Text = "";
                    string sql = "";
                    if (cmbSyncData.Text == "CargoGroup Master")
                    {
                        sql = "SELECT CargoGroupId,CargoGroupName FROM [dbo].[CargoGroupMst] where  RStatus='Active'";
                    }
                    else if (cmbSyncData.Text == "Commodity Master")
                    {
                        sql = "SELECT CommodityId,CommodityName,CargoGroupId,RFIDCode FROM [dbo].[CommodityMst] where  RStatus='Active'";
                    }
                    else if (cmbSyncData.Text == "User Master")
                    {
                        sql = " Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                    }
                    else if (cmbSyncData.Text == "Loader Master")
                    {
                        sql = " Select LId,LoaderName,LoaderCode from [dbo].[LoaderMst] where [LStatus]='Active'";
                    }
                    else if (cmbSyncData.Text == "Truck Master")
                    {
                        //sql = "Select EqupmetName,EqupmentID from [dbo].[EquipmentMst] where [Status='Active']";
                    }

                    string CONNSTRING = sqlConnection;
                    SqlConnection dbCon = new SqlConnection(CONNSTRING);
                    dbCon.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, dbCon);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (cmbSyncData.Text == "CargoGroup Master")
                    {
                        clearlocaldbCargoGroupMaster();
                    }
                    else if (cmbSyncData.Text == "Commodity Master")
                    {
                        clearlocaldbCommodityMaster();
                    }
                    else if (cmbSyncData.Text == "User Master")
                    {
                        clearlocaldbUserMaster();
                    }
                    else if (cmbSyncData.Text == "Loader Master")
                    {
                        clearlocaldbLoaderMaster();
                    }
                    else if (cmbSyncData.Text == "Truck Master")
                    {
                        clearlocaldbTruckMaster();
                    }
                    //................................................

                    lblStatus.Text = "Sync In Progress....";
                    string InsertQry = "";
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
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
                                    else if (cmbSyncData.Text == "Truck Master")
                                    {
                                        InsertQry = "INSERT INTO [TruckMaster] ([Id],[Tkid],[TruckNo],[Tagid],[TagNo],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','Active')";
                                    }
                                    string CONN_STRING = localConnection;
                                    SqlCeConnection Con = new SqlCeConnection(CONN_STRING);

                                    Con.Open();
                                    SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                    cmd.ExecuteNonQuery();
                                    Con.Close();

                                    lblStatus.Text = "Sync In Progress....";
                                    System.Threading.Thread.Sleep(5000);
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
                cmbSyncData.Enabled = true; btnRequestSync.Enabled = true;
            }
            catch
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

        public void clearlocaldbTruckMaster()
        {
            string connectionString = localConnection;
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            cn.Open();
            string Query = "DELETE FROM TruckMaster";
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
    }
}