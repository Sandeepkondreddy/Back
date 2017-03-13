using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
namespace WAP3
{
    public partial class formReason : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string readerno; string TAG; string saveops; string QTY; string OPLOCID;
        public formReason(string tagno, string truckno, string readerno, string saveoperation, string savetime, string User, string usertype, string location, string Qty, string OPLOCid)
        {
            InitializeComponent();
            txtTruckNo.Text = truckno;
            TAG = tagno;
            saveops = saveoperation; QTY = Qty; OPLOCID = OPLOCid;
            txtStage.Text = saveoperation.ToString();
        }

        public void ReasonMst() //Reason Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT ReasonName,Id FROM [ReasonMst] ORDER BY Id";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["ReasonName"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbReasonName.DataSource = dt;
            cmbReasonName.DisplayMember = "ReasonName";
            cmbReasonName.ValueMember = "Id";
            dbCon.Close();
        }
        public void gethandledetailsfromlocaldb()//HHD Details
        {
            string myHost = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
            string CONN_STRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONN_STRING);
            string cmd = "select ReaderNo  from ReaderConfig where Status='A'";
            try
            {
                dbCon.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd, dbCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                readerno = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                dbCon.Close();
            }
            catch
            {
                MessageBox.Show("Config Details Not found.");
            }
        }
        private void formReason_Load(object sender, EventArgs e)
        {
            ReasonMst(); gethandledetailsfromlocaldb(); btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (cmbReasonName.Text == "OTHER" && txtRemarks.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Please enter Remarks."); txtRemarks.Focus();
            }
            else
            {
                string reasonid = cmbReasonName.SelectedValue.ToString(); btnSave.Enabled = false;
                string reasondesc = txtRemarks.Text.ToString();
                string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                if (classLogin.Connectivity == "WIFI")
                {
                    string wifistate = classConnectivityCheck.WifiSignalStrenthCheck();
                    if (wifistate == "Success")
                    {
                        string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        if (ConnectionStatus == "Failed")
                        {
                            ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        }
                        if (ConnectionStatus == "Success")
                        {
                            KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                            string state = details.InsertReasonDetailsOracle(TAG, classLogin.ReaderNo.ToString(), saveops, savetime, classLogin.User.ToString(), classLogin.UserType.ToString(), classLogin.Location.ToString(), QTY, OPLOCID, reasonid, reasondesc);
                            if (state == "SUCCESS") this.Close();
                            else lblStatus.Text = state.ToString();
                        }
                        else
                        {
                            classLog.writeLog("Error @: KPCT Operations-Save: Web Service Not Connected.");
                            lblStatus.Text = "Web Service Not Connected.";
                        }
                        
                    }
                    else
                    {
                        classLog.writeLog("Error @: KPCT Operations-Save: Wifi Signal Week.");
                        lblStatus.Text = "Wifi Signal Week.";
                    }
                }
            }
        }
    }
}