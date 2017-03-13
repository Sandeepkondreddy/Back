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
    public partial class DelegateDetails : Form
    {
        int PFlag = 0, TruckFlag = 0;
        SqlCommand Sqlcmd;
        SqlConnection Sqlcon;
        int ids = 0;
        DataTable dt = new DataTable("DELEGATEDATA");
        DataSet ds = new DataSet();

        static string Sqlstr = GlobalVariables.Sqlconnection;

        public DelegateDetails()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 25;
            checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Sqlcon = new SqlConnection(Sqlstr);
            gv.Columns.Insert(0, checkboxColumn);
            FillMethods();
            BindEmptyGrid();
            btnSave.Text = "&Save";
        }
       
        private void EventDetails_Load(object sender, EventArgs e)
        {
           // this.WindowState = FormWindowState.Maximized;
            //Sqlcon = new SqlConnection(Sqlstr);
            //FillMethods();

        }
        #region FilCombos
        public void FillMethods()
        {
            FillCombo("SELECT [TagId] ,[TagNo] FROM  [dbo].[TagMst] where  [Status]='N'", ref cmbTagNo);
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

        //private void BindEmptyGrid()
        //{
        //    dt.Columns.Add("DMId");
        //    dt.Columns.Add("TAGID");
        //    dt.Columns.Add("TAGNO");
        //    dt.Columns.Add("TAGCATEGORY");
        //    dt.Columns.Add("INVITEENAME");
        //    dt.Columns.Add("CATEGORY");
        //    dt.Columns.Add("NOOFPERSONS");
        //    dt.Columns.Add("VECHICLENO");
        //    dt.Columns.Add("PANAME");
        //    dt.Columns.Add("PACONTACTNO");
        //    dt.Columns.Add("HOSTNAME");
        //    dt.Columns.Add("HOSTCONTACT");
        //    dt.Columns.Add("REMARKS");
        //    gv.DataSource = dt;
        //    for (int i = 0; i <= gv.Rows.Count; i++)
        //    {
        //        gv.Columns["DMId"].Visible = false;
        //        gv.Columns["TAGID"].Visible = false;
        //    }
        //    gv.ShowEditingIcon = false;
        //}
        private void BindEmptyGrid()
        {
            if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            {
                Sqlcon.Open();
            }
            string strinsgv = "SELECT [DMId] DMId,DM.[TagId] TAGID,TM.TagNo Tagno,DM.[DelegateName] InviteeName,DM.[Category] Category,VPC.Category VPCategory,DM.[Company] Company,DM.[NoofPeople] NoOfPersons,DM.[PAName] PAName,DM.[PAMobileNo] PAContactNo,DM.[VehicleNo] VechicleNo,DM.[HostName] HostName,DM.[HostMobileNo] HostContact,DM.[Remarks] Remarks,VPC.VPCId FROM  [dbo].[DelegateMaster] DM,[dbo].[TagMst] TM ,[dbo].[VisitorPassCategory] VPC where VPC.VPCId=DM.VPCId and  DM.TagId=TM.TagId order by DM.DMId desc";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(strinsgv, Sqlcon);
            da.Fill(ds);

            gv.DataSource = ds.Tables[0];
            //gv.Columns.Add(cc);
            DetailsControlsEnable();
            for (int i = 0; i <= gv.Rows.Count - 1; i++)
            {
                gv.Columns["DMId"].Visible = false;
                gv.Columns["TAGID"].Visible = false;
                gv.Columns["VPCId"].Visible = false;
            }
            cmbTagNo.Select();
            gv.ShowEditingIcon = false;
        }
        private void DetailsControlsEnable()
        {
            btnSave.Enabled = true;
            cmbTagNo.Enabled = true;
            cmbTagCateogry.Enabled = true;
            txtInviteeName.Enabled = true;
            cmbCategory.Enabled = true;
            txtNoofPersons.Enabled = true;
            txtVechicleNo.Enabled = true;
            txtPAName.Enabled = true;
            txtPAContactNo.Enabled = true;
            txtHostName.Enabled = true;
            txtHostContact.Enabled = true;
            txtRemarks.Enabled = true;
            cmbTagNo.Text = "";
            cmbTagNo.SelectedIndex = 0;
            cmbTagCateogry.SelectedIndex = 0;
            txtInviteeName.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
            txtNoofPersons.Text = "0";
            txtVechicleNo.Text = string.Empty;
            txtPAName.Text = string.Empty;
            txtPAContactNo.Text = string.Empty;
            txtHostName.Text = string.Empty;
            txtHostContact.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtCompany.Text = string.Empty;
            cmbTagNo.Focus();
        }
     
        private void DetailsClear()
        {
            cmbTagNo.SelectedIndex = 0;
            cmbTagNo.SelectedIndex =0;
            cmbTagCateogry.SelectedIndex = 0;
            txtInviteeName.Text = string.Empty;
            cmbCategory.SelectedIndex = 0;
            txtNoofPersons.Text = "0";            
            txtVechicleNo.Text = string.Empty;
            txtPAName.Text = string.Empty;
            txtPAContactNo.Text = string.Empty;
            txtHostName.Text = string.Empty;
            txtHostContact.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtCompany.Text = string.Empty;
        }
     
        private bool CheckValidations()
        {
            if (cmbTagNo.Text == string.Empty || cmbTagNo.Text == "--Select--")
            {
                MessageBox.Show("TAG NO IS REQUIRED.","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTagNo.Focus();
                return false;
            }

            if (cmbTagCateogry.Text == string.Empty || cmbTagCateogry.Text == "--Select--")
            {
                MessageBox.Show("TAG CATEGORY IS REQUIRED.","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTagCateogry.Focus();
                return false;
            }
            if (txtInviteeName.Text == string.Empty)
            {
                MessageBox.Show("INVITEE NAME IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInviteeName.Focus();
                return false;
            }
            if (cmbCategory.Text == string.Empty || cmbCategory.Text == "--Select--")
            {
                MessageBox.Show("CATEGORY IS REQUIRED.","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCategory.Focus();
                return false;
            }
            if (txtCompany.Text == string.Empty)
            {
                MessageBox.Show("COMPANY NAME IS REQUIRED.","WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompany.Focus();
                return false;
            }
            if (txtNoofPersons.Text == string.Empty)
            {
                MessageBox.Show("NO OF PERSONS IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoofPersons.Focus();
                return false;
            }
            
            //if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
            //{
            //    Sqlcon.Open();
            //}
            //string statusQuery = "SELECT VPS.Category,VPSE.EventCode FROM [dbo].[VPassEventMapping] VPSE,[dbo].[VisitorPassCategory] VPS where VPSE.VPCId=VPS.VPCId  and VPSE.Status='A' and VPS.Category='" + cmbTagCateogry.Text + "' and VPSE.EventCode='" + cmbEventName.Text + "'  order by VPSE.[VPEMId] desc ";
            //SqlCommand statuscmd = new SqlCommand(statusQuery, Sqlcon);
            //SqlDataReader dr = statuscmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    dr.Close();
            //    MessageBox.Show("This Delegate is Already Allocated to selected Event Category...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtInviteeName.Text = "";
            //    return false;
            //}
            //dr.Close();
            return true;
        }
       
        private void cmbTagNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                ComboBox comboBox = (ComboBox)sender;

                // Save the selected Ref Code's , because we will check it is correct code or not 
                // the Ref Code's from the list. 
                string selectedRefCode = (string)cmbTagNo.Text;
                int resultIndex = -1;

                // Call the FindStringExact method to find the first  
                // occurrence in the list.
                resultIndex = cmbTagNo.FindStringExact(selectedRefCode);

                if (resultIndex == -1)
                {
                    MessageBox.Show("Please Select Valid Tag No...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cmbRefCode.Focus();
                    cmbTagNo.Text = "";
                    //btnAdd.Enabled = false;
                    return;
                }
                else
                {
                    //btnAdd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //CatchException(string.Format("{0} - {1} - {2} - {3} - {4}", ex.Message.ToString(), ex.StackTrace.ToString(), DateTime.Now, GlobalVariables.LGNID, GlobalVariables.version));
                Sqlcon.Close();
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
                hdnDMID.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["DMId"].Value);
                hdnTagId.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["TagId"].Value);
                cmbTagNo.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["TAGNO"].Value);
                cmbTagCateogry.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["VPCategory"].Value);
                txtCompany.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["Company"].Value);
                txtInviteeName.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["INVITEENAME"].Value);
                cmbCategory.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["CATEGORY"].Value);
                txtNoofPersons.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["NOOFPERSONS"].Value);
                txtVechicleNo.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["VECHICLENO"].Value);
                txtPAName.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["PANAME"].Value);
                txtPAContactNo.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["PACONTACTNO"].Value);
                txtHostName.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["HOSTNAME"].Value);
                txtHostContact.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["HOSTCONTACT"].Value);
                txtRemarks.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["REMARKS"].Value);
                hdnVPCID.Text = Convert.ToString(gv.Rows[e.RowIndex].Cells["VPCId"].Value);
                btnSave.Text = "&Update";

                cmbTagNo.Enabled = false;
                cmbTagCateogry.Enabled = true;
                txtInviteeName.Enabled = true;
                cmbCategory.Enabled = true;
                txtNoofPersons.Enabled = true;
                txtVechicleNo.Enabled = true;
                txtPAName.Enabled = true;
                txtPAContactNo.Enabled = true;
                txtHostName.Enabled = true;
                txtHostContact.Enabled = true;
                txtRemarks.Enabled = true;
                btnSave.Enabled = true;

            }
            else
            {
                cmbTagNo.Enabled = cmbTagCateogry.Enabled = txtInviteeName.Enabled = cmbCategory.Enabled = txtNoofPersons.Enabled = txtNoofPersons.Enabled = true;
                txtVechicleNo.Enabled = txtPAName.Enabled = txtPAContactNo.Enabled = txtHostName.Enabled = txtHostContact.Enabled = txtNoofPersons.Enabled = txtRemarks.Enabled = true;
                btnSave.Text = "&Save";
                cmbTagNo.SelectedIndex = 0;
                cmbTagCateogry.SelectedIndex = 0;
                txtInviteeName.Text = string.Empty;
                cmbCategory.SelectedIndex = 0;
                txtNoofPersons.Text = "0";
                txtVechicleNo.Text = string.Empty;
                txtPAName.Text = string.Empty;
                txtPAContactNo.Text = string.Empty;
                txtHostName.Text = string.Empty;
                txtHostContact.Text = string.Empty;
                txtRemarks.Text = string.Empty;
            }
        }

        private void gv_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            int width = e.Column.Width;
            if (e.Column.DataPropertyName == "")
                //e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                e.Column.Width = 25;
            //else if (e.Column.DataPropertyName == "RTAAPPROVEDQTY" || e.Column.DataPropertyName == "DESTINATION")
            //    e.Column.Width = 121;
            else
                e.Column.Width = 97;
        }

        private void btnGetTagNo_Click(object sender, EventArgs e)
        {

        }

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
                        BindEmptyGrid();

                    }
                }
                else
                {
                    if (UpdateData())
                    {
                        btnSave.Enabled = false;
                        MessageBox.Show("RECORD UPDATE SUCCESSFULLY", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindEmptyGrid();
                    }
                }
            }
        }
        private bool SaveData()
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }

                string str = "insert into [dbo].[DelegateMaster] ([TagId],[DelegateName],[Category],[Company],[NoofPeople],[PAName],[PAMobileNo],[VehicleNo],[HostName],[HostMobileNo],[Remarks],[VPCId],CreatedBy,CreatedTime,Status) Values (@TagId,@DelegateName,@Category,@Company,@NoofPeople,@PAName,@PAMobileNo,@VehicleNo,@HostName,@HostMobileNo,@Remarks,@VPCId,@CreatedBy,GetDate(),@Status)";
                SqlCommand inscmd = new SqlCommand(str, Sqlcon);
                inscmd.Parameters.AddWithValue("@TagId", cmbTagNo.SelectedValue.ToString());
                inscmd.Parameters.AddWithValue("@DelegateName", txtInviteeName.Text.ToString());
                inscmd.Parameters.AddWithValue("@Category", cmbCategory.Text.ToString());
                inscmd.Parameters.AddWithValue("@Company", txtCompany.Text.ToString());
                inscmd.Parameters.AddWithValue("@NoofPeople", txtNoofPersons.Text.ToString());
                inscmd.Parameters.AddWithValue("@PAName", txtPAName.Text.ToString());
                inscmd.Parameters.AddWithValue("@PAMobileNo", txtPAContactNo.Text.ToString());
                inscmd.Parameters.AddWithValue("@VehicleNo", txtVechicleNo.Text.ToString());
                inscmd.Parameters.AddWithValue("@HostName", txtHostName.Text.ToString());
                inscmd.Parameters.AddWithValue("@HostMobileNo", txtHostContact.Text.ToString());
                inscmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                inscmd.Parameters.AddWithValue("@VPCId", cmbTagCateogry.SelectedValue.ToString());
                inscmd.Parameters.AddWithValue("@CreatedBy", GlobalVariables.LGNID);
                inscmd.Parameters.AddWithValue("@Status", "Active");

                int eta = inscmd.ExecuteNonQuery();

                string upstr = "update [dbo].[TagMst] set [Status]='Y' where TagId=" + cmbTagNo.SelectedValue.ToString();
                SqlCommand upcmd = new SqlCommand(upstr, Sqlcon);
                upcmd.ExecuteNonQuery();
                if (eta == 1)
                {
                    return true;
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
        private bool UpdateData()
        {
            try
            {
                if (Sqlcon.State == ConnectionState.Closed || Sqlcon.State == ConnectionState.Broken)
                {
                    Sqlcon.Open();
                }
                SqlCommand qrycmd = new SqlCommand("update [dbo].[DelegateMaster] set [DelegateName]=@DelegateName,[Category]=@Category,[Company]=@Company,[NoofPeople]=@NoofPeople,[PAName]=@PAName,[PAMobileNo]=@PAMobileNo,[VehicleNo]=@VehicleNo,[HostName]=@HostName,[HostMobileNo]=@HostMobileNo,[Remarks]=@Remarks,VPCId=@VPCId,ModifiedBy=@ModifiedBy,ModifiedTime=GetDate()  where DMId=@DMId", Sqlcon);
                qrycmd.Parameters.AddWithValue("@DMId", hdnDMID.Text.ToString());
                qrycmd.Parameters.AddWithValue("@DelegateName", txtInviteeName.Text.ToString());
                qrycmd.Parameters.AddWithValue("@Category", cmbCategory.Text.ToString());
                qrycmd.Parameters.AddWithValue("@Company", txtCompany.Text.ToString());
                qrycmd.Parameters.AddWithValue("@NoofPeople", txtNoofPersons.Text.ToString());
                qrycmd.Parameters.AddWithValue("@PAName", txtPAName.Text.ToString());
                qrycmd.Parameters.AddWithValue("@PAMobileNo", txtPAContactNo.Text.ToString());
                qrycmd.Parameters.AddWithValue("@VehicleNo", txtVechicleNo.Text.ToString());
                qrycmd.Parameters.AddWithValue("@HostName", txtHostName.Text.ToString());
                qrycmd.Parameters.AddWithValue("@HostMobileNo", txtHostContact.Text.ToString());
                qrycmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                qrycmd.Parameters.AddWithValue("@VPCId", cmbTagCateogry.SelectedValue.ToString());
                qrycmd.Parameters.AddWithValue("@ModifiedBy", GlobalVariables.LGNID);
                qrycmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            DetailsClear();
            btnSave.Text = "&Save";
            btnSave.Enabled = true;
            BindEmptyGrid();
            
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

        private void DelegateDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            cmbTagNo.Leave -= cmbTagNo_Leave;
        }

        private void txtPAContactNo_Validating(object sender, CancelEventArgs e)
        {
            const string str = "0123456789";
            bool res = true;
            for (int i = 0; i <= txtPAContactNo.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtPAContactNo.Text.Substring(i, 1);
                for (j = 0; j <= str.Length - 1; j++)
                    if (strchar == str.Substring(j, 1))
                        break;
                if (j == str.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                MessageBox.Show("ENTER NUMBERS ONLY", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAContactNo.Focus();
                return;
            }           
        }

        private void txtHostContact_Validating(object sender, CancelEventArgs e)
        {
            const string str = "0123456789";
            bool res = true;
            for (int i = 0; i <= txtHostContact.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtHostContact.Text.Substring(i, 1);
                for (j = 0; j <= str.Length - 1; j++)
                    if (strchar == str.Substring(j, 1))
                        break;
                if (j == str.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                MessageBox.Show("ENTER NUMBERS ONLY", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHostContact.Focus();
                return;
            }  
        }

        private void txtNoofPersons_Validating(object sender, CancelEventArgs e)
        {
            const string str = "0123456789";
            bool res = true;
            for (int i = 0; i <= txtNoofPersons.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtNoofPersons.Text.Substring(i, 1);
                for (j = 0; j <= str.Length - 1; j++)
                    if (strchar == str.Substring(j, 1))
                        break;
                if (j == str.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                MessageBox.Show("ENTER NUMBERS ONLY", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoofPersons.Focus();
                return;
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
                string strinsgv = "SELECT [DMId] DMId,DM.[TagId] TAGID,TM.TagNo Tagno,DM.[DelegateName] InviteeName,DM.[Category] Category,VPC.Category VPCategory,DM.[Company] Company,DM.[NoofPeople] NoOfPersons,DM.[PAName] PAName,DM.[PAMobileNo] PAContactNo,DM.[VehicleNo] VechicleNo,DM.[HostName] HostName,DM.[HostMobileNo] HostContact,DM.[Remarks] Remarks,VPC.VPCId FROM  [dbo].[DelegateMaster] DM,[dbo].[TagMst] TM ,[dbo].[VisitorPassCategory] VPC where VPC.VPCId=DM.VPCId and  DM.TagId=TM.TagId and DelegateName like '%" + txtSearch + "%'  order by DM.DMId desc";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strinsgv, Sqlcon);
                da.Fill(ds);

                gv.DataSource = ds.Tables[0];
                //gv.Columns.Add(cc);
                DetailsControlsEnable();
                for (int i = 0; i <= gv.Rows.Count - 1; i++)
                {
                    gv.Columns["DMId"].Visible = false;
                    gv.Columns["TAGID"].Visible = false;
                    gv.Columns["VPCId"].Visible = false;
                }
                cmbTagNo.Select();
                gv.ShowEditingIcon = false;
            }
        }

        private void cmbTagNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
        }

    }
}
