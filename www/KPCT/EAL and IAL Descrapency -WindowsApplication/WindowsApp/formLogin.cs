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
    public partial class formLogin : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formLogin()
        {
            
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin checkuser;
            checkuser = new UserLogin();
            if (checkuser.getUserInfo(txtUserID.Text.Trim(), txtPassword.Text.Trim()) == true)
            {
                lblMsg.Visible = false;
                formHome frm = new formHome();
                frm.Show();
                this.Hide();
                //FileForm hme = new FileForm();
                //hme.Show();
                //this.Hide();
            }
            else
            {
                //MessageBox.Show("Invalid User Name or Password. Please Check Your Username and Password");
                lblMsg.Text = "Invalid User Name or Password."; classLog.writeLog("Error @:Login Details Invalid...");
                lblMsg.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); classLog.writeLog("Message @:Application Exited...");
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            classLog.writeLog("Message @:Application Login Screen Loading...");
            CheckConnection();
        }
        public void CheckConnection()
        {
            try
            {
                for(int i=5;i>0;i--)
                {
                    try
                    {
                        string CONN_STRING = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
                        OleDbConnection dbCon = new OleDbConnection(CONN_STRING);
                        dbCon.Open();
                        dbCon.Close();
                    }
                    catch (Exception ex)
                    {
                        classLog.writeLog("Exception @:Oracle Connection con1 Problem");
                        classLog.writeLog("Exception @:"+ex.ToString());
                    }
                    try
                    {
                        string CONN = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                        OleDbConnection db_Con = new OleDbConnection(CONN);
                        db_Con.Open();
                        db_Con.Close();
                    }
                    catch (Exception ex)
                    {
                        classLog.writeLog("Exception @:MS Access Connection con Problem");
                        classLog.writeLog("Exception @:" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                //MessageBox.Show("Database Disconnected. Please Contact Administrator.");
                lblMsg.Text = "Database Disconnected. Please Contact Administrator";
                txtUserID.Enabled = false;
                txtPassword.Enabled = false;
                btnLogin.Enabled = false;
            }
            
        }
        
    }
}