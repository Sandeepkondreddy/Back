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
    public partial class formChangePassword : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formChangePassword()
        {
            InitializeComponent();
            loadcombo();
        }
        public void clear()
        {
            cmbUserId.Text="";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
        }
        public void loadcombo()
        {
            cmbUserId.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUserId.AutoCompleteSource = AutoCompleteSource.ListItems;
            string CONNSTRING = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            OleDbConnection dbCon = new OleDbConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT User_Name FROM UserMast";
            
            OleDbDataAdapter da = new OleDbDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbUserId.DataSource = dt;
            cmbUserId.DisplayMember = "User_Name";
            cmbUserId.ValueMember = "User_Name";
            dbCon.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbUserId.Text == "")
            {
                MessageBox.Show("Please select User ID.");
            }
            else if (txtOldPassword.Text == "")
            {
                MessageBox.Show("Please enter Old Password.");
            }
            else if (txtNewPassword.Text == "")
            {
                MessageBox.Show("Please enter New Password.");
            }
            else
            {
                ChangePasswod checkuser;
                checkuser = new ChangePasswod();
                if (checkuser.checkuserinfo(cmbUserId.Text.Trim(),txtOldPassword.Text.Trim()) == false)
                {
                    MessageBox.Show("Invalid Login Id or Password.");
                }
                else if (checkuser.checkuserinfo(cmbUserId.Text.Trim(), txtOldPassword.Text.Trim()) == true)
                {
                    ChangePasswod changepasswd;
                    changepasswd = new ChangePasswod();
                    if (changepasswd.updatePasswd(cmbUserId.Text.Trim().ToLower(), txtNewPassword.Text.Trim()) == true)
                    {
                        MessageBox.Show("Password updated Successfully.");
                        clear();
                        
                    }
                    else
                    {
                        MessageBox.Show("Unable to Update Password.");
                    }

                }
            }
        }

        private void formChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}