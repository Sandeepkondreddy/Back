using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using DelegateMgtSystem.DAL;
using DelegateMgtSystem.DelegatesEventDetails;
using DelegateMgtSystem.Monitoring;

namespace DelegateMgtSystem
{
    public partial class MainPage : Form
    {
        private int childFormNumber = 0;
        static string str = GlobalVariables.Sqlconnection;
        SqlConnection Sqlconn;
        DataSet Ds;
        ToolStripMenuItem tItem;
        string TransType = string.Empty;
        string Expgpno = string.Empty;
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Icon = new Icon("gnome_network_offline.ico");
                //this.Location = new Point(0, 0);
                //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                //this.Icon = new Icon("gnome_network_offline.ico");
                //lblUserName.Text = GlobalVariables.LGNID;
                //lblDepartment.Text = GlobalVariables.DeptName;
                //lblVersion.Text = GlobalVariables.version;
                Sqlconn = new SqlConnection(str);
                pnlTruck.Visible = true;
                if (GlobalVariables.OfflineFlag == "Y")
                {

                }
                else
                {
                    //GetScreens();
                    foreach (Form childForm in MdiChildren)
                    {
                        childForm.Close();
                    }
                    DelegateDetails DDE = new DelegateDetails();
                    DDE.MdiParent = this;
                    DDE.Show();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            GetLogoutLog();
            GlobalVariables.GrpMapp = string.Empty;
            Hide();
            LoginPage login = new LoginPage();
            login.Show();
        }
        private void GetLogout()
        {
            LoginPage log = new LoginPage();
            log.MdiParent = this;
            log.Show();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetLogoutLog();
            Dispose();
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private void GetLogoutLog()
        {
            try
            {
                //OracleConnection conn = new OracleConnection(GlobalVariables.CONN_STRING);
                //if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                //{
                //    conn.Open();
                //}
                ////LGNID - Emp Id - GlobalVariables.EmpId
                ////USRID - UserMaster Seq Unique ID - GlobalVariables.LoginID 
                ////USERNAME - Login ID - GlobalVariables.LGNID
                ////GROUPNAME - Emp Group - GlobalVariables.GrpName
                ////APPLICATION - Running Application, VERSIONTYPE - Version No, SYSTEMIP - Current Sys IP, LOGINTIME - LoginTime

                //string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                //// Get the IP
                //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //OracleCommand comd = new OracleCommand("update SETP.APPVERSION_LOG set LOGOUTTIME =to_date('" + Common.GetDBSysdate() + "','DD-MON-YYYY HH24:MI:SS') where APPLICATION='" + GlobalVariables.ApplicationName + "' and SYSTEMIP='" + myIP + "' and VERSIONTYPE='" + GlobalVariables.version + "' and LOGINTIME=(SELECT MAX (LOGINTIME) FROM SETP.APPVERSION_LOG WHERE APPLICATION='" + GlobalVariables.ApplicationName + "' and SYSTEMIP='" + myIP + "')", conn);
                //comd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
        }
      
        private static void CatchException(string excepmsg)
        {
            StreamWriter strW = File.AppendText(Application.ExecutablePath + "ErrorLog.txt");
            strW.WriteLine(excepmsg);
            strW.Close();
        }

     
        private void MainPage_Shown(object sender, EventArgs e)
        {
           
        }

        private void delegateDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void delegateEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            DelegateDetails DDE = new DelegateDetails();
            DDE.MdiParent = this;
            DDE.Show();
        }


        private void vPassEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            VPassEventMapping DDE = new VPassEventMapping();
            DDE.MdiParent = this;
            DDE.Show();
        }

        private void EventMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            EventMonitoring DDE = new EventMonitoring();
            DDE.MdiParent = this;
            DDE.Show();
        }

        private void trackingMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            DelegateTracking DDE = new DelegateTracking();
            DDE.MdiParent = this;
            DDE.Show();
        }

        private void delegateTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            DelegateMonitoring DDE = new DelegateMonitoring();
            DDE.MdiParent = this;
            DDE.Show();
        }

        private void localDataSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlTruck.Visible = true;
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            LocalDataSync DDE = new LocalDataSync();
            DDE.MdiParent = this;
            DDE.Show();
        }
    }
}
