using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;
using System.Data.OleDb;
namespace WindowsApp
{
    public partial class SubStaff : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public SubStaff()
        {
            InitializeComponent();
        }

        private void kryptonPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        public void getDepartmentsList()
        {
            string CONN_STRING = ConfigurationManager.ConnectionStrings["con2"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            dbCon.Open();
            string query = "SELECT DEPTNAME,DEPTID FROM hrms.HM_DEPARTMENT where STATUS='A'";
            OleDbDataAdapter da = new OleDbDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["DEPTNAME"] = "---Select---";
            dt.Rows.InsertAt(drow, 0);
            ComboDepartment.DataSource = dt;
            ComboDepartment.DisplayMember = "DEPTNAME";
            ComboDepartment.ValueMember = "DEPTID";
            dbCon.Close();
        }
        public void getDesignationsList()
        {
            //string CONN_STRING = ConfigurationManager.ConnectionStrings["con2"].ConnectionString;
            //OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
            //dbCon.Open();
            //string query = "SELECT DESGNAME,DESGID FROM setp.SM_DESGMST where STATUS='A'";
            //OleDbDataAdapter da = new OleDbDataAdapter(query, dbCon);
            //DataTable dt1 = new DataTable();
            //da.Fill(dt1);
            //DataRow drow = dt1.NewRow();
            //drow["DESGNAME"] = "---Select---";gridSubStaff.Columns.
            //dt1.Rows.InsertAt(drow, 0);
            //ComboDesignation.DataSource = dt1;
            //ComboDesignation.DisplayMember = "DESGNAME";
            //ComboDesignation.ValueMember = "DESGID";
            //dbCon.Close();
        }
        private void SubStaff_Load(object sender, EventArgs e)
        {
            getDepartmentsList();
            getDesignationsList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridSubStaff.Rows)
                {
                    string SubStaffCode = row.Cells["SubStaffCode"].Value.ToString().Trim();
                    string SubStaffName = row.Cells["SubStaffName"].Value.ToString();
                    string Department = row.Cells["Department"].Value.ToString();
                    //Dept Code--Start--
                    string CONNSTRING1 = ConfigurationManager.ConnectionStrings["con2"].ConnectionString;
                    OleDbConnection cn1 = new OleDbConnection(CONNSTRING1);
                    cn1.Open();
                    string sql1;
                    sql1 = "select DEPTID from hrms.HM_DEPARTMENT where DEPTNAME='" + Department + "'";
                    OleDbCommand cmd1 = new OleDbCommand(sql1, cn1);
                    int DEPTID;
                    DEPTID = int.Parse(cmd1.ExecuteScalar().ToString());
                    cn1.Close();
                    //Dept Code--End--
                    string Designation = row.Cells["Designation"].Value.ToString().Trim();
                    //Disg Code--Start--
                    string CONNSTRING = ConfigurationManager.ConnectionStrings["con2"].ConnectionString;
                    OleDbConnection cn = new OleDbConnection(CONNSTRING);
                    cn.Open();
                    string sql;
                    sql = "select DESGID FROM setp.SM_DESGMST where DESGNAME='" + Designation + "'";
                    OleDbCommand cmd = new OleDbCommand(sql, cn);
                    int DESGID;
                    DESGID = int.Parse(cmd.ExecuteScalar().ToString());
                    cn.Close();
                    //Disg Code--End--
                    
                    string ContactNo = row.Cells["ContactNo"].Value.ToString();
                    string Remarks = row.Cells["Remarks"].Value.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Records saved Successfully...");
            }
        }

        
    }
}