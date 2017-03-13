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

namespace DelegateMgtSystem.DelegatesEventDetails
{
    public partial class VPassEventMapping : Form
    {
        static string Sqlstr = GlobalVariables.Sqlconnection;
        SqlConnection Sqlcon;
        public VPassEventMapping()
        {
            InitializeComponent(); 
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 25;
            checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gv.Columns.Insert(0, checkboxColumn);
            cmbStatus.SelectedIndex = 0;
        }

        private void VPassEventMapping_Load(object sender, EventArgs e)
        {
            Sqlcon = new SqlConnection(Sqlstr);
            FillMethods();
            loadgrid();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidations())
            {
                if (btnSave.Text == "&Save")
                {
                    if (SaveData())
                    {
                        btnSave.Enabled = false;
                        MessageBox.Show("RECORD SAVED SUCCESSFULLY", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadgrid();
                    }
                }
                else
                {
                    if (UpdateData())
                    {
                        btnSave.Enabled = false;
                        MessageBox.Show("RECORD UPDATE SUCCESSFULLY", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadgrid();

                    }
                }
            }
        }
        private bool CheckValidations()
        {
            if (cmbEventName.Text == string.Empty || cmbEventName.Text == "--Select--")
            {
                MessageBox.Show("EVENT NAME IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbEventName.Focus();
                return false;
            }

            if (cmbTagCateogry.Text == string.Empty || cmbTagCateogry.Text == "--Select--")
            {
                MessageBox.Show("TAG CATEGORY IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTagCateogry.Focus();
                return false;
            }
            
            return true;
        }
        public void loadgrid()
        {
            try
            {
                clear();
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string Query = "SELECT VPSE.[VPEMId] VPEMId,VPSE.VPCId,VPSE.EId,VPS.Category,VPSE.[EventCode] EventCode,case when  VPSE.Status='A' then 'Active' when  VPSE.Status='I' then 'InActive' end Status,VPSE.Remarks FROM [dbo].[VPassEventMapping] VPSE,[dbo].[VisitorPassCategory] VPS,[dbo].[EventMaster] EM where VPSE.VPCId=VPS.VPCId and EM.EId=VPSE.EId  and VPSE.Status='A' order by VPSE.[VPEMId] desc";
                SqlCommand cmd = new SqlCommand(Query, Sqlcon);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                da.Fill(ds, "Query");
                gv.DataMember = "Query";
                gv.DataSource = ds;
                for (int i = 0; i <= gv.Rows.Count - 1; i++)
                {
                    gv.Columns["VPEMId"].Visible = false;
                    gv.Columns["VPCId"].Visible = false;
                    gv.Columns["EId"].Visible = false;
                }

                Sqlcon.Close();
                //  txtDataCount.Text = gv.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private bool SaveData()
        {
            try
            {
                if (ValidateMapping())
                {
                    if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                    {
                        Sqlcon.Open();
                    }

                    string str = "insert into [dbo].[VPassEventMapping] ([VPCId],[EId],[EventCode],[Status],CreatedBy,CreatedTime,[Remarks]) Values (@VPCId,@EId,@EventCode,@Status,@CreatedBy,GetDate(),@Remarks)";
                    SqlCommand inscmd = new SqlCommand(str, Sqlcon);
                    inscmd.Parameters.AddWithValue("@VPCId", cmbTagCateogry.SelectedValue.ToString());
                    inscmd.Parameters.AddWithValue("@EId", cmbEventName.SelectedValue.ToString());
                    inscmd.Parameters.AddWithValue("@EventCode", cmbEventName.Text.ToString());
                    inscmd.Parameters.AddWithValue("@Status", "A");
                    inscmd.Parameters.AddWithValue("@CreatedBy", GlobalVariables.LGNID);
                    inscmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                    int eta = inscmd.ExecuteNonQuery();

                    if (eta == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool ValidateMapping()
        {
            return true;
        }
        private bool UpdateData()
        {
            try
            {
                string vpestatus = string.Empty;
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                if (cmbStatus.Text == "Active")
                {
                    vpestatus = "A";
                }
                else if (cmbStatus.Text == "Inactive")
                {
                    vpestatus = "I";
                }
                SqlCommand qrycmd = new SqlCommand("update [dbo].[VPassEventMapping] set  [Status]=@Status,[ModifiedBy]=@ModifiedBy,ModifiedTime=GetDate(),Remarks=@Remarks  where VPEMId=@VPEMId", Sqlcon);
                qrycmd.Parameters.AddWithValue("@VPEMId", hdnvpceId.Text.ToString());
                qrycmd.Parameters.AddWithValue("@Status", vpestatus);
                qrycmd.Parameters.AddWithValue("@ModifiedBy", GlobalVariables.LGNID);
                qrycmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                qrycmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void gv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i <= gv.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(gv.Rows[e.RowIndex].Cells[0].Value) == false)
                    {
                        gv.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool isChecked = (Boolean)gv.Rows[e.RowIndex].Cells[0].Value;  //gv[e.ColumnIndex, e.RowIndex].FormattedValue;
            if (isChecked)
            {
                hdnEMID.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["EId"].Value);
                hdnvpceId.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["VPEMId"].Value);
                hdnVPCID.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["VPCId"].Value);
                cmbEventName.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["EventCode"].Value);
                cmbTagCateogry.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["Category"].Value);
                txtRemarks.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["Remarks"].Value);
                cmbStatus.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["Status"].Value);
                btnSave.Text = "&Update";

                cmbEventName.Enabled = false;
                cmbTagCateogry.Enabled = false;
                cmbStatus.Enabled = true;
                txtRemarks.Enabled = true;
                btnSave.Enabled = true;

            }
            else
            {
                cmbStatus.Text = "";
                cmbEventName.Enabled = true;
                cmbTagCateogry.Enabled = true;
                cmbStatus.Enabled = false;
                txtRemarks.Enabled = true;
                btnSave.Text = "&Save";
                cmbEventName.SelectedIndex = 0;
                cmbTagCateogry.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                txtRemarks.Text = string.Empty;
            }
        }

        private void cmbEventName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbEventName.Text != "--Select--")
                {
                    ComboBox comboBox = (ComboBox)sender;

                    // Save the selected Ref Code's , because we will check it is correct code or not 
                    // the Ref Code's from the list. 
                    string selectedRefCode = (string)cmbEventName.Text;
                    int resultIndex = -1;

                    // Call the FindStringExact method to find the first  
                    // occurrence in the list.
                    resultIndex = cmbEventName.FindStringExact(selectedRefCode);

                    if (resultIndex == -1)
                    {
                        MessageBox.Show("Please Select Valid EventName...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //cmbRefCode.Focus();
                        cmbEventName.Text = "";
                        return;
                    }
                    else
                    {
                        if (btnSave.Text == "&Save")
                        {
                            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                            {
                                Sqlcon.Open();
                            }
                            string statusQuery = "SELECT VPS.Category,VPSE.EventCode FROM [dbo].[VPassEventMapping] VPSE,[dbo].[VisitorPassCategory] VPS where VPSE.VPCId=VPS.VPCId  and VPSE.Status='A' and VPS.Category='" + cmbTagCateogry.Text + "' and VPSE.EventCode='" + cmbEventName.Text + "'  order by VPSE.[VPEMId] desc ";
                            SqlCommand statuscmd = new SqlCommand(statusQuery, Sqlcon);
                            SqlDataReader dr = statuscmd.ExecuteReader();
                            if (dr.Read())
                            {
                                dr.Close();
                                MessageBox.Show("This Event is Already mapped to select category...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmbEventName.Text = "";
                                return;
                            }
                            dr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //CatchException(string.Format("{0} - {1} - {2} - {3} - {4}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, GlobalVariables.version));
                Sqlcon.Close();
            }
        }

        private void cmbTagCateogry_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbTagCateogry.Text != "--Select--")
                {
                    ComboBox comboBox = (ComboBox)sender;

                    // Save the selected Ref Code's , because we will check it is correct code or not 
                    // the Ref Code's from the list. 
                    string selectedRefCode = (string)cmbTagCateogry.Text;
                    int resultIndex = -1;

                    // Call the FindStringExact method to find the first  
                    // occurrence in the list.
                    resultIndex = cmbTagCateogry.FindStringExact(selectedRefCode);

                    if (resultIndex == -1)
                    {
                        MessageBox.Show("Please Select Valid Category..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //cmbRefCode.Focus();
                        cmbTagCateogry.Text = "";
                        return;
                    }
                    else
                    {
                        // btnAdd.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //CatchException(string.Format("{0} - {1} - {2} - {3} - {4}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, GlobalVariables.version));
                Sqlcon.Close();
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string strinsgv = "SELECT VPSE.[VPEMId] VPEMId,VPSE.VPCId,VPSE.EId,VPS.Category,VPSE.[EventCode], VPSE.Status,VPSE.Remarks FROM [dbo].[VPassEventMapping] VPSE,[dbo].[VisitorPassCategory] VPS,[dbo].[EventMaster] EM where VPSE.VPCId=VPS.VPCId and EM.EId=VPSE.EId  and VPSE.[EventCode] like '%" + txtSearch.Text + "%' order by VPSE.[VPEMId] desc";
                //string strinsgv = "SELECT [DMId] DMId,DM.[TagId] TAGID,TM.TagNo TAGNO,[DelegateName] INVITEENAME,[Category] CATEGORY,[Company] Company,[NoofPeople] NOOFPERSONS,[PAName] PANAME,[PAMobileNo] PACONTACTNO,[VehicleNo] VECHICLENO,[HostName] HOSTNAME,[HostMobileNo] HOSTCONTACT,DM.[Remarks] REMARKS,VPC.Category VPCategory,VPC.VPCId FROM  [dbo].[DelegateMaster] DM,[dbo].[TagMst] TM ,[dbo].[VisitorPassCategory] VPC where VPC.VPCId=DM.VPCId and DM.TagId=TM.TagId and DelegateName like '%" + txtSearch + "%'  order by DM.DMId desc";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strinsgv, Sqlcon);
                da.Fill(ds);

                gv.DataSource = ds.Tables[0];
                for (int i = 0; i <= gv.Rows.Count - 1; i++)
                {
                    gv.Columns["VPEMId"].Visible = false;
                    gv.Columns["VPCId"].Visible = false;
                    gv.Columns["EId"].Visible = false;
                }
                cmbEventName.Select();
                gv.ShowEditingIcon = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            cmbEventName.Enabled = true;
            cmbTagCateogry.Enabled = true;
            cmbStatus.Enabled = false;
            txtRemarks.Enabled = true;
            btnSave.Text = "&Save";
            cmbEventName.SelectedIndex = 0;
            cmbTagCateogry.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            cmbStatus.SelectedIndex = 0;
            gvEventName.Visible = false;
        }
        private void cmdexit_Click(object sender, EventArgs e)
        {
            CausesValidation = false;
            DialogResult result = MessageBox.Show("DO YOU WANT TO EXIT?", "INFORMATION", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void VPassEventMapping_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmbEventName.Leave -= cmbEventName_Leave;
            cmbTagCateogry.Leave -= cmbTagCateogry_Leave;
        }

        private void cmbTagCateogry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTagCateogry.Text != "--Select--")
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                string strinsgv = "SELECT VPSE.[EventCode] FROM [dbo].[VPassEventMapping] VPSE,[dbo].[VisitorPassCategory] VPS,[dbo].[EventMaster] EM where VPSE.VPCId=VPS.VPCId and EM.EId=VPSE.EId  and VPSE.Status='A'  and VPS.Category='" + cmbTagCateogry.Text + "'  order by VPSE.[VPEMId] desc";
                //string strinsgv = "SELECT [DMId] DMId,DM.[TagId] TAGID,TM.TagNo TAGNO,[DelegateName] INVITEENAME,[Category] CATEGORY,[Company] Company,[NoofPeople] NOOFPERSONS,[PAName] PANAME,[PAMobileNo] PACONTACTNO,[VehicleNo] VECHICLENO,[HostName] HOSTNAME,[HostMobileNo] HOSTCONTACT,DM.[Remarks] REMARKS,VPC.Category VPCategory,VPC.VPCId FROM  [dbo].[DelegateMaster] DM,[dbo].[TagMst] TM ,[dbo].[VisitorPassCategory] VPC where VPC.VPCId=DM.VPCId and DM.TagId=TM.TagId and DelegateName like '%" + txtSearch + "%'  order by DM.DMId desc";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strinsgv, Sqlcon);
                da.Fill(ds);
                gvEventName.DataSource = ds.Tables[0];              
                cmbEventName.Select();
                gvEventName.ShowEditingIcon = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvEventName.Visible = true;
                }
                else
                {
                    gvEventName.Visible = false;
                }
            }
        }

        private void gv_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            int width = e.Column.Width;
            if (e.Column.DataPropertyName == "")
                //e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                e.Column.Width = 25;
            else if (e.Column.DataPropertyName == "Category" || e.Column.DataPropertyName == "EventCode" || e.Column.DataPropertyName == "Status" || e.Column.DataPropertyName == "Remarks")
                e.Column.Width = 140;
            else
                e.Column.Width = 97;
            
        }
    }
}
