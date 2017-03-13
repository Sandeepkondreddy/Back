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
    public partial class DelegateTracking : Form
    {
        static string Localstr = GlobalVariables.Sqlconnection;
        static string Mainstr = GlobalVariables.MainServerSqlconnection;
        SqlConnection Sqlcon;
        string frmdate, todate;
        public DelegateTracking()
        {
            InitializeComponent();
            string SQLstatus = ConfigurationSettings.AppSettings.Get("LocalorMain").ToString();
            if (SQLstatus == "Local")
            {
                Sqlcon = new SqlConnection(Localstr);
            }
            if (SQLstatus == "Main")
            {
                Sqlcon = new SqlConnection(Mainstr);
            }
            BindEmptyGrid();
            FillMethods();
            FillInvitations();
        }

        private void TrackingMonitoring_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = " ";
            dtpFromDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = " ";
            dtpToDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
            ResetDateFormats();
        }
        public void FillInvitations()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) Invitations FROM  [dbo].[TagMst]", Sqlcon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtinvitations.Text = dr["Invitations"].ToString();
            }
            dr.Close();
            SqlCommand issuecmd = new SqlCommand("SELECT COUNT(*) Issued FROM  [dbo].[TagMst]  where Status='Y'", Sqlcon);
            SqlDataReader issuedr = issuecmd.ExecuteReader();
            if (issuedr.Read())
            {
                txtissued.Text = issuedr["Issued"].ToString();
            }
            issuedr.Close();
            SqlCommand Attencmd = new SqlCommand("SELECT COUNT(*) Attendees FROM  [dbo].[DelegateTrackingLog]  where DMId!='0'", Sqlcon);
            SqlDataReader Attendr = Attencmd.ExecuteReader();
            if (Attendr.Read())
            {
                txtattendees.Text = Attendr["Attendees"].ToString();
            }
            Attendr.Close();
           
        }

        #region FilCombos
        public void FillMethods()
        {
            FillCombo("SELECT [EId] ,[EventCode] FROM  [dbo].[EventMaster] where  [Status]='Active'", ref cmbEventName);
            FillCombo("SELECT [VPCId] ,[Category] FROM  [dbo].[VisitorPassCategory] where  [Status]='A'", ref cmbTagCateogry);

        }
        public void FillCombo(string Qry, ref ComboBox cbo)
        {
            try
            {
                DataSet cmbds = new DataSet();
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                SqlDataAdapter Catdar2 = new SqlDataAdapter(Qry, Sqlcon);
                Catdar2.Fill(cmbds);
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("ID");

                DataRow dr;
                dr = dt.NewRow();
                dr["Name"] = "--Select--";
                dr["ID"] = 0;
                dt.Rows.Add(dr);
                foreach (DataRow drDS in cmbds.Tables[0].Rows)
                {
                    dr = dt.NewRow();
                    dr["Name"] = drDS[1];
                    dr["ID"] = drDS[0];
                    dt.Rows.Add(dr);
                }
                cbo.DataSource = dt;
                cbo.DisplayMember = "Name";
                cbo.ValueMember = "ID";
            }
            catch
            {

            }
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (CheckDateValidation())
            {
                if (CheckDateValidate())
                {
                    IFormatProvider format = new CultureInfo("en-GB");
                    frmdate = string.Empty;
                    todate = string.Empty;
                    if (dtpFromDate.Text != " ")
                    {
                        frmdate = Convert.ToDateTime(dtpFromDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (dtpToDate.Text != " ")
                    {
                        todate = Convert.ToDateTime(dtpToDate.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    const string strinsgv = "Select case when DTL.DMId='0'  then ' ' else DTSumm.DelegateName end DelegateName,DTL.TagNo,DTSumm.Category,DTSumm.VPCategory, DTL.ReaderIP,DTL.ReaderLocation,DTL.ReadTime FROM [DMS].[dbo].[DelegateTrackingLog]  DTL LEFT OUTER JOIN (Select DT.TrackingId, DM.DelegateName,DM.Category,VP.Category VPCategory FROM [dbo].[DelegateTrackingLog] DT, [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP where DT.DMId=DM.DMId and VP.VPCId=DM.VPCId ) DTSumm ON DTL.TrackingId=DTSumm.TrackingId  where 1=1 ";
                    string condition = " ";
                    if (cmbTagCateogry.Text != "--Select--")
                        condition = condition + " AND DTSumm.VPCategory ='" + cmbTagCateogry.Text + "'";
                    if (cmbCategory.Text != "--Select--")
                        condition = condition + " AND DTSumm.Category ='" + cmbCategory.Text + "'";
                    if (cmbEventName.Text != "--Select--")
                        condition = condition + " AND DTL.ReaderLocation ='" + cmbEventName.Text + "'";
                    if (txtInviteeName.Text != string.Empty)
                        condition = condition + " AND DTSumm.DelegateName Like '%" + txtInviteeName.Text + "%'";
                    if (dtpFromDate.Text != " " && dtpToDate.Text != " ")
                        condition = condition + " AND (DTL.CreatedTime between ('" + frmdate + "') and ('" + todate + "') or DTL.ModifiedTime between ('" + frmdate + "') and ('" + todate + "'))";
                    condition = condition + " order by DTL.TrackingId desc";
                    string searchqry = strinsgv + condition;
                    BindGrid(searchqry);
                    if (gv.Rows.Count <= 0)
                    {
                        MessageBox.Show("There is No Records Found.. Please Verify the Search...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindEmptyGrid();
                        ResetData();
                    }
                }
            }
        }

        private void BindEmptyGrid()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string strinsgv = "Select case when DTL.DMId='0'  then ' ' else DTSumm.DelegateName end DelegateName,DTL.TagNo,DTSumm.Category,DTSumm.VPCategory, DTL.ReaderIP,DTL.ReaderLocation,DTL.ReadTime FROM [DMS].[dbo].[DelegateTrackingLog]  DTL  LEFT OUTER JOIN (Select DT.TrackingId, DM.DelegateName,DM.Category,VP.Category VPCategory FROM [dbo].[DelegateTrackingLog] DT, [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP where DT.DMId=DM.DMId and VP.VPCId=DM.VPCId ) DTSumm  ON DTL.TrackingId=DTSumm.TrackingId order by DTL.TrackingId desc";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strinsgv, Sqlcon);
            da.Fill(ds);

            gv.DataSource = ds.Tables[0];
            //for (int i = 0; i <= gv.Rows.Count - 1; i++)
            //{
            //    gv.Columns["DMId"].Visible = false;
            //}
            gv.ShowEditingIcon = false;
        }
        private void BindGrid(string condition)
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }     
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(condition, Sqlcon);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 500)
            {
                MessageBox.Show("Selected Criteira fetching More no. of records Please Verify your Search...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                gv.DataSource = ds.Tables[0];
                gv.ShowEditingIcon = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetData();
        }
        private void ResetData()
        {
            ResetDateFormats();
            txtInviteeName.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
            cmbTagCateogry.SelectedIndex = 0;
            cmbEventName.SelectedIndex = 0;
        }
        private void ResetDateFormats()
        {
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = " ";
            dtpFromDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = " ";
            dtpToDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
            dtpFromDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
            if (dtpToDate.Text != " ")
            {
                DateTime fromdate1 = Convert.ToDateTime(dtpFromDate.Text);
                DateTime todate1 = Convert.ToDateTime(dtpToDate.Text);
                if (fromdate1 <= todate1)
                {
                }
                else
                {
                    MessageBox.Show("From Date Must be Less Than To Date", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpToDate.CustomFormat = " ";
                }
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
            dtpToDate.MaxDate = Convert.ToDateTime(DateTime.Now.ToString());
            // dtpToDate.Text = Common.GetDBSysdate().ToString();

            if (dtpFromDate.Text != " ")
            {
                DateTime fromdate1 = Convert.ToDateTime(dtpFromDate.Text);
                DateTime todate1 = Convert.ToDateTime(dtpToDate.Text);
                if (fromdate1 <= todate1)
                {

                }
                else
                {
                    MessageBox.Show("From Date Must be Less Than To Date", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpToDate.CustomFormat = " ";
                }
            }
        }
        private bool CheckDateValidate()
        {
            try
            {
                if (dtpFromDate.Text != " ")
                {
                    if (dtpToDate.Text != " ")
                    {
                        if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(DateTime.Now))
                        {
                            MessageBox.Show("FROM DATE SHOULD BE LESS THAN CURRENT DATE.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpFromDate.Focus();
                            return false;
                        }
                        if (Convert.ToDateTime(dtpToDate.Text) > Convert.ToDateTime(DateTime.Now))
                        {
                            MessageBox.Show("TO DATE SHOULD BE LESS THAN CURRENT DATE.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpToDate.Focus();
                            return false;
                        }
                        if (Convert.ToDateTime(dtpFromDate.Text) > Convert.ToDateTime(dtpToDate.Text))
                        {
                            MessageBox.Show("TO DATE SHOULD BE GREATER THAN FROM DATE.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpToDate.Focus();
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show("PLEASE ENTER VALID DATE && TIME.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFromDate.Focus();
                return false;
            }
        }
        public bool CheckDateValidation()
        {
            if (dtpFromDate.CustomFormat == " " && dtpToDate.CustomFormat != " ")
            {
                MessageBox.Show("Please Select FromDate", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (dtpFromDate.CustomFormat != " " && dtpToDate.CustomFormat == " ")
            {
                MessageBox.Show("Please Select ToDate", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
                return true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillInvitations();
        }
    }
}
