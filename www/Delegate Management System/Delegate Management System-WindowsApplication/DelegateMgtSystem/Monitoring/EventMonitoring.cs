using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Data.SqlClient;
using DelegateMgtSystem.DAL;

namespace DelegateMgtSystem.Monitoring
{
    public partial class EventMonitoring : Form
    {
        SqlConnection Sqlcon;
        static string Sqlstr = GlobalVariables.Sqlconnection;

        public EventMonitoring()
        {
            InitializeComponent();
            Sqlcon = new SqlConnection(Sqlstr);
            string HdrName = ConfigurationSettings.AppSettings.Get("Header1Name").ToString();
            string AGateName = ConfigurationSettings.AppSettings.Get("AGateName").ToString();
            string BGateName = ConfigurationSettings.AppSettings.Get("BGateName").ToString();
            string AGateReaderIP = ConfigurationSettings.AppSettings.Get("AGateReaderIP").ToString();
            string BGateReaderIP = ConfigurationSettings.AppSettings.Get("BGateReaderIP").ToString();
            lblHeader1.Text = HdrName;
            lblAGate.Text = AGateName;
            lblBGate.Text = BGateName;
            lblAReaderIP.Text = AGateReaderIP;
            lblBReaderIP.Text = BGateReaderIP;
            picAStatus.Image = Properties.Resources.Wrong;
        }

        private void DelegateMonitoring_Load(object sender, EventArgs e)
        {
            tmrAGate.Enabled = true;
            tmrBGate.Enabled = true;
        }
        public void GetAGateDetails()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string cmd = "Select  TOP 1  case when DTL.DMId='0'  then ' ' else DTSumm.DelegateName end DelegateName,DTL.TagNo,DTSumm.Category,DTSumm.VPCategory,DTSumm.NoofPeople,DTSumm.VehicleNo,DTL.ReadTime FROM [DMS].[dbo].[DelegateTrackingLog]  DTL  LEFT OUTER JOIN (Select DT.TrackingId, DM.DelegateName,DM.Category,VP.Category VPCategory,DM.NoofPeople,DM.VehicleNo FROM [dbo].[DelegateTrackingLog] DT, [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP where DT.DMId=DM.DMId and VP.VPCId=DM.VPCId ) DTSumm  ON DTL.TrackingId=DTSumm.TrackingId where DTL.ReaderNo ='" + lblAReaderIP.Text + "'  order by DTL.TrackingId desc";
            SqlDataAdapter da = new SqlDataAdapter(cmd, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //picAStatus.Image = Properties.Resources.Allow;
                string DName = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (DName == " " || DName == string.Empty)
                {
                    lblADName.Text = "UnKnown";
                    tmrABlinking.Enabled = true;
                    picAStatus.Image = Properties.Resources.Wrong;
                }
                else
                {
                    tmrABlinking.Enabled = false;
                    pnlADname.BackColor = Color.FromArgb(193, 225, 220);
                    lblADName.Text = DName;
                    picAStatus.Image = Properties.Resources.Allow;
                }
                lblAPassID.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                lblACategory.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                lblAVPCategory.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                lblANoofPersons.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                lblAVechicleNo.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                lblATime.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            }
            else
            {
                picAStatus.Image = Properties.Resources.Wrong;
            }
        }
        public void GetBGateDetails()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string cmd = "Select  TOP 1  case when DTL.DMId='0'  then ' ' else DTSumm.DelegateName end DelegateName,DTL.TagNo,DTSumm.Category,DTSumm.VPCategory,DTSumm.NoofPeople,DTSumm.VehicleNo,DTL.ReadTime FROM [DMS].[dbo].[DelegateTrackingLog]  DTL  LEFT OUTER JOIN (Select DT.TrackingId, DM.DelegateName,DM.Category,VP.Category VPCategory,DM.NoofPeople,DM.VehicleNo FROM [dbo].[DelegateTrackingLog] DT, [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP where DT.DMId=DM.DMId and VP.VPCId=DM.VPCId ) DTSumm  ON DTL.TrackingId=DTSumm.TrackingId where DTL.ReaderNo ='" + lblBReaderIP.Text + "'  order by DTL.TrackingId desc";
            SqlDataAdapter da = new SqlDataAdapter(cmd, Sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //picBStatus.Image = Properties.Resources.Allow;
                string DName = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                if (DName == " " || DName == string.Empty)
                {
                    lblBDName.Text = "UnKnown";
                    tmrBBlinking.Enabled = true;
                    picBStatus.Image = Properties.Resources.Wrong;
                }
                else
                {
                    lblBDName.Text = DName;
                    tmrBBlinking.Enabled = false;
                    pnlBDname.BackColor = Color.FromArgb(193, 225, 220);
                    picBStatus.Image = Properties.Resources.Allow;
                }
                lblBPassID.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                lblBCategory.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                lblBVPCategory.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                lblBNoofPersons.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
                lblBVechicleNo.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                lblBTime.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            }
            else
            {
                picBStatus.Image = Properties.Resources.Wrong;
            }
        }

        private void tmrAGate_Tick(object sender, EventArgs e)
        {
            GetAGateDetails();
        }

        private void tmrBGate_Tick(object sender, EventArgs e)
        {
            GetBGateDetails();
        }

        private void tmrABlinking_Tick(object sender, EventArgs e)
        {
            //    for (int i = 0; i < 10; i++)
            //    {
            System.Threading.Thread.Sleep(500); // Set fast to slow.
            if (pnlADname.BackColor == Color.Red)
                pnlADname.BackColor = Color.FromArgb(193, 225, 220);
            else
                pnlADname.BackColor = Color.Red;
            //  }

        }

        private void tmrBBlinking_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500); // Set fast to slow.
            if (pnlBDname.BackColor == Color.Red)
                pnlBDname.BackColor = Color.FromArgb(193, 225, 220);
            else
                pnlBDname.BackColor = Color.Red;
        }
    }
}
