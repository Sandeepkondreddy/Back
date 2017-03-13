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
    public partial class DelegateMonitoring : Form
    {
        static string Sqlstr = GlobalVariables.Sqlconnection;
        SqlConnection Sqlcon;
        public DelegateMonitoring()
        {
            InitializeComponent();
            Sqlcon = new SqlConnection(Sqlstr);
            BindEmptyGrid();
            FillMethods();
            FillInvitations();
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
          
        }
        private void DelegateMonitoring_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            FillCategorywise();
            FillVPCategorywise();
        }
        private void FillCategorywise()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select Category,COUNT(*) NoOfPasses from [dbo].[DelegateMaster] group by Category", Sqlcon);
            da.Fill(ds);
            gvcategorywisealloted.DataSource = ds.Tables[0];
            gvcategorywisealloted.ShowEditingIcon = false;
        }

        private void FillVPCategorywise()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select a.Category VPCategory,COUNT(*) NoOfPasses from [dbo].[VisitorPassCategory] a,[dbo].[DelegateMaster] b where a.VPCId = b.VPCId group by a.Category", Sqlcon);
            da.Fill(ds);
            gvVPCategoryWise.DataSource = ds.Tables[0];
            gvVPCategoryWise.ShowEditingIcon = false;

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

            IFormatProvider format = new CultureInfo("en-GB");

            const string strinsgv = "Select DM.DelegateName,TM.TagNo,DM.Category,VP.Category VPCategory FROM  [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP,[dbo].[TagMst] TM where VP.VPCId=DM.VPCId and TM.TagId=DM.TagId ";
            string condition = " ";
            if (cmbTagCateogry.Text != "--Select--")
                condition = condition + " AND VP.Category ='" + cmbTagCateogry.Text + "'";
            if (cmbCategory.Text != "--Select--")
                condition = condition + " AND DM.Category ='" + cmbCategory.Text + "'";
            //if (cmbEventName.Text != "--Select--")
            //    condition = condition + " AND DTL.ReaderLocation ='" + cmbEventName.Text + "'";
            if (txtInviteeName.Text != string.Empty)
                condition = condition + " AND DM.DelegateName Like '%" + txtInviteeName.Text + "%'";
            condition = condition + " order by DM.DMId desc";
            string searchqry = strinsgv + condition;
            BindGrid(searchqry);
            if (gv.Rows.Count <= 0)
            {
                MessageBox.Show("There is No Records Found.. Please Verify the Search...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindEmptyGrid();
                ResetData();
            }
        }

        private void BindEmptyGrid()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string strinsgv = "Select DM.DelegateName,TM.TagNo,DM.Category,VP.Category VPCategory FROM  [dbo].[DelegateMaster] DM,[dbo].[VisitorPassCategory] VP,[dbo].[TagMst] TM where VP.VPCId=DM.VPCId and TM.TagId=DM.TagId order by DM.DMId desc";
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
            txtInviteeName.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
            cmbTagCateogry.SelectedIndex = 0;
            cmbEventName.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillCategorywise();
            FillVPCategorywise();
        }

    }
}
