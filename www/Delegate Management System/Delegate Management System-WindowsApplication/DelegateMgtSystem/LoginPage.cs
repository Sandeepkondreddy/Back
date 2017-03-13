using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using DelegateMgtSystem.DAL;
using System.Data.SqlClient;

namespace DelegateMgtSystem
{
    public partial class LoginPage : Form
    {

        public static string ip, mac;
        string CONNSTRING = GlobalVariables.Sqlconnection;
        OleDbConnection con = new OleDbConnection(GlobalVariables.CON_STRING);
        SqlConnection cn;
        public LoginPage()
        {
            InitializeComponent();
            GlobalVariables.OfflineFlag = "N";
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(CONNSTRING);
            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken)
            {
                cn.Open();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin checkuser;
            checkuser = new UserLogin();
            if (checkuser.getUserInfo(txtUserName.Text.Trim(), txtPWD.Text.Trim()) == true)
            {
                GetLogDetails();
                GlobalVariables.EmpName = txtUserName.Text.Trim().ToString();
                lblMsg.Visible = false;
                MainPage frm = new MainPage();
                frm.Show();
                this.Hide();
            }
            else
            {
                //MessageBox.Show("Invalid User Name or Password. Please Check Your Username and Password");
                lblMsg.Text = "* Invalid User Name or Password.";
                lblMsg.Visible = true;
            }
        }
        private void GetLogDetails()
        {
            try
            {
                //LGNID - Emp Id - GlobalVariables.EmpId
                //USRID - Seq
                //USERNAME - Login ID - GlobalVariables.LGNID
                //GROUPNAME - Designation - GlobalVariables.GrpName
                //APPLICATION - Running Application, VERSIONTYPE - Version No, SYSTEMIP - Current Sys IP, LOGINTIME - LoginTime

                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //SqlCommand comd = new SqlCommand("Insert into  [dbo].[APPVersion_LOG] (LGNID,USRID,USERNAME,GROUPNAME,APPLICATION,VERSIONTYPE,SYSTEMIP,LOGINTIME) values (" + GlobalVariables.EmpId + "," + GlobalVariables.LoginID + ",'" + GlobalVariables.LGNID + "','" + GlobalVariables.GrpName + "','" + GlobalVariables.ApplicationName + "','" + GlobalVariables.version + "','" + myIP + "',GETDATE())", cn);
                //comd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void btnLoginOffline_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("USER NAME IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Text = "";
                txtUserName.Focus();
                return;
            }
            if (txtPWD.Text == "")
            {
                MessageBox.Show("PASSWORD IS REQUIRED.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Text = "";
                txtPWD.Focus();
                return;
            }
            else if (txtUserName.Text != "" && txtPWD.Text != "")
            {
                string Qry = string.Format("select count(UserName) from Users where UserName='{0}' AND Password='{1}'", txtUserName.Text.ToUpper(), txtPWD.Text);
                OleDbCommand cd = new OleDbCommand(Qry, con);
                if (con.State != ConnectionState.Open)
                    con.Open();
                int i = Convert.ToInt32(cd.ExecuteScalar().ToString());
                con.Close();
                if (i == 0)
                {
                    MessageBox.Show("USERNAME/PASSWORD ARE INVALID, TRY AGAIN!.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    txtUserName.Text = "";
                    txtPWD.Text = "";
                    return;
                }
                else
                {
                    string strQry = string.Format("SELECT UserName,Password FROM Users where UserName='{0}' AND Password='{1}'", txtUserName.Text.ToUpper(), txtPWD.Text);
                    OleDbCommand cmd = new OleDbCommand(strQry, con);
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    OleDbDataReader mydr = cmd.ExecuteReader();
                    if (mydr.HasRows == true)
                    {
                        while (mydr.Read())
                        {
                            GlobalVariables.EmpName = mydr["UserName"].ToString().ToUpper();
                            GlobalVariables.OfflineFlag = "Y";
                        }
                    }
                    con.Close();
                    MainPage mdi = new MainPage();
                    //mdi.txtUserName.Text = txtUserName.Text.ToUpper().Replace("'", "").Replace("'", "");
                    GlobalVariables.LGNID = txtUserName.Text.ToUpper();
                    mdi.Show();
                    Hide();
                }
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            const string str1 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            bool res = true;
            for (int i = 0; i <= txtUserName.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtUserName.Text.Substring(i, 1);
                for (j = 0; j <= str1.Length - 1; j++)
                    if (strchar == str1.Substring(j, 1))
                        break;
                if (j == str1.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                //MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", 530, 134);
                MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Text = "";
                txtUserName.Focus();
                return;
            }
        }

        private void txtPWD_Validating(object sender, CancelEventArgs e)
        {
            const string str1 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            bool res = true;
            for (int i = 0; i <= txtPWD.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtPWD.Text.Substring(i, 1);
                for (j = 0; j <= str1.Length - 1; j++)
                    if (strchar == str1.Substring(j, 1))
                        break;
                if (j == str1.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                //MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", 530, 134);
                MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Text = "";
                txtPWD.Focus();
                return;
            }
        }

        private void LoginPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void txtPWD_KeyUp(object sender, KeyEventArgs e)
        {
            const string str1 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            bool res = true;
            for (int i = 0; i <= txtPWD.Text.Length - 1; i++)
            {
                int j;
                string strchar = txtPWD.Text.Substring(i, 1);
                for (j = 0; j <= str1.Length - 1; j++)
                    if (strchar == str1.Substring(j, 1))
                        break;
                if (j == str1.Length)
                {
                    res = false;
                    break;
                }
            }
            if (!res)
            {
                //MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", 530, 134);
                MessageBox.Show("YOU SHOULD ENTER ALPHA-NUMERIC CHARACTERS ONLY.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPWD.Text = "";
                txtPWD.Focus();
                return;
            }
        }

        private void txtPWD_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}+{END}");
        }
    }
}
