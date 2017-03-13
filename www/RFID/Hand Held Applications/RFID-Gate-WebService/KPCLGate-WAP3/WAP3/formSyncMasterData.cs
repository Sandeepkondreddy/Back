using System;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KPCLGate.appserver;
using System.Net;
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
                    //if (cmbSyncData.Text == "User Master")
                    //{
                    //    sql = " Select UId,Loginid,Password,LoginType,UserName from [dbo].[HHLoginMst] where [UStatus]='Active'";
                    //}
                    
                    DataTable dt = getDatafromWebservice(cmbSyncData.Text.ToString());

                    if (cmbSyncData.Text == "User Master")
                    {
                        clearlocaldbUserMaster();
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
                                    if (cmbSyncData.Text == "User Master")
                                    {
                                        InsertQry = "INSERT INTO [UserMst] ([Id],[LoginId],[Password],[LoginType],[UserName],[Status]) Values ('" + dr[0].ToString() + "','" + dr[1].ToString() + "','" + dr[2].ToString() + "','" + dr[3].ToString() + "','" + dr[4].ToString() + "','Active')";
                                    }
                                    string CONN_STRING = localConnection;
                                    SqlCeConnection Con = new SqlCeConnection(CONN_STRING);
                                    Con.Open();
                                    SqlCeCommand cmd = new SqlCeCommand(InsertQry, Con);
                                    cmd.ExecuteNonQuery();
                                    Con.Close();
                                    lblStatus.Text = "Sync In Progress....";
                                    //System.Threading.Thread.Sleep(5000);
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
            bool hasNet = false; DataTable dt=new DataTable();
            KPCLGate.appserver.Service1 details = new KPCLGate.appserver.Service1();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://172.168.0.45/rfid-web/");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                //request.Timeout = 5000;
                //request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        hasNet = true;
                        classLog.writeLog("Message @:Web Service Connected.");
                        classLog.writeLog("Message @:GetDtails: " + getdetails.ToString());
                        dt = details.GetMasterDetails(getdetails);
                       
                        if (dt.Rows.Count == 0)
                        {
                            //MessageBox.Show("Details Not found."); 
                            lblStatus.Text = "Details Not found.";
                            classLog.writeLog("Message @: Master Details Not found.");
                        }
                        else
                        {
                            classLog.writeLog("Message @:Master Received from DB.");
                        }
                        //details.Dispose(); ds.Dispose();
                    }
                    else
                    {
                        //MessageBox.Show("Network Not Connected.."); 
                        lblStatus.Text = "Network Not Connected..";
                        classLog.writeLog("Error @:Network Not Connected..");
                    }
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

    }
}