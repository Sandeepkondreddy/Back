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
    public partial class formTallySheetNew : Form
    {
        string localConnection = classServerDetails.SdfConnection;
        string TAG;
        string readerno; string Ploc;
        public formTallySheetNew(string tagno, string truckno, string PLoc, string SaveOperation)
        {
            InitializeComponent();
            txtTruckNo.Text = truckno.ToString();
            
            //if (cmbLocation.Items.Contains(OPLoc.ToString()) != null)
            //{
            //    cmbLocation.Text = OPLoc.ToString();
            //}
            Ploc = PLoc;
            TAG = tagno;
            btnSave.Text = SaveOperation.ToString();
        }
        public void selectOpLocation(string PlanedLoc)
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT LocationName,LId FROM [LocationMst] where LocationName='" + PlanedLoc + "'";
            classLog.writeLog("Messge @: KPCT Operations: Find Location:"+query.ToString());
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmbLocation.SelectedValue = ds.Tables[0].Rows[0].ItemArray[1];
            }
            else cmbLocation.SelectedValue = 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnDisable();
            if (radioCarting.Checked == false && radioStuffing.Checked == false) { lblStatus.Text = "Select Carting or Stuffing."; radioCarting.Focus(); }
            else if (cmbLocation.Text == "Select") { lblStatus.Text = "Select Location details"; cmbLocation.Focus(); }
            else if (cmbCargoCondition.Text == "Select") { lblStatus.Text = "Select Cargo Condition"; cmbCargoCondition.Focus(); }
            else if (cmbHandledBy.Text == "Select") { lblStatus.Text = "Select Handled By"; cmbHandledBy.Focus(); }
            else if (cmbHandledType.Text == "Select") { lblStatus.Text = "Select Handild Type"; cmbHandledType.Focus(); }
            else if (txtOpQty.Text == "0" && TSRequired == "Yes") { lblStatus.Text = "Tally Sheet Details Required"; txtWidth.Focus(); }
            else if (radioStuffing.Checked == true && txtContainerNo.Text.Trim().ToString() == "") { lblStatus.Text = "Container No Required"; txtContainerNo.Focus(); }
            else
            {
                insertKPCTDetailstoWS(TAG);
            }
            btnEnable();
        }
        public void insertKPCTDetailstoWS(string tagno) //inserting data to Web Service
        {

            if (tagno.ToString() == "")
            {
                MessageBox.Show("Tag details not found.");
                classLog.writeLog("Warning @: KPCT Operations: Tag details not found.");
            }
            else
            {
                string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                string OPLOCid = cmbLocation.SelectedValue.ToString();
                string HandledType;
                if (cmbHandledType.Text == "Select") HandledType = "";
                else HandledType = cmbHandledType.Text;
                string TallySheetType = "";
                if (radioCarting.Checked == true) TallySheetType = radioCarting.Text.ToString();
                else if (radioStuffing.Checked == true) TallySheetType = radioStuffing.Text.ToString();
                
                try
                {
                    string state = "";
                    if (classLogin.Connectivity == "WIFI")
                    {
                        KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                        if (classServerDetails.DBType == "Oracle") state = details.InsertKPCTOperationDetailsOracle(tagno, classLogin.ReaderNo.ToString(), btnSave.Text, savetime, classLogin.User, classLogin.UserType.ToString(), classLogin.Location.ToString(), txtOpQty.Text.ToString(), OPLOCid, cmbCargoCondition.SelectedValue.ToString(), cmbWeatherCondition.SelectedValue.ToString(), cmbHandledBy.SelectedValue.ToString(), HandledType, TallySheetType, txtContainerNo.ToString());
                    }
                    else if (classLogin.Connectivity == "GPRS")
                    {
                        KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                        if (classServerDetails.DBType == "Oracle") state = details.InsertKPCTOperationDetailsOracle(tagno, classLogin.ReaderNo.ToString(), btnSave.Text, savetime, classLogin.User, classLogin.UserType.ToString(), classLogin.Location.ToString(), txtOpQty.Text.ToString(), OPLOCid, cmbCargoCondition.SelectedValue.ToString(), cmbWeatherCondition.SelectedValue.ToString(), cmbHandledBy.SelectedValue.ToString(), HandledType, TallySheetType, txtContainerNo.ToString());
                    }
                    //classLog.writeLog("Info @: KPCT Operations:"+TagRegid+","+ tagno+"," +truckno+","+ trucktype+","+ readerno+","+readerip+","+ User+","+ unregtime);
                    if (state == "SUCCESS")
                    {
                        lblStatus.Text = "Saved..in Online.";
                        MessageBox.Show("Saved..Successfully.");

                        WAP3.formKPCTOps r = new WAP3.formKPCTOps(1);
                        r.btnVerify_Click(TAG); r._lbReadInfoReadWrite.Text = "Saved..in Online.";
                        this.Close();
                    }
                    else
                    {
                        classLog.writeLog("Error @: KPCT Operations Tally Sheet: "+state.ToString());
                        lblStatus.Text = state.ToString();
                    }
                }
                catch (Exception ex)
                {
                    classLog.writeLog("Error @: KPCT Operations TallySheet: Connection failed.");
                    classLog.writeLog("Exception @: " + ex.ToString());
                    lblStatus.Text = "Connection failed.";
                }
            }


        }
        private void txtOpQty_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtOpQty.Text, "[^0-9]"))
            {
                lblStatus.Text = "Please enter only numbers.";
                //MessageBox.Show("Please enter only numbers.");
                txtOpQty.Text = "";
            }
        }

        public void btnDisable()
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
            txtHight.Enabled = false;
            txtWidth.Enabled = false;
            cmbLocation.Enabled = false;
            cmbCargoCondition.Enabled = false;
            cmbHandledBy.Enabled = false;
            cmbHandledType.Enabled = false;
            cmbWeatherCondition.Enabled = false;
            txtOpQty.Enabled = false;
        }
        public void btnEnable()
        {
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
            txtHight.Enabled = true;
            txtWidth.Enabled = true;
            cmbLocation.Enabled = true;
            cmbCargoCondition.Enabled = true;
            cmbHandledBy.Enabled = true;
            cmbHandledType.Enabled = true;
            cmbWeatherCondition.Enabled = true;
            //txtOpQty.Enabled = true;
        }
        public void savedetails()
        {
            if (txtHight.Text == "" || txtWidth.Text == "")
            {
                lblStatus.Text = "Please Enter Hight&Width details.";
            }
            else if (txtHight.Text == "0" || txtWidth.Text == "0")
            {
                lblStatus.Text = "Please Enter Hight&Width details.";
            }
            else
            {
                if (classLogin.Connectivity == "WIFI")
                {
                    KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                    string WifiConnectionStatus = classConnectivityCheck.WifiSignalStrenthCheck();
                    if (WifiConnectionStatus == "Success")
                    {
                        string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        if (ConnectionStatus == "Failed")
                        {
                            ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                        }
                        if (ConnectionStatus == "Success")
                        {
                            string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                            string state = details.InsertKPCTTruckTallySheetDetailsOracleNew(TAG, txtWidth.Text.ToString(), txtHight.Text.ToString(), txtCTotal.Text.ToString(), classLogin.User, savetime);
                            if (state == "SUCCESS")
                            {
                                classLog.writeLog("Message @: KPCT Operations Tally Sheet: Saved successfully in DB.");
                                AddDetailstoDataGrid();
                                txtWidth.Text = ""; txtHight.Text = ""; txtCTotal.Text = "";
                            }
                            else
                            {
                                lblStatus.Text = state.ToString();
                                classLog.writeLog("Error @: KPCT Operations-TallySheet Insert: SaveFailed.");
                            }
                           }
                        else
                        {
                            lblStatus.Text = " Webservice Not Connected..";
                            classLog.writeLog("Error @: KPCT Operations-TallySheet Wifi:  Webservice Not Connected..");
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Wifi Network Not Connected..";
                        classLog.writeLog("Error @: KPCT Operations-TallySheet: Wifi Network Not Connected..");
                    }
                }
                else if (classLogin.Connectivity == "GPRS")
                {
                    string ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    if (ConnectionStatus == "Failed")
                    {
                        ConnectionStatus = classConnectivityCheck.WebServiceConnectivityCheck();
                    }
                    if (ConnectionStatus == "Success")
                    {
                        KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                        string savetime = System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
                        string state = details.InsertKPCTTruckTallySheetDetailsOracleNew(TAG, txtWidth.Text.ToString(), txtHight.Text.ToString(), txtCTotal.Text.ToString(), classLogin.User, savetime);
                        if (state == "SUCCESS")
                        {
                            classLog.writeLog("Error @: KPCT Operations Tally Sheet: Saved successfully in DB.");
                            AddDetailstoDataGrid(); txtWidth.Text = ""; txtHight.Text = ""; txtCTotal.Text = "";
                        }
                        else
                        {
                            classLog.writeLog("Error @: KPCT Operations-TallySheet Insert: SaveFailed.");
                        }
                    }
                    else
                    {
                        lblStatus.Text = " Webservice Not Connected..";
                        classLog.writeLog("Error @: KPCT Operations-TallySheet GPRS:  Webservice Not Connected..");
                    }
                }
                else
                {
                    lblStatus.Text = "Network Not Connected..";
                    classLog.writeLog("Error @: KPCT Operations-TallySheet: Network Not Connected..");
                }


            
            }
        }
        private void AddDetailstoDataGrid()
        {
            string sno=(dataGridTallySheet.VisibleRowCount + 1).ToString();
            object[] o = { sno.ToString(), txtWidth.Text, txtHight.Text, txtCTotal.Text};
            workTable.Rows.Add(o);
            dataGridTallySheet.DataSource = workTable;
            
            //Display Total Qty and Status---Start---
            int sum = 0;

            foreach (DataRow dr in workTable.Rows)
            {
                sum += Convert.ToInt32(dr["RTotal"]);
            }
            txtOpQty.Text = txtTotalQty1.Text = sum.ToString();
            lblStatus1.Text = lblStatus.Text = workTable.Rows.Count.ToString() + " Rows Inserted.";
            //Display Total Qty and Status---End---
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnDisable();
            savedetails();
            btnEnable(); txtWidth.Focus();
        }
        public void CargoConditionList() //Cargo Condition Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT Name,Id FROM [CargoConditionMst] ORDER BY Id";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["Name"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbCargoCondition.DataSource = dt;
            cmbCargoCondition.DisplayMember = "Name";
            cmbCargoCondition.ValueMember = "Id";
            dbCon.Close();
        }
        public void WeatherConditionList() //Weather Condition Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT Name,Id FROM [WeatherConditionMst] ORDER BY Id";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["Name"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbWeatherCondition.DataSource = dt;
            cmbWeatherCondition.DisplayMember = "Name";
            cmbWeatherCondition.ValueMember = "Id";
            dbCon.Close();
        }
        public void OpHandildbyList() //Operation Handild By Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT Name,Id FROM [OpHandledByMst] ORDER BY Id";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["Name"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbHandledBy.DataSource = dt;
            cmbHandledBy.DisplayMember = "Name";
            cmbHandledBy.ValueMember = "Id";
            dbCon.Close();
        }
        public void OpHandildTypeList() //Operation Handild Type Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT Name,Id FROM [OpsHandledTypeMst] ORDER BY Id";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["Name"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbHandledType.DataSource = dt;
            cmbHandledType.DisplayMember = "Name";
            cmbHandledType.ValueMember = "Id";
            dbCon.Close();
        }
        private void formTallySheetNew_Load(object sender, EventArgs e)
        {
            gethandledetailsfromlocaldb(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-Reader details Loaded Successfully.");
            DataGridArrangements(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-Grid Arrangements Completed Successfully.");
            
            LocationList(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-LocationList Loaded Successfully.");
            selectOpLocation(Ploc); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-Planed Location Selection Completed Successfully.");
            CargoConditionList(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-CargoConditionList Loaded Successfully.");
            WeatherConditionList(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-WeatherConditionList Loaded Successfully.");
            OpHandildbyList(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-OpHandildbyList Loaded Successfully.");
            OpHandildTypeList(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-OpHandildTypeList Loaded Successfully.");
            LoadTallySheetDetails(); classLog.writeLog("Messge @: KPCT Operations: Tally Sheet-LoadTallySheetDetails Loaded Successfully.");
        }
        #region DataGid
        DataTable workTable = new DataTable("TallySheet");
        public void DataGridArrangements()
        {
            
                workTable.Columns.Add("RowNo", typeof(String));
                workTable.Columns.Add("Width", typeof(String));
                workTable.Columns.Add("Height", typeof(String));
                workTable.Columns.Add("RTotal", typeof(String));
            dataGridTallySheet.DataSource = workTable;

            //--Start-- Grid Style---
            dataGridTallySheet.TableStyles.Clear();
            DataGridTableStyle tableStyle = new DataGridTableStyle();
            tableStyle.MappingName = workTable.TableName;
            foreach (DataColumn item in workTable.Columns)
            {
                DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
                tbcName.Width = 93;
                tbcName.MappingName = item.ColumnName;
                tbcName.HeaderText = item.ColumnName;
                tableStyle.GridColumnStyles.Add(tbcName);
            }
            dataGridTallySheet.TableStyles.Add(tableStyle);
            //--End-- Grid Style---
        }
        public void LocationList() //Location Master
        {
            string CONNSTRING = localConnection;
            SqlCeConnection dbCon = new SqlCeConnection(CONNSTRING);
            dbCon.Open();
            string query = "SELECT LocationName,LId FROM [LocationMst] ORDER BY LId";
            SqlCeDataAdapter da = new SqlCeDataAdapter(query, dbCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow drow = dt.NewRow();
            drow["LocationName"] = "Select";
            dt.Rows.InsertAt(drow, 0);
            cmbLocation.DataSource = dt;
            cmbLocation.DisplayMember = "LocationName";
            cmbLocation.ValueMember = "LId";
            dbCon.Close();
        }
        public void gethandledetailsfromlocaldb()
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

        string TSRequired = "";
        public void LoadTallySheetDetails()
        {
            TSRequired = "";
            DataSet ds = new DataSet();
            DataSet dstallylocdetails = new DataSet();
            if (classLogin.Connectivity == "WIFI")
            {
                KPCTSDS.appserver.Service1 details = new KPCTSDS.appserver.Service1();
                if (classServerDetails.DBVarient == "Oracle")
                {
                    ds = details.GetKPCTTruckTallySheetDetailsOracleNew(TAG);
                    TSRequired = details.GetKPCTTruckTallySheetValidationDetailsOracle(TAG);
                    if (btnSave.Text == "CT-YARD/WH OUT")
                    {
                        dstallylocdetails = details.GetKPCTTruckTallySheetLocationDetailsOracle(TAG);
                        cmbLocation.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[0];
                        cmbCargoCondition.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[1];
                        cmbWeatherCondition.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[2];
                        cmbHandledBy.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[3];
                        cmbHandledType.Text= dstallylocdetails.Tables[0].Rows[0].ItemArray[4].ToString();
                        txtContainerNo.Text = dstallylocdetails.Tables[0].Rows[0].ItemArray[6].ToString();
                        if (dstallylocdetails.Tables[0].Rows[0].ItemArray[5].ToString() == "Carting") radioCarting.Checked = true;
                        else if (dstallylocdetails.Tables[0].Rows[0].ItemArray[5].ToString() == "Stuffing") radioStuffing.Checked = true;
                    }
                }
            }
            else if (classLogin.Connectivity == "GPRS")
            {
                KPCTSDS.WebReference_GPRS.Service1 details = new KPCTSDS.WebReference_GPRS.Service1();
                if (classServerDetails.DBVarient == "Oracle")
                {
                    ds = details.GetKPCTTruckTallySheetDetailsOracleNew(TAG);
                    TSRequired = details.GetKPCTTruckTallySheetValidationDetailsOracle(TAG);
                    if (btnSave.Text == "CT-YARD/WH OUT")
                    {
                        dstallylocdetails = details.GetKPCTTruckTallySheetLocationDetailsOracle(TAG);
                        cmbLocation.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[0];
                        cmbCargoCondition.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[1];
                        cmbWeatherCondition.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[2];
                        cmbHandledBy.SelectedValue = dstallylocdetails.Tables[0].Rows[0].ItemArray[3];
                        cmbHandledType.Text = dstallylocdetails.Tables[0].Rows[0].ItemArray[4].ToString();
                    }
                }
                
            }
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("TRUCKID");
                workTable.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string sno=(dataGridTallySheet.VisibleRowCount + 1).ToString();
                    object[] o = { sno.ToString(), row["WIDTH"].ToString(), row["HEIGHT"].ToString(), row["COLTOTAL"].ToString() };
                    workTable.Rows.Add(o);
                }
            }
            dataGridTallySheet.DataSource = workTable;

            //Display Total Qty and Status---Start---
            int sum = 0;
            foreach (DataRow dr in workTable.Rows)
            {
                sum += Convert.ToInt32(dr["RTotal"]);
            }
            txtOpQty.Text = txtTotalQty1.Text = sum.ToString();
            lblStatus1.Text = lblStatus.Text = workTable.Rows.Count.ToString() + " Rows Inserted.";
        }
        #endregion

        private void txtHight_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtHight.Text, "[^0-9]"))
            {
                lblStatus.Text = "Please enter only numbers.";
                //MessageBox.Show("Please enter only numbers.");
                txtHight.Text = "";
            }
            else
            {
                if (txtHight.Text.Trim() != "" && txtWidth.Text.Trim() != "")
                {
                    txtCTotal.Text = (Convert.ToInt32(txtHight.Text.ToString()) * Convert.ToInt32(txtWidth.Text.ToString())).ToString();
                }
            }
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtWidth.Text, "[^0-9]"))
            {
                lblStatus.Text = "Please enter only numbers.";
                //MessageBox.Show("Please enter only numbers.");
                txtWidth.Text = "";

            }
            else
            {
                if (txtHight.Text.Trim() != "" && txtWidth.Text.Trim() != "")
                {
                    txtCTotal.Text = (Convert.ToInt32(txtHight.Text.ToString()) * Convert.ToInt32(txtWidth.Text.ToString())).ToString();
                }
            }
        }





    }
}