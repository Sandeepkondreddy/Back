using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
namespace KPCLVisitor
{
    
    public partial class formSQL_DBConnection : Form
    {
        string localConnection = WAP3.classServerDetails.SdfConnection;
        public formSQL_DBConnection()
        {
            InitializeComponent();
        }

        public void getDBConfigdetails()//getting the details from SQL DB Config details from local database...
        {
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select DataSource,InitialCatalog,UserId,Password  from DBConnection where Status='Active'";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                txtDataSource.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txtInitialCatalog.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                txtLoginId.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                txtPassword.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
                dbCon.Close();
            }
            catch
            {
                MessageBox.Show("Config Details Not found."); WAP3.classLog.writeLog("Error @: SQL1 Config : Config Details Not found.");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            try
            {
                dbCon.Open();
                string datet = System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                string ndt = "{ts '" + datet + "'}";
                SqlCeCommand cmd = new SqlCeCommand("update DBConnection set DataSource='" + txtDataSource.Text.Trim() + "', InitialCatalog='" + txtInitialCatalog.Text.Trim() + "',UserId='" + txtLoginId.Text.Trim() + "',Password='" + txtPassword.Text.Trim() + "' where Status='Active'", dbCon);
                string newconfigstatus = verifyLocalSQLDBConfigStstus();
                if (newconfigstatus == "Success") MessageBox.Show("Connection Success");
                if (newconfigstatus == "Failed") MessageBox.Show("Connection Failed");
                cmd.ExecuteNonQuery();
                dbCon.Close();
                MessageBox.Show("Configuration Updated..."); WAP3.classLog.writeLog("Error @: SQL1 Config : Configuration Updated Successfully...");
                getDBConfigdetails();
            }
            catch
            {
                MessageBox.Show("Configuration Faild..."); WAP3.classLog.writeLog("Error @: SQL1 Config : Configuration Faild...");
            }
        }

        private void formSQL_DBConnection_Load(object sender, EventArgs e)
        {
            getDBConfigdetails(); WAP3.classLog.writeLog("Message @: SQL1 Config : formSQL_DBConnection Loaded...");
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new WAP3.formConfig();
            config.Show();
            this.Hide();
            WAP3.classLog.writeLog("Message @: SQL1 Config : formSQL_DBConnection Closed...");
        }

        public string verifyLocalSQLDBConfigStstus()//getting the details from SQL DB Config details from local database...
        {
            string ServerConnectionStatus = "Failed";
            //string CONN_STRING = localConnection;
            //SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            //string cmd = "select DataSource,InitialCatalog,UserId,Password  from DBConnection where Status='Active'";
            try
            {
                //dbCon.Open();
                string connection = "Data Source=" + txtDataSource.Text.Trim() + " ;Initial Catalog=" + txtInitialCatalog.Text.Trim() + ";User ID=" + txtLoginId.Text.Trim() + ";Password=" + txtPassword.Text.Trim() + ";";
                WAP3.classLog.writeLog("Message @: SQL1 Connection: " + connection.ToString());
                SqlConnection dbCon = new SqlConnection(connection); 
                dbCon.Open();
                dbCon.Close(); ServerConnectionStatus = "Success";
                WAP3.classLog.writeLog("Message @: SQL1 Connection: Connection Success" );
                
                
            }
            catch (Exception ex)
            {
                ServerConnectionStatus = "Failed";
                WAP3.classLog.writeLog("Message @: SQL1 Connection: Connection Failed");
                WAP3.classLog.writeLog("Message @: SQL1 Connection: "+ex.ToString());
            }
            return ServerConnectionStatus;
        }
    }
}