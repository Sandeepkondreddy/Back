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
    public partial class formAddUser : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formAddUser()
        {
            InitializeComponent();
        }

        private void formAddUser_Load(object sender, EventArgs e)
        {
            loadgrid();
        }
        public void clear()
        {
            txtPassword.Text = "";
            txtEMPName.Text = "";
            txtEMPNo.Text = "";
            txtRetypePassword.Text = "";
            txtUserID.Text = "";
        }
        public void loadgrid()
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONNSTRING);
            dbCon.Open();
            string Query = "SELECT Emp_No,Emp_Name,User_Name,Status FROM UserMast order by Emp_Name";
            OleDbCommand cmd = new OleDbCommand(Query, dbCon);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(da);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);

            DataSet ds = new DataSet();
            da.Fill(ds, "Query");
            gdvUserDetails.DataMember = "Query";
            gdvUserDetails.DataSource = ds;
            gdvUserDetails.Visible = true;
            dbCon.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
             if (txtEMPNo.Text.Trim() == "")
            {
                MessageBox.Show("Enter EMP No.");
            }
             else if (txtEMPName.Text.Trim() == "")
             {
                 MessageBox.Show("Enter Employee Name.");
             }
             else if (txtEMPName.Text.Trim() == "")
             {
                 MessageBox.Show("Enter Employee Name.");
             }
            else if (txtUserID.Text.Trim() == "" && txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter User Name and Password.");
            }
            else if (txtUserID.Text.Trim() == "")
            {
                MessageBox.Show("Enter User Name.");
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter Password.");
            }
            else if (txtRetypePassword.Text.Trim() == "")
            {
                MessageBox.Show("Enter Confirm Password.");
            }
            //    else if(txtPassword.Text.Trim()==txtRetypePassword.Text.Trim())
            //{
            //    MessageBox.Show("Confirm Password and Password should be same.");
            //    txtPassword.Text = "";
            //    txtRetypePassword.Text = "";
            //    txtPassword.Focus();
            //}
            
            else
            {
                AddUsers checkuser;
                checkuser = new AddUsers();
                if (checkuser.checkuserinfo(txtUserID.Text.Trim()) == true)
                {
                    MessageBox.Show("User ID is already exists chang your User Name.");
                }
                else
                {
                    AddUsers newuser;
                    newuser = new AddUsers();
                    if (newuser.insertuser(txtUserID.Text.Trim().ToLower(), txtPassword.Text.Trim(),txtEMPNo.Text.Trim(),txtEMPName.Text.Trim()) == true)
                    {
                        MessageBox.Show("User Added Successfully.");
                        clear();
                        loadgrid();
                    }
                    else
                    {
                        MessageBox.Show("Unable to Add User.");
                    }

                }
            }
        }

        private void gdvUserDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}